using Microsoft.Data.SqlClient;

namespace PracticeDb.Cli;

/// <summary>Задание 28: подключение и SELECT.</summary>
public static class Task28
{
    public static async Task RunAsync(string connectionString)
    {
        try
        {
            await using var connection = await Db.OpenAsync(connectionString);

            const string sql = "SELECT EmployeeName, Salary FROM Employees ORDER BY EmployeeId";
            await using var command = new SqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                Console.WriteLine("В таблице Employees нет данных.");
                return;
            }

            Console.WriteLine($"{"Сотрудник",-25}{"Зарплата",12}");
            while (await reader.ReadAsync())
            {
                var name = reader.GetString(0);
                var salary = reader.GetDecimal(1);
                Console.WriteLine($"{name,-25}{salary,12:F2}");
            }
        }
        catch (SqlException ex)
        {
            Db.PrintSqlError("Task 28", ex);
        }
    }
}
