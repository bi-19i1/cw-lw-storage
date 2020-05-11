/*
 * Лабораторная работа 3. Работа с файлами
 * Написать программу, которая позволяет инициализировать двумерный массив, заданный в файле в формате: {1} {2} {n1 n2 n3 ...},
 * где {1} соответствует количеству строк массива, {2} соответствует количеству столбцов массива,
 * а {n1 n2 n3 ...} соответствуют значениям массива.
 * Для инициализированного массива:
 * написать функцию вывода массива по строкам и столбцам;
 * написать функцию, которая возвращает копию исходного массива,
 * в котором значения элементов на главной и побочной диагонали поменяны местами;
 * написать функцию, которая возвращает все четные элементы исходного массива в виде одномерного массива;
 * написать функцию, которая записывает в файл, имя которого передано в качестве аргумента,
 * одномерный массив в формате, при котором все числа массива, разделенные пробелом, находятся на одной строке.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LabWorks.Lab_3
{
  public class Lab_3
  {
    public void Start()
    {
      var array = GenerateArray();
      Task1(array);
      Task2(array);
      // 3 задание
      Console.WriteLine("3 задание: ");
      foreach (var item in Task3(array))
      {
        Console.Write(item.ToString() + " ");
      }
      Console.WriteLine("\n");
      // end 3 задание
      Task4(array);
    }

    static void Task4(string[,] array)
    {
      Console.WriteLine("4 задание:");
      try
      {
        using (var sw = new StreamWriter("Task4.txt", false, Encoding.UTF8))
        {
          for (var i = 0; i < array.GetLength(0); i++)
          {
            for (var j = 0; j < array.GetLength(1); j++)
            {
              sw.Write(array[i, j] + " ");
            }
          }
        }

        Console.WriteLine("В папку с программой успешно записан файл \"Task4\"");
      }
      catch (Exception)
      {
        Console.WriteLine("Что-то пошло не так.");
        throw;
      }
    }

    private static string[,] GenerateArray()
    {
      var componentsArray = InitArray();
      var columns = Convert.ToInt32(componentsArray[0]);
      var rows = Convert.ToInt32(componentsArray[1]);
      var array = new string[columns, rows];
      var z = 2;
      var exceptionIsShow = false;
      for (int i = 0; i < columns; i++)
      {
        for (int j = 0; j < rows; j++)
        {
          try
          {
            array[i, j] = componentsArray[z];
            z++;
          }
          catch (Exception)
          {
            array[i, j] = "0";
            if (!exceptionIsShow)
            {
              Console.WriteLine(
                "В файле были заданы значения не для всех элементов массива.\nНезаданным элементам присвоен \"0\".\n");
            }

            exceptionIsShow = true;
          }
        }
      }

      return array;
    }

    private static void
      Task1(string[,] array,
        string message = "Полученная матрица") // 1. Написать функцию вывода массива по строкам и столбцам
    {
      Console.WriteLine($"{message}:");
      for (var i = 0; i < array.GetLength(0); i++)
      {
        for (var j = 0; j < array.GetLength(1); j++)
        {
          Console.Write(array[i, j]);
        }

        Console.WriteLine();
      }

      Console.WriteLine();
    }

    // 2. написать функцию, которая возвращает копию исходного массива,
    // в котором значения элементов на главной и побочной диагонали поменяны местами;
    private static void Task2(string[,] array)
    {
      for (var i = 0; i < array.GetLength(0); i++)
      {
        for (var j = 0; j < array.GetLength(1); j++)
        {
          if (i != j) continue;
          var temp = array[i, array.GetLength(0) - i - 1];
          array[i, array.GetLength(0) - i - 1] = array[i, j];
          array[i, j] = temp;
        }
      }

      Task1(array, "2 задание");
    }

    private static int[]
      Task3(string[,] array) // написать функцию, которая возвращает все четные элементы исходного массива в виде одномерного массива;
    {
      var listTemp = new List<int>();
      for (var i = 0; i < array.GetLength(0); i++)
      {
        for (var j = 0; j < array.GetLength(1); j++)
        {
          if (Convert.ToInt32(array[i, j]) % 2 == 0)
          {
            listTemp.Add(Convert.ToInt32(array[i, j]));
          }
        }
      }

      var doneArray = listTemp.ToArray();
      return doneArray;
    }

    private static string[] InitArray() // чтение параметров массива из файла
    {
      try
      {
        using (var sr = new StreamReader("settings.ini", Encoding.UTF8))
        {
          var array = sr.ReadToEnd().Replace('{', ' ')
            .Replace('}', ' ')
            .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
          return array;
        }
      }
      catch (FileNotFoundException)
      {
        Console.WriteLine(
          "Файла с конфигурациями массива не создан.\nСоздайте, пожалуйста, файл с именем \"settings.ini\" в папке с программой и повторите попытку.");
        Console.ReadKey();
        throw;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        throw;
      }
    }
  }
}