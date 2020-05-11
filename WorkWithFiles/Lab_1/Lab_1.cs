/*
 * Лабораторная работа 1. Создание файла.
 * Написать программу, которая позволяет пользователю инициализировать двумерный массив и записать его в файл в формате таком, что:
 * 1. каждая строка массива соответствует строке в файле; числа разделяются пробелом.
 * 2. {1} {2} {n1 n2 n3 ...}, где {1} соответствует количеству строк массива,
 * {2} соответствует количеству столбцов массива, а {n1 n2 n3 ...} соответствуют значениям массива.
 */

using System;
using System.IO;
using System.Text;

namespace LabWorks.Lab_1
{
    public class Lab_1
    {
        private int Columns { get; set; }
        private int Lines { get; set; }
        private string[,] Array { get; set; }
        private string Path { get; set; } = "TextFile.txt";
        private bool Use2Variant { get; set; } = false;
        public void Start() // Метод входа в программу.
        {
            InitialArray();
            ChoiseOfMethodWritingArray();
        }

        void ChoiseOfMethodWritingArray()
        {
            bool correctly = false;
            while (!correctly)
            {
                Console.Write(new string('_', count: Console.BufferWidth) + 
                              "# МЕТОДЫ ЗАПИСИ МАССИВА В ФАЙЛ:\n" +
                              "# \"1\" - каждая строкEа массива соответствует строке в файле; числа разделяются пробелом.\n" +
                              "# \"2\" - {1} {2} {n1 n2 n3 ...}, где {1} соответствует количеству строк массива,\n" +
                              "#\t{2} соответствует количеству столбцов массива, а {n1 n2 n3 ...} соответствуют значениям массива.\n" +
                              "# \"3\" - выполнить оба метода записи.\n" +
                              new string('_', count: Console.BufferWidth) +
                              "\nВыберите метод записи массива в файл: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        correctly = true;
                        WritingArrayInFileV1();
                        break;
                    case "2":
                        correctly = true;
                        WritingArrayInFileV2();
                        break;
                    case "3":
                        Use2Variant = true;
                        correctly = true;
                        WritingArrayInFileV1();
                        WritingArrayInFileV2();
                        break;
                    default:
                        Console.WriteLine("Введено неверное значение метода! Повторите, пожалуйста, попытку.");
                        break;
                }
            }
        }
        
        void WritingArrayInFileV2() // 2 вариант записи массива в файл.
        {
            if (Use2Variant)
                Path = "2" + Path;
            try
            {
                using (var sw = new StreamWriter(Path, false, Encoding.UTF8))
                {
                    byte count = 0;
                    sw.Write($"[{Lines}] [{Columns}] ");
                    foreach (var value in Array)
                    {
                        sw.Write(value);
                        count++;
                        if (count < Array.Length) // Лишние пробелы в конце каждой строки нам ни к чему.
                            sw.Write(" ");
                    }
                }
                Console.WriteLine($"[2] Массив успешно записан в текстовый файл \"{Path}\"");
            }
            catch (Exception e)
            {
                Console.WriteLine("[Ошибка] "+ e.Message);
                throw;
            }
        }
        void WritingArrayInFileV1() // 1 вариант записи массива в файл.
        {
            try
            {
                using (var sw = new StreamWriter(Path, false, Encoding.UTF8))
                {
                    for (int i = 0; i < Array.GetLength(0); i++)
                    {
                        for (int j = 0; j < Array.GetLength(1); j++)
                        {
                            sw.Write(Array[i,j]);
                            if (j < Array.GetLength(1) - 1) // Лишние пробелы в конце каждой строки нам ни к чему.
                                sw.Write(" ");
                        }

                        if ( i < Array.GetLength(0) - 1) // Чтоб курсор не переносил на новую строку в конце.
                            sw.WriteLine();
                    }
                }
                Console.WriteLine($"[1] Массив успешно записан в текстовый файл \"{Path}\"");
            }
            catch (Exception e)
            {
                Console.WriteLine("[ОШИБКА] " + e.Message);
                throw;
            }
        }
        void InitialArray()
        {
            try
            {
                Console.Write("Введите количество столбцов массива: ");
                Columns = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите количество строк массива: ");
                Lines = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Array = new string[Columns,Lines];
                for (int i = 0; i < Array.GetLength(0); i++)
                {
                    for (int j = 0; j < Array.GetLength(1); j++)
                    {
                        Console.Write($"Введите значение элемента массива [{i}][{j}]: ");
                        Array[i, j] = Console.ReadLine();
                    }
                }
                Console.WriteLine("\nМассив успешно создан.\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("[ОШИБКА] " + e.Message);
                throw;
            }
        }
    }
}