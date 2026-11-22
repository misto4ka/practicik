using System.Data;
using Microsoft.Data.SqlClient;

namespace PracticeDb.Cli;

/// <summary>Задание 29: параметризованный INSERT.</summary>
public static class Task29
{
    private const int ForeignKeyViolation = 547;
    private const int DuplicateKey = 2627;
    private const int DuplicateIndex = 2601;

    /// <summary>
    /// Добавляет сотрудника и возвращает его EmployeeId (или null при ошибке).
    /// Значения передаются только параметрами, конкатенации нет.
    /// HireDate в сигнатуре нет, поэтому берётся текущая дата на стороне SQL Server.
    /// </summary>
    public static async Task<int?> AddEmployeeAsync(
        string connectionString, string name, decimal salary, int departmentId)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
        {
            Console.Error.WriteLine("Имя не должно быть пустым и длиннее 100 символов.");
            return null;
        }
        if (salary <= 0)
        {
            Console.Error.WriteLine("Зарплата должна быть больше нуля.");
            return null;
        }

        const string sql = @"
INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId, ManagerId)
OUTPUT INSERTED.EmployeeId
VALUES (@name, @salary, CAST(GETDATE() AS DATE), @deptId, NULL);";

        try
        {
            await using var connection = await Db.OpenAsync(connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
            command.Parameters.Add(new SqlParameter("@salary", SqlDbType.Decimal)
            {
                Precision = 10,
                Scale = 2,
                Value = salary
            });
            command.Parameters.Add("@deptId", SqlDbType.Int).Value = departmentId;

            var newId = (int)(await command.ExecuteScalarAsync())!;
            await PrintEmployeeAsync(connection, newId);
            return newId;
        }
        catch (SqlException ex) when (ex.Number == ForeignKeyViolation)
        {
            Console.Error.WriteLine($"Отдела с Id = {departmentId} не существует (нарушение внешнего ключа).");
            return null;
        }
        catch (SqlException ex) when (ex.Number is DuplicateKey or DuplicateIndex)
        {
            Console.Error.WriteLine("Такая запись уже существует (дубликат ключа).");
            return null;
        }
        catch (SqlException ex)
        {
            Db.PrintSqlError("Task 29", ex);
            return null;
        }
    }

    private static async Task PrintEmployeeAsync(SqlConnection connection, int employeeId)
    {
        const string sql =
            "SELECT EmployeeId, EmployeeName, Salary, DepartmentId FROM Employees WHERE EmployeeId = @id";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@id", SqlDbType.Int).Value = employeeId;
        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            Console.WriteLine(
                $"Добавлен: Id={reader.GetInt32(0)}, {reader.GetString(1)}, " +
                $"зарплата {reader.GetDecimal(2):F2}, отдел {reader.GetInt32(3)}");
        }
        else
        {
            Console.WriteLine("Запись после вставки не найдена.");
        }
    }

    public static async Task RunAsync(string connectionString)
    {
        Console.WriteLine("1) Корректная вставка:");
        await AddEmployeeAsync(connectionString, "Тестовый Сотрудник", 50000m, 1);

        Console.WriteLine();
        Console.WriteLine("2) Несуществующий отдел (ожидается ошибка внешнего ключа):");
        await AddEmployeeAsync(connectionString, "Ошибка FK", 50000m, 999);

        Console.WriteLine();
        Console.WriteLine("3) Попытка SQL-инъекции в имени: строка сохраняется как обычный текст:");
        await AddEmployeeAsync(connectionString, "Robert'); DROP TABLE Employees;--", 40000m, 1);

        Console.WriteLine();
        Console.WriteLine("Чтобы убрать тестовые строки, выполните init.sql заново.");
    }
}
