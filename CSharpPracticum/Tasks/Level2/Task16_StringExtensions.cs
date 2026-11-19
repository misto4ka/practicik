using CSharpPracticum.Common;
using CSharpPracticum.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPracticum.Tasks.Level2
{
    internal class Task16_StringExtensions : ITask
    {
        public string Title => "==================================================\n Задание 16. Методы расширения для string\n--------------------------------------------------\n";
        public string Description => "Описание:\r\n  Написать extension-методы для string.\n";
        public string[] Requirements => new string[] { "Требования:\r\n - Реализовать в статическом классе StringExtensions.\r\n - Методы: IsPalindrome(), WordCount(), ReverseString().\r\n - Методы должны быть public static с this string первым параметром.\r\n - Продемонстрировать вызов на 2–3 строках.\r\n" };

        public void Run()
        {
            Console.WriteLine(Title);
            Console.WriteLine(Description);
            Console.WriteLine(Requirements[0]);
            Console.WriteLine("--------------------------------------------------\n");

            string str1 = "abc";
            string str2 = "А роза упала на лапу Азора";
            string str3 = "C# — отличный язык программирования!";

            Console.WriteLine($"Строка: \"{str1}\"");
            Console.WriteLine($"  Реверс: \"{str1.ReverseString()}\"");
            Console.WriteLine($"  Палиндром: {str1.IsPalindrome()}");
            Console.WriteLine($"  Слов: {str1.WordCount()}\n");

            Console.WriteLine($"Строка: \"{str2}\"");
            Console.WriteLine($"  Реверс: \"{str2.ReverseString()}\"");
            Console.WriteLine($"  Палиндром: {str2.IsPalindrome()}");
            Console.WriteLine($"  Слов: {str2.WordCount()}\n");

            Console.WriteLine($"Строка: \"{str3}\"");
            Console.WriteLine($"  Реверс: \"{str3.ReverseString()}\"");
            Console.WriteLine($"  Палиндром: {str3.IsPalindrome()}");
            Console.WriteLine($"  Слов: {str3.WordCount()}");


            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            Console.Clear();
            LevelMenu.Show(2);
        }
    }

        public static class StringExtensions
        {
            public static bool IsPalindrome(this string text)
            {
                if (string.IsNullOrEmpty(text))
                {
                    return true;
                }

                string cleaned = "";
                for (int i = 0; i < text.Length; i++)
                {
                    char c = char.ToLower(text[i]);
                    if (char.IsLetterOrDigit(c))
                    {
                        cleaned += c;
                    }
                }

                int left = 0;
                int right = cleaned.Length - 1;

                while (left < right)
                {
                    if (cleaned[left] != cleaned[right])
                    {
                        return false;
                    }
                    left++;
                    right--;
                }

                return true;
            }

            public static int WordCount(this string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return 0;
                }

                char[] separators = new char[]
                {
            ' ', '\t', '\n', '\r', '.', ',', '!', '?', ':', ';', '-', '—', '"', '\''
                };

                string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return words.Length;
            }

            public static string ReverseString(this string text)
            {
                if (string.IsNullOrEmpty(text))
                {
                    return text;
                }

                char[] chars = new char[text.Length];
                for (int i = 0; i < text.Length; i++)
                {
                    chars[i] = text[text.Length - 1 - i];
                }

                return new string(chars);
            }
        }

}

