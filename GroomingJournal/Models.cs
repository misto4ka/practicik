namespace GroomingJournal;

// Модель "Услуга" — одна строка таблицы Services.
public class Service
{
    public int Id { get; set; }          // уникальный номер услуги
    public string Name { get; set; }     // название, например "Стрижка"
    public decimal Price { get; set; }   // цена в рублях

    // ToString определяет, как объект выглядит в выпадающем списке (ComboBox).
    public override string ToString()
    {
        return $"{Name} — {Price:0.##} ₽";
    }
}

// Модель "Запись журнала" — одна строка итоговой таблицы на экране.
// Данные собираются из пяти таблиц сразу (запрос с JOIN).
public class Appointment
{
    public int Id { get; set; }              // номер записи (Appointments.Id)
    public DateTime Date { get; set; }       // дата визита
    public TimeSpan Time { get; set; }       // время визита (в SQL тип TIME = в C# TimeSpan)
    public string PetName { get; set; }      // кличка (Pets.Name)
    public string AnimalType { get; set; }   // вид животного (AnimalTypes.Name)
    public string Breed { get; set; }        // порода (Pets.Breed)
    public string OwnerName { get; set; }    // ФИО хозяина (Clients.FullName)
    public string Phone { get; set; }        // телефон хозяина (Clients.Phone)
    public string ServiceName { get; set; }  // название услуги (Services.Name)
    public decimal Price { get; set; }       // цена услуги (Services.Price)
}
