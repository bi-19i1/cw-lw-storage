/*
 * Лабораторная работа 2. Чтение из файла.
 * Пусть дан файл, содержащий двумерный массив чисел. Структура файла определена пользователем.
 * Задание 1. Из прочитанного в файле массива, найти самое большое число и вывести его на экран.
 * Задание 2. Из прочитанного в файле массива, поменять значения элементов четных столбцов и строк на символ "*".
 * Записать измененный массив в другой файл.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LabWorks.Lab_2
{
    public class Lab_2
    {
        string Path = "file.txt";
        string[,] Array;
        private string PathFromNewFile = "NewFile.txt";

        public void Start()
        {
            ReadArrayFromFile();
            FindBigNumber();
            ChangeElements();
            WriteArrayInFile();
            Console.ReadKey();
        }

        void WriteArrayInFile()
        {
            try
            {
                using (var sw = new StreamWriter(PathFromNewFile, false, Encoding.UTF8))
                {
                    for (int i = 0; i < Array.GetLength(0); i++)
                    {
                        for (int j = 0; j < Array.GetLength(1); j++)
                        {
                            sw.Write(Array[i, j]);
                            if (j < Array.GetLength(1) - 1)
                                sw.Write(" ");
                        }

                        if (i < Array.GetLength(0) - 1)
                            sw.WriteLine();
                    }
                }
                Console.WriteLine($"Файл с измененным массивом успешно создан \"{PathFromNewFile}\"");
            }
            catch (Exception e)
            {
                Console.WriteLine("[ОШИБКА] " + e.Message);
                throw;
            }
        }
        void ChangeElements()
        {
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    if ((i+1) % 2 == 0 || (j+1) % 2 == 0)
                        Array[i, j] = "*";
                }
            }
        }
        void FindBigNumber()
        {
            int result = Convert.ToInt32(Array[0, 0]);
            foreach (var number in Array)
            {
                if (Convert.ToInt32(number) > result)
                {
                    result = Convert.ToInt32(number);
                }
            }
            Console.WriteLine("Самое большое число из файла: " + result);
        }
        void ReadArrayFromFile()
        {
            try
            {
                using (var sr = new StreamReader(Path, Encoding.UTF8))
                {
                    var columns = sr.ReadLine().Split().Length;
                    var lines = File.ReadAllLines(Path).Length;
                    Array = new string[lines,columns];
                    var tempNum = 0;
                    var temp = File.ReadAllText(Path).Split().Where( s => s != "").ToArray();
                    for (int i = 0; i < lines; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            Array[i, j] = temp[tempNum];
                            tempNum++;
                        }
                    }
                }
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine($"[ОШИБКА] Неверно введен двумерный массив в файле.\nПроверьте правильность введенного Вами массива в файле: \"{Path}\"\n" +
                                  "Нажмите любую клавишу, чтобы продолжить работу программы, ESC - для выхода");
                {
                     switch (Console.ReadKey().Key)
                     {
                         case ConsoleKey.Escape:
                             Process.GetCurrentProcess().Kill();
                             break;
                     }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[ОШИБКА] "+ e.Message);
                throw;    
            }
        }
    }
}