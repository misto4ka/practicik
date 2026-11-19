using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace CSharpPracticum.Menu
{
    public class MenuRunner
    {
        public static void Run()
        {
            Console.WriteLine("=== Практикум по C# ===");
            Console.WriteLine("1. Уровень 1 - Базовый (задания 1-15)");
            Console.WriteLine("2. Уровень 2 - Средний (задания 16-35)");
            Console.WriteLine("3. Уровень 3 - Продвинутый (задания 36-50)");
            Console.WriteLine("4. Выход\n");

            Console.Write("Выберите уровень (1-4): ");
            var lvlchoice = (Console.ReadLine());

            if (Convert.ToInt32(lvlchoice) == 4 || Convert.ToInt32(lvlchoice) == 0 || Convert.ToString(lvlchoice) == "q")
            {
                Console.Clear();
                Console.WriteLine("Выход из программы");
                Environment.Exit(0);
            }

            else if (Convert.ToInt32(lvlchoice) == 1 || Convert.ToInt32(lvlchoice) == 2 || Convert.ToInt32(lvlchoice) == 3)
            {
                Console.Clear();
                LevelMenu LevelMenu = new LevelMenu();
                LevelMenu.Show(int.Parse(lvlchoice));
            }

            else if (Convert.ToInt32(lvlchoice) > 4)
            {
                Console.Clear();
                Console.WriteLine("Неверный ввод, введите от 1 до 4\n");
                MenuRunner.Run();
            }

            else
            {
                Console.Clear();
                Console.WriteLine("Неверный ввод, введите число от 1 до 4\n");
                MenuRunner.Run();
            }
        }
    }
}
