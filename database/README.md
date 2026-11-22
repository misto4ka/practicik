# database

Практикум по БД (Microsoft SQL Server).

- `init.sql` — пересоздаёт `PracticeDB` с нуля (схема и тестовые данные).
- `sql-notes.md` — запросы, результаты и выводы по заданиям 1–27.
- `csharp/PracticeDb.Cli` — задания 28–30 (консольное приложение, `Microsoft.Data.SqlClient`).

## Запуск C#-части

1. Выполните `init.sql` в SSMS или Azure Data Studio.
2. Задайте строку подключения в переменной окружения (в репозиторий она не попадает):

   ```powershell
   $env:PRACTICE_DB_CONNECTION = "Server=localhost\SQLEXPRESS;Database=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True;"
   ```

3. Запустите:

   ```powershell
   cd csharp/PracticeDb.Cli
   dotnet run
   ```

`TrustServerCertificate=True` нужен для локального SQL Server без доверенного сертификата. Имя сервера подставьте своё.
