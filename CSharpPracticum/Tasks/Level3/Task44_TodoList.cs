using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level3
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }

    public class TaskRepository
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions { WriteIndented = true };

        private readonly string _filePath;
        private List<TaskItem> _items = new List<TaskItem>();

        public TaskRepository(string filePath)
        {
            _filePath = filePath;
            Load();
        }

        public IReadOnlyList<TaskItem> GetAll()
        {
            return _items.AsReadOnly();
        }

        public TaskItem GetById(int id)
        {
            return _items.FirstOrDefault(t => t.Id == id);
        }

        public TaskItem Add(string title)
        {
            int id = _items.Count == 0 ? 1 : _items.Max(t => t.Id) + 1;
            TaskItem item = new TaskItem { Id = id, Title = title, IsDone = false };
            _items.Add(item);
            Save();
            return item;
        }

        public bool Update(TaskItem item)
        {
            int index = _items.FindIndex(t => t.Id == item.Id);
            if (index < 0) return false;

            _items[index] = item;
            Save();
            return true;
        }

        public bool Remove(int id)
        {
            TaskItem item = GetById(id);
            if (item == null) return false;

            _items.Remove(item);
            Save();
            return true;
        }

        private void Load()
        {
            if (!File.Exists(_filePath))
            {
                _items = new List<TaskItem>();
                return;
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                _items = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch (JsonException)
            {
                Console.WriteLine("Файл tasks.json повреждён. Начинаем с пустого списка.");
                _items = new List<TaskItem>();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Не удалось прочитать файл: {ex.Message}");
                _items = new List<TaskItem>();
            }
        }

        private void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(_items, JsonOptions);
                File.WriteAllText(_filePath, json);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Не удалось сохранить файл: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет доступа к файлу: {ex.Message}");
            }
        }
    }

    public class TaskService
    {
        private readonly TaskRepository _repository;

        public TaskService(TaskRepository repository)
        {
            _repository = repository;
        }

        public TaskItem AddTask(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название задачи не может быть пустым.");

            return _repository.Add(title.Trim());
        }

        public IReadOnlyList<TaskItem> GetAllTasks()
        {
            return _repository.GetAll();
        }

        public bool MarkDone(int id)
        {
            TaskItem item = _repository.GetById(id);
            if (item == null) return false;

            item.IsDone = true;
            return _repository.Update(item);
        }

        public bool DeleteTask(int id)
        {
            return _repository.Remove(id);
        }
    }

    public class Task44_TodoList : ITask
    {
        public string Title => "==================================================\r\nЗадание 44. Мини-проект «Todo List» (классы + JSON)\r\n--------------------------------------------------";
        public string Description => "Описание: консольное приложение для списка задач с сохранением в JSON. ";
        public string[] Requirements => new string[] { "Требования:\r\n\r\nКлассы: TaskItem (Id, Title, IsDone), TaskRepository (CRUD), TaskService (бизнес-логика).\r\nCRUD-операции: добавить, вывести все, отметить выполненной, удалить.\r\nСохранение/загрузка через System.Text.Json (файл tasks.json).\r\nМеню с выбором действий.\r\nОбработка ошибок (некорректный ввод, отсутствующий файл)." };

        private static void AddTaskFlow(TaskService service)
        {
            Console.Write("Название задачи: ");
            string title = Console.ReadLine();

            try
            {
                TaskItem item = service.AddTask(title);
                Console.WriteLine($"Задача добавлена (Id = {item.Id}).");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void ShowAllFlow(TaskService service)
        {
            IReadOnlyList<TaskItem> tasks = service.GetAllTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст.");
                return;
            }

            foreach (TaskItem item in tasks)
            {
                string mark = item.IsDone ? "[x]" : "[ ]";
                Console.WriteLine($"{item.Id,3}. {mark} {item.Title}");
            }
        }

        private static bool ReadId(string prompt, out int id)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out id))
                return true;

            Console.WriteLine("Ошибка: нужно ввести целое число.");
            return false;
        }

        private static void MarkDoneFlow(TaskService service)
        {
            if (!ReadId("Id задачи, которую нужно отметить выполненной: ", out int id)) return;

            Console.WriteLine(service.MarkDone(id) ? "Задача отмечена выполненной." : "Задача с таким Id не найдена.");
        }

        private static void DeleteFlow(TaskService service)
        {
            if (!ReadId("Id задачи для удаления: ", out int id)) return;

            Console.WriteLine(service.DeleteTask(id) ? "Задача удалена." : "Задача с таким Id не найдена.");
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(string.Join("\n", Requirements));
            Console.WriteLine(new string('-', 30));
            Console.WriteLine();

            TaskService service = new TaskService(new TaskRepository("tasks.json"));
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Показать все задачи");
                Console.WriteLine("3. Отметить задачу выполненной");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddTaskFlow(service);
                        break;
                    case "2":
                        ShowAllFlow(service);
                        break;
                    case "3":
                        MarkDoneFlow(service);
                        break;
                    case "4":
                        DeleteFlow(service);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестный пункт меню.");
                        break;
                }
            }

            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }
    }
}
