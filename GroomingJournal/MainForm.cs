using System.Globalization;

namespace GroomingJournal;

// MainForm — главное окно. Наследуется от Form (стандартное окно Windows).
// Весь интерфейс создаём кодом в конструкторе, без визуального редактора —
// так видно каждую строку и её легко объяснить.
public class MainForm : Form
{
    // ----- Элементы интерфейса (поля класса, чтобы обращаться к ним из любых методов) -----
    private DateTimePicker dtpDate;      // выбор даты записи
    private ComboBox cmbTime;            // выбор времени
    private TextBox txtOwner;            // ФИО хозяина
    private TextBox txtPhone;            // телефон хозяина
    private TextBox txtPetName;          // кличка
    private ComboBox cmbAnimal;          // вид животного
    private TextBox txtBreed;            // порода
    private ComboBox cmbService;         // услуга
    private Label lblPrice;              // цена выбранной услуги
    private Button btnAdd;               // кнопка "Записать"
    private Button btnDelete;            // кнопка "Удалить"
    private CheckBox chkFilter;          // галочка "только за дату"
    private DateTimePicker dtpFilter;    // дата для фильтра
    private Label lblSummary;            // итог: сколько записей и на какую сумму
    private DataGridView grid;           // таблица-журнал

    // Русская культура — нужна, чтобы день недели был по-русски ("понедельник")
    private readonly CultureInfo ru = new CultureInfo("ru-RU");

    // ----- Конструктор: вызывается при создании окна -----
    public MainForm()
    {
        Text = "Журнал записей — груминг-салон";    // заголовок окна
        Size = new Size(1250, 700);                  // размер окна
        StartPosition = FormStartPosition.CenterScreen;  // открыть по центру экрана

        // ----- Минимальное оформление окна -----
        Font = new Font("Segoe UI", 10F);               // шрифт для всех элементов окна
        BackColor = Color.FromArgb(245, 246, 250);      // светло-серый фон

        BuildInterface();    // 1. создаём элементы
        LoadServices();      // 2. заполняем список услуг из базы
        LoadAnimalTypes();   // 3. заполняем список видов животных из базы
        RefreshGrid();       // 4. показываем записи из базы в таблице
    }

    // ===================== Построение интерфейса =====================
    private void BuildInterface()
    {
        // ---------- Таблица (занимает всё свободное место) ----------
        grid = new DataGridView
        {
            Dock = DockStyle.Fill,                       // растянуть на всё оставшееся место
            ReadOnly = true,                             // нельзя править ячейки руками
            AllowUserToAddRows = false,                  // убрать пустую строку для ввода внизу
            SelectionMode = DataGridViewSelectionMode.FullRowSelect, // выделяется вся строка
            MultiSelect = false,                         // только одна строка за раз
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, // столбцы делят ширину
            RowHeadersVisible = false,                   // убрать левый служебный столбец
            BackgroundColor = Color.White
        };
        // Добавляем столбцы: (имя столбца, заголовок)
        grid.Columns.Add("colDate", "Дата");
        grid.Columns.Add("colDay", "День недели");
        grid.Columns.Add("colTime", "Время");
        grid.Columns.Add("colPet", "Кличка");
        grid.Columns.Add("colType", "Животное");
        grid.Columns.Add("colBreed", "Порода");
        grid.Columns.Add("colOwner", "Хозяин");
        grid.Columns.Add("colPhone", "Телефон");
        grid.Columns.Add("colService", "Услуга");
        grid.Columns.Add("colPrice", "Цена");

        // Оформление таблицы
        grid.BorderStyle = BorderStyle.None;                              // без рамки вокруг таблицы
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // только горизонтальные линии
        grid.GridColor = Color.FromArgb(225, 228, 232);
        grid.RowTemplate.Height = 30;                                     // выше строки — легче читать
        grid.EnableHeadersVisualStyles = false;                           // разрешаем свои цвета заголовков
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersHeight = 38;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);   // тёмно-синий заголовок
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 248); // чередование строк
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);     // цвет выбранной строки
        grid.DefaultCellStyle.SelectionForeColor = Color.White;

        // Удаление выбранной записи клавишей Delete
        grid.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Delete)
                BtnDelete_Click(s, e);
        };

        // ---------- Панель фильтра (под формой ввода) ----------
        var filterPanel = new Panel { Dock = DockStyle.Top, Height = 40 };

        chkFilter = new CheckBox { Text = "Показать только за дату:", Left = 15, Top = 10, Width = 180 };
        dtpFilter = new DateTimePicker { Left = 200, Top = 8, Width = 130, Format = DateTimePickerFormat.Short };
        lblSummary = new Label { Left = 360, Top = 12, Width = 280, Font = new Font(Font, FontStyle.Bold) };

        // Подписываемся на события: при изменении фильтра перерисовываем таблицу
        chkFilter.CheckedChanged += (s, e) => RefreshGrid();
        dtpFilter.ValueChanged += (s, e) => RefreshGrid();

        btnDelete = new Button
        {
            Text = "Удалить запись",
            Left = 660, Top = 4, Width = 150, Height = 32   // фиксированное место справа от итога
        };
        btnDelete.Click += BtnDelete_Click;
        StyleButton(btnDelete, Color.FromArgb(192, 57, 43));   // красная кнопка

        filterPanel.Controls.Add(chkFilter);
        filterPanel.Controls.Add(dtpFilter);
        filterPanel.Controls.Add(lblSummary);
        filterPanel.Controls.Add(btnDelete);

        // ---------- Форма добавления новой записи ----------
        var box = new GroupBox { Text = "Новая запись", Dock = DockStyle.Top, Height = 205 };

        // Первая строка: дата, время, хозяин, телефон
        AddLabel(box, "Дата:", 15, 22);
        dtpDate = new DateTimePicker { Left = 15, Top = 42, Width = 130, Format = DateTimePickerFormat.Short };

        AddLabel(box, "Время:", 160, 22);
        cmbTime = new ComboBox { Left = 160, Top = 42, Width = 90, DropDownStyle = ComboBoxStyle.DropDownList };
        // Заполняем время с 9:00 до 19:30 с шагом 30 минут
        for (int hour = 9; hour <= 19; hour++)
        {
            cmbTime.Items.Add($"{hour:00}:00");   // {hour:00} — число из двух цифр: 09, 10...
            cmbTime.Items.Add($"{hour:00}:30");
        }
        cmbTime.SelectedIndex = 0;                // по умолчанию выбрано 09:00

        AddLabel(box, "Хозяин (ФИО):", 265, 22);
        txtOwner = new TextBox { Left = 265, Top = 42, Width = 250 };

        AddLabel(box, "Телефон:", 530, 22);
        txtPhone = new TextBox { Left = 530, Top = 42, Width = 150 };

        // Вторая строка: кличка, вид, порода
        AddLabel(box, "Кличка:", 15, 82);
        txtPetName = new TextBox { Left = 15, Top = 102, Width = 150 };

        AddLabel(box, "Животное:", 180, 82);
        // DropDown (а не DropDownList) — можно выбрать из списка ИЛИ вписать новый вид
        cmbAnimal = new ComboBox { Left = 180, Top = 102, Width = 140, DropDownStyle = ComboBoxStyle.DropDown };

        AddLabel(box, "Порода:", 335, 82);
        txtBreed = new TextBox { Left = 335, Top = 102, Width = 180 };

        // Третья строка: услуга, цена, кнопка
        AddLabel(box, "Услуга:", 15, 142);
        cmbService = new ComboBox { Left = 15, Top = 162, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
        cmbService.SelectedIndexChanged += (s, e) => UpdatePriceLabel();

        lblPrice = new Label { Left = 335, Top = 166, Width = 170, Font = new Font(Font, FontStyle.Bold) };

        btnAdd = new Button { Text = "Записать", Left = 530, Top = 158, Width = 120, Height = 30 };
        btnAdd.Click += BtnAdd_Click;
        StyleButton(btnAdd, Color.FromArgb(39, 174, 96));      // зелёная кнопка

        // Кладём все элементы внутрь рамки
        box.Controls.AddRange(new Control[]
        {
            dtpDate, cmbTime, txtOwner, txtPhone,
            txtPetName, cmbAnimal, txtBreed,
            cmbService, lblPrice, btnAdd
        });

        // ВАЖНО: порядок добавления в окно. Элементы с Dock=Top "прижимаются" в порядке
        // добавления снизу вверх: последний добавленный окажется самым верхним.
        // Поэтому сначала таблица, потом фильтр, потом форма ввода.
        Controls.Add(grid);
        Controls.Add(filterPanel);
        Controls.Add(box);
    }

    // Вспомогательный метод: единое оформление кнопок (плоская, цветная, белый текст)
    private void StyleButton(Button button, Color color)
    {
        button.FlatStyle = FlatStyle.Flat;            // плоский вид без объёма
        button.FlatAppearance.BorderSize = 0;         // убираем рамку
        button.BackColor = color;
        button.ForeColor = Color.White;
        button.Font = new Font(Font, FontStyle.Bold);
        button.Cursor = Cursors.Hand;                 // курсор-рука при наведении
    }

    // Вспомогательный метод: создаёт подпись (Label) и кладёт в родителя
    private void AddLabel(Control parent, string text, int x, int y)
    {
        parent.Controls.Add(new Label { Text = text, Left = x, Top = y, AutoSize = true });
    }

    // ===================== Загрузка справочников из базы =====================
    private void LoadServices()
    {
        cmbService.Items.Clear();
        foreach (Service s in Database.GetServices())
        {
            cmbService.Items.Add(s);   // в список кладём сам объект; текст берётся из ToString()
        }
        if (cmbService.Items.Count > 0)
            cmbService.SelectedIndex = 0;
    }

    private void LoadAnimalTypes()
    {
        cmbAnimal.Items.Clear();
        cmbAnimal.Items.AddRange(Database.GetAnimalTypes().ToArray());
    }

    // Показываем цену выбранной услуги рядом со списком
    private void UpdatePriceLabel()
    {
        // "is Service s" — проверяем, что выбран объект Service, и сразу кладём его в переменную s
        if (cmbService.SelectedItem is Service s)
            lblPrice.Text = $"Цена: {s.Price:0.##} ₽";
        else
            lblPrice.Text = "";
    }

    // ===================== Обновление таблицы =====================
    private void RefreshGrid()
    {
        grid.Rows.Clear();   // стираем старые строки

        // Если галочка фильтра стоит — берём дату из фильтра, иначе null (все записи)
        DateTime? filterDate = chkFilter.Checked ? dtpFilter.Value.Date : (DateTime?)null;

        List<Appointment> list = Database.GetAppointments(filterDate);
        decimal total = 0;

        foreach (Appointment a in list)
        {
            // День недели по-русски: "воскресенье" -> "Воскресенье"
            string day = ru.DateTimeFormat.GetDayName(a.Date.DayOfWeek);
            day = char.ToUpper(day[0]) + day.Substring(1);

            // Rows.Add возвращает номер добавленной строки
            int index = grid.Rows.Add(
                a.Date.ToString("dd.MM.yyyy"),
                day,
                a.Time.ToString(@"hh\:mm"),     // TimeSpan -> "09:30"
                a.PetName,
                a.AnimalType,
                a.Breed,
                a.OwnerName,
                a.Phone,
                a.ServiceName,
                a.Price.ToString("0.##") + " ₽");

            // Tag — "ярлык" строки, прячем в нём Id записи, чтобы потом удалить из базы
            grid.Rows[index].Tag = a.Id;
            total += a.Price;
        }

        lblSummary.Text = $"Записей: {list.Count}    Сумма: {total:0.##} ₽";
    }

    // ===================== Кнопка "Записать" =====================
    private void BtnAdd_Click(object sender, EventArgs e)
    {
        // 1. Читаем и проверяем введённые данные.
        // Trim() убирает пробелы по краям, чтобы нельзя было ввести "   ".
        string owner = txtOwner.Text.Trim();
        string phone = txtPhone.Text.Trim();
        string petName = txtPetName.Text.Trim();
        string animal = cmbAnimal.Text.Trim();
        string breed = txtBreed.Text.Trim();

        if (owner == "")
        {
            ShowWarning("Введите ФИО хозяина.");
            return;   // выходим из метода — запись не создаётся
        }
        if (phone == "")
        {
            ShowWarning("Введите телефон хозяина.");
            return;
        }
        if (petName == "")
        {
            ShowWarning("Введите кличку животного.");
            return;
        }
        if (animal == "")
        {
            ShowWarning("Укажите, какое это животное.");
            return;
        }
        if (cmbService.SelectedItem == null)
        {
            ShowWarning("Выберите услугу.");
            return;
        }
        if (breed == "")
            breed = "не указана";   // порода необязательна (например, беспородное животное)

        DateTime date = dtpDate.Value.Date;                        // .Date отбрасывает время, оставляет дату
        TimeSpan time = TimeSpan.Parse(cmbTime.SelectedItem.ToString());  // "14:30" -> TimeSpan
        var service = (Service)cmbService.SelectedItem;            // приводим выбранный элемент к типу Service

        try
        {
            // 2. Проверяем, что время не занято
            if (Database.IsSlotBusy(date, time))
            {
                ShowWarning("На это время уже есть запись. Выберите другое время.");
                return;
            }

            // 3. Сохраняем в базу (клиент, животное и запись — одной транзакцией)
            Database.AddAppointment(date, time, owner, phone, petName, animal, breed, service.Id);
        }
        catch (Exception ex)
        {
            // Любая ошибка базы данных не должна "ронять" программу — показываем сообщение
            MessageBox.Show("Не удалось сохранить запись:\n" + ex.Message, "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // 4. Очищаем поля и обновляем данные на экране
        txtOwner.Clear();
        txtPhone.Clear();
        txtPetName.Clear();
        txtBreed.Clear();
        cmbAnimal.Text = "";
        LoadAnimalTypes();   // вдруг добавлен новый вид животного — обновим список
        RefreshGrid();
    }

    // Показать предупреждение (чтобы не повторять длинную строку MessageBox.Show)
    private void ShowWarning(string text)
    {
        MessageBox.Show(text, "Проверьте данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    // ===================== Кнопка "Удалить" =====================
    private void BtnDelete_Click(object sender, EventArgs e)
    {
        // CurrentRow — выбранная в таблице строка (null, если ничего не выбрано)
        if (grid.CurrentRow == null)
        {
            MessageBox.Show("Выберите запись в таблице.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Спрашиваем подтверждение
        var answer = MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (answer != DialogResult.Yes) return;

        int id = (int)grid.CurrentRow.Tag;   // достаём спрятанный Id и приводим к int
        Database.DeleteAppointment(id);
        RefreshGrid();
    }
}
