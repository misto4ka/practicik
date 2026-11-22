using Microsoft.Data.SqlClient;

namespace PracticeDb.Cli;

/// <summary>Задание 30: мини-отчёт по отделам.</summary>
public static class Task30
{
    private sealed record Row(string Department, int Count, decimal? Avg);

    public static async Task RunAsync(string connectionString)
    {
        const string sql = @"
SELECT d.DepartmentName, COUNT(e.EmployeeId) AS EmpCount, AVG(e.Salary) AS AvgSalary
FROM Departments d
LEFT JOIN Employees e ON d.DepartmentId = e.DepartmentId
GROUP BY d.DepartmentName
ORDER BY d.DepartmentName;";

        var rows = new List<Row>();
        try
        {
            await using var connection = await Db.OpenAsync(connectionString);
            await using var command = new SqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                rows.Add(new Row(
                    reader.GetString(0),
                    reader.GetInt32(1),
                    reader.IsDBNull(2) ? null : reader.GetDecimal(2)));
            }
        }
        catch (SqlException ex)
        {
            Db.PrintSqlError("Task 30", ex);
            return;
        }

        if (rows.Count == 0)
        {
            Console.WriteLine("Отделов в базе нет.");
            return;
        }

        const string h1 = "Отдел", h2 = "Сотрудников", h3 = "Средняя зарплата";
        var avgTexts = rows.Select(r => r.Avg is null ? "N/A" : r.Avg.Value.ToString("F2")).ToList();

        var w1 = Math.Max(h1.Length, rows.Max(r => r.Department.Length));
        var w2 = Math.Max(h2.Length, rows.Max(r => r.Count.ToString().Length));
        var w3 = Math.Max(h3.Length, avgTexts.Max(t => t.Length));

        Console.WriteLine($"{h1.PadRight(w1)} | {h2.PadLeft(w2)} | {h3.PadLeft(w3)}");
        Console.WriteLine($"{new string('-', w1)}-+-{new string('-', w2)}-+-{new string('-', w3)}");
        for (var i = 0; i < rows.Count; i++)
        {
            Console.WriteLine(
                $"{rows[i].Department.PadRight(w1)} | {rows[i].Count.ToString().PadLeft(w2)} | {avgTexts[i].PadLeft(w3)}");
        }
    }
}
