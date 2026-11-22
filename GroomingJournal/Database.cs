using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace GroomingJournal;

// Класс Database — вся работа с базой данных SQL Server собрана здесь.
// static = объект создавать не нужно, методы вызываются как Database.Метод().
public static class Database
{
    private const string ServerName = @"MISTO4KA\MSSQLSERVER01";

    private const string ConnectionString =
        $@"Server={ServerName};Database=GroomingSalon;Integrated Security=True;TrustServerCertificate=True";

    private const string MasterConnectionString =
        $@"Server={ServerName};Database=master;Integrated Security=True;TrustServerCertificate=True";

    public static void Initialize()
    {
        using var conn = new SqlConnection(MasterConnectionString);
        conn.Open();

        // DB_ID возвращает номер базы или NULL, если такой базы нет
        using (var check = new SqlCommand("SELECT DB_ID('GroomingSalon')", conn))
        {
            object id = check.ExecuteScalar();
            if (id != null && id != DBNull.Value)
                return;   // база уже создана
        }

        string script = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Database.sql"));

        // В скрипте команды разделены словом GO — это команда среды SQL Server,
        // а не SQL. Из C# её выполнить нельзя, поэтому режем скрипт по строкам "GO"
        // на отдельные части (пакеты) и выполняем по очереди.
        string[] batches = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        foreach (string batch in batches)
        {
            if (string.IsNullOrWhiteSpace(batch))
                continue;   // пропускаем пустые части

            // Команда USE GroomingSalon из скрипта переключает это же соединение
            // на новую базу, поэтому следующие части создаются уже в ней.
            using var cmd = new SqlCommand(batch, conn);
            cmd.ExecuteNonQuery();
        }
    }

    // ---------- Открыть соединение с базой ----------
    private static SqlConnection OpenConnection()
    {
        var conn = new SqlConnection(ConnectionString);
        conn.Open();
        return conn;
    }

    // ---------- Проверка подключения при запуске программы ----------
    public static void CheckConnection()
    {
        using var conn = OpenConnection();   // если не получится — вылетит исключение
    }

    // ---------- Получить список услуг (для выпадающего списка) ----------
    public static List<Service> GetServices()
    {
        var result = new List<Service>();

        // using — соединение и команда автоматически закроются в конце метода
        using var conn = OpenConnection();
        using var cmd = new SqlCommand("SELECT Id, Name, Price FROM Services ORDER BY Name", conn);

        // SqlDataReader читает результат запроса строка за строкой
        using var reader = cmd.ExecuteReader();
        while (reader.Read())   // Read() переходит к следующей строке; false — когда строки кончились
        {
            result.Add(new Service
            {
                Id = reader.GetInt32(0),      // 0 — первый столбец в SELECT (Id)
                Name = reader.GetString(1),   // 1 — второй столбец (Name)
                Price = reader.GetDecimal(2)  // 2 — третий столбец (Price)
            });
        }
        return result;
    }

    // ---------- Получить список видов животных ----------
    public static List<string> GetAnimalTypes()
    {
        var result = new List<string>();

        using var conn = OpenConnection();
        using var cmd = new SqlCommand("SELECT Name FROM AnimalTypes ORDER BY Name", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            result.Add(reader.GetString(0));

        return result;
    }

    // ---------- Получить записи журнала ----------
    // Если date передана — только записи за этот день, если null — все записи.
    public static List<Appointment> GetAppointments(DateTime? date)
    {
        var result = new List<Appointment>();

        using var conn = OpenConnection();
        using var cmd = new SqlCommand();
        cmd.Connection = conn;

        // JOIN объединяет таблицы: запись -> животное -> клиент и вид, запись -> услуга.
        // Так из пяти таблиц получается одна понятная строка журнала.
        string sql = @"
            SELECT a.Id, a.AppointmentDate, a.AppointmentTime,
                   p.Name, t.Name, p.Breed,
                   c.FullName, c.Phone,
                   s.Name, s.Price
            FROM Appointments a
            JOIN Pets        p ON p.Id = a.PetId
            JOIN Clients     c ON c.Id = p.ClientId
            JOIN AnimalTypes t ON t.Id = p.AnimalTypeId
            JOIN Services    s ON s.Id = a.ServiceId";

        if (date.HasValue)
        {
            sql += " WHERE a.AppointmentDate = @date";
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = date.Value.Date;
        }

        // Сортируем по дате, затем по времени — получается хронологический журнал
        sql += " ORDER BY a.AppointmentDate, a.AppointmentTime";
        cmd.CommandText = sql;

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Appointment
            {
                Id = reader.GetInt32(0),
                Date = reader.GetDateTime(1),     // тип DATE читается как DateTime
                Time = reader.GetTimeSpan(2),     // тип TIME читается как TimeSpan
                PetName = reader.GetString(3),
                AnimalType = reader.GetString(4),
                Breed = reader.GetString(5),
                OwnerName = reader.GetString(6),
                Phone = reader.GetString(7),
                ServiceName = reader.GetString(8),
                Price = reader.GetDecimal(9)
            });
        }
        return result;
    }

    // ---------- Проверка: занято ли это время ----------
    // В салоне один мастер, поэтому на одно время — одна запись.
    public static bool IsSlotBusy(DateTime date, TimeSpan time)
    {
        using var conn = OpenConnection();
        using var cmd = new SqlCommand(
            "SELECT COUNT(*) FROM Appointments WHERE AppointmentDate = @date AND AppointmentTime = @time", conn);

        // Параметры (@date, @time) защищают от SQL-инъекций и ошибок с кавычками
        cmd.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
        cmd.Parameters.Add("@time", SqlDbType.Time).Value = time;

        int count = (int)cmd.ExecuteScalar();   // ExecuteScalar возвращает одно значение
        return count > 0;
    }

    // ---------- Добавить новую запись ----------
    // Один метод заводит клиента, вид животного, животное и запись.
    // Если клиент или животное уже есть в базе — используются существующие.
    // Всё выполняется в ТРАНЗАКЦИИ: либо сохраняется всё, либо ничего.
    public static void AddAppointment(DateTime date, TimeSpan time,
                                      string ownerName, string phone,
                                      string petName, string animalType, string breed,
                                      int serviceId)
    {
        using var conn = OpenConnection();
        using var tran = conn.BeginTransaction();   // начало транзакции
        try
        {
            int clientId = GetOrCreateClient(conn, tran, ownerName, phone);
            int typeId = GetOrCreateAnimalType(conn, tran, animalType);
            int petId = GetOrCreatePet(conn, tran, clientId, typeId, petName, breed);

            using var cmd = new SqlCommand(@"
                INSERT INTO Appointments (PetId, ServiceId, AppointmentDate, AppointmentTime)
                VALUES (@pet, @service, @date, @time)", conn, tran);
            cmd.Parameters.AddWithValue("@pet", petId);
            cmd.Parameters.AddWithValue("@service", serviceId);
            cmd.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
            cmd.Parameters.Add("@time", SqlDbType.Time).Value = time;
            cmd.ExecuteNonQuery();   // выполняет команду без результата (INSERT/UPDATE/DELETE)

            tran.Commit();           // всё прошло успешно — сохраняем
        }
        catch
        {
            tran.Rollback();         // ошибка — отменяем все изменения
            throw;                   // и передаём ошибку выше, чтобы показать пользователю
        }
    }

    // ---------- Найти клиента по телефону или создать нового ----------
    private static int GetOrCreateClient(SqlConnection conn, SqlTransaction tran, string fullName, string phone)
    {
        using var find = new SqlCommand("SELECT Id FROM Clients WHERE Phone = @phone", conn, tran);
        find.Parameters.AddWithValue("@phone", phone);
        object found = find.ExecuteScalar();      // null, если клиента нет
        if (found != null)
            return (int)found;

        // OUTPUT INSERTED.Id — сразу возвращает номер только что добавленной строки
        using var insert = new SqlCommand(
            "INSERT INTO Clients (FullName, Phone) OUTPUT INSERTED.Id VALUES (@name, @phone)", conn, tran);
        insert.Parameters.AddWithValue("@name", fullName);
        insert.Parameters.AddWithValue("@phone", phone);
        return (int)insert.ExecuteScalar();
    }

    // ---------- Найти вид животного по названию или создать новый ----------
    private static int GetOrCreateAnimalType(SqlConnection conn, SqlTransaction tran, string name)
    {
        using var find = new SqlCommand("SELECT Id FROM AnimalTypes WHERE Name = @name", conn, tran);
        find.Parameters.AddWithValue("@name", name);
        object found = find.ExecuteScalar();
        if (found != null)
            return (int)found;

        using var insert = new SqlCommand(
            "INSERT INTO AnimalTypes (Name) OUTPUT INSERTED.Id VALUES (@name)", conn, tran);
        insert.Parameters.AddWithValue("@name", name);
        return (int)insert.ExecuteScalar();
    }

    // ---------- Найти животное клиента или создать новое ----------
    private static int GetOrCreatePet(SqlConnection conn, SqlTransaction tran,
                                      int clientId, int typeId, string petName, string breed)
    {
        using var find = new SqlCommand(@"
            SELECT Id FROM Pets
            WHERE ClientId = @client AND AnimalTypeId = @type AND Name = @name AND Breed = @breed", conn, tran);
        find.Parameters.AddWithValue("@client", clientId);
        find.Parameters.AddWithValue("@type", typeId);
        find.Parameters.AddWithValue("@name", petName);
        find.Parameters.AddWithValue("@breed", breed);
        object found = find.ExecuteScalar();
        if (found != null)
            return (int)found;

        using var insert = new SqlCommand(@"
            INSERT INTO Pets (ClientId, AnimalTypeId, Name, Breed)
            OUTPUT INSERTED.Id
            VALUES (@client, @type, @name, @breed)", conn, tran);
        insert.Parameters.AddWithValue("@client", clientId);
        insert.Parameters.AddWithValue("@type", typeId);
        insert.Parameters.AddWithValue("@name", petName);
        insert.Parameters.AddWithValue("@breed", breed);
        return (int)insert.ExecuteScalar();
    }

    // ---------- Удалить запись по Id ----------
    // Удаляется только запись журнала. Клиент и животное остаются в базе (вдруг придут снова).
    public static void DeleteAppointment(int id)
    {
        using var conn = OpenConnection();
        using var cmd = new SqlCommand("DELETE FROM Appointments WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
