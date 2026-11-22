using Microsoft.Data.SqlClient;

namespace PracticeDb.Cli;

/// <summary>Строка подключения берётся только из переменной окружения.</summary>
public static class Db
{
    public const string EnvVarName = "PRACTICE_DB_CONNECTION";

    public static string? GetConnectionString()
    {
        var value = Environment.GetEnvironmentVariable(EnvVarName);
        return string.IsNullOrWhiteSpace(value) ? null : value;
    }

    public static async Task<SqlConnection> OpenAsync(string connectionString)
    {
        var connection = new SqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            return connection;
        }
        catch
        {
            await connection.DisposeAsync();
            throw;
        }
    }

    public static void PrintSqlError(string context, SqlException ex)
    {
        Console.Error.WriteLine($"[{context}] Ошибка SQL Server №{ex.Number}: {ex.Message}");
    }
}
