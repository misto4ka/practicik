using System.Text;
using PracticeDb.Cli;

Console.OutputEncoding = Encoding.UTF8;

var connectionString = Db.GetConnectionString();
if (connectionString is null)
{
    Console.Error.WriteLine($"Не задана переменная окружения {Db.EnvVarName}.");
    Console.Error.WriteLine(
        "Пример (PowerShell): $env:PRACTICE_DB_CONNECTION = " +
        "\"Server=localhost\\SQLEXPRESS;Database=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True;\"");
    return 1;
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== PracticeDB: задания C# + SQL ===");
    Console.WriteLine("28 — подключение и SELECT");
    Console.WriteLine("29 — параметризованный INSERT");
    Console.WriteLine("30 — отчёт по отделам");
    Console.WriteLine("0  — выход");
    Console.Write("Выбор: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "28": await Task28.RunAsync(connectionString); break;
        case "29": await Task29.RunAsync(connectionString); break;
        case "30": await Task30.RunAsync(connectionString); break;
        case "0":  return 0;
        default:   Console.WriteLine("Неизвестный пункт меню."); break;
    }
}
