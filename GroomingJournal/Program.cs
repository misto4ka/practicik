namespace GroomingJournal;

// Program — точка входа: с метода Main начинается выполнение программы.
static class Program
{
    // [STAThread] нужен для Windows Forms (однопоточная модель для окон).
    [STAThread]
    static void Main()
    {
        // Настройка стандартных параметров Windows Forms (шрифт, масштаб и т.д.)
        ApplicationConfiguration.Initialize();

        // Перед показом окна создаём базу (если её ещё нет) и проверяем подключение.
        try
        {
            Database.Initialize();       // при первом запуске выполняет Database.sql
            Database.CheckConnection();  // убеждаемся, что к базе можно подключиться
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Не удалось подключиться к базе данных.\n" +
                "1) Установлен ли SQL Server LocalDB? (проверка: команда sqllocaldb info)\n" +
                "2) Лежит ли файл Database.sql рядом с программой?\n" +
                "3) Верна ли строка подключения в Database.cs?\n\n" +
                "Подробности: " + ex.Message,
                "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;   // выходим — без базы программа работать не может
        }

        // Запускаем главное окно. Программа работает, пока окно открыто.
        Application.Run(new MainForm());
    }
}
