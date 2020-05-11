/*
 *   Лабораторная работа. Работа с массивами массивов
 * - Инициализировать родительский массив, дочерними элементами которого являются массивы целочисленного типа данных.
 *   Количество дочерних массивов, а также количество элементов  в них запросить у пользователя.
 * - Элементы дочерних массивов задать случайным образом в диапазоне от 0 до 10.
 * - Вывести все четные элементы из всех дочерних массивов.
 * - Выполнить сортировку родительского массива по возрастанию количества элементов дочерних массивов кратных 5.
 * - Вывести отсортированный родительский массив на экран.
 * - Вывести дочерний массив с наибольшим количеством нечетных элементов.
 */

using System;

namespace LabWorks.ExtendedWorkWithArrays
{
    public class Lab_1
    {
        int[][] arrays;
        public void Start()
        {
            InitArray();
            GenerateRandomElements();
            ShowArrays();
            ShowEvenNum();
            SortArray();
        }

        void SortArray()
        {
            Array.Sort(arrays,
                (x, y) => (x.Length - y.Length) % 5); // Не понял суть задания.
            ShowArrays();
        }
        private void ShowArrays()
        {
            var temp = 1;
            foreach (var array in arrays)
            {
                Console.Write($"Элементы {temp} массива: ");
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    Console.Write(array[i]);
                    if (i < array.GetLength(0) - 1)
                        Console.Write(", ");
                }
                Console.WriteLine();
                temp++;
            }
        }

        private void ShowEvenNum()
        {
            Console.Write("Чётные элементы из всех дочерних массивов: ");
            foreach (var array in arrays)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i] % 2 == 0)
                    {
                        Console.Write(array[i] + " ");
                    }
                }
            }
            Console.WriteLine("\n");
        }
        private void GenerateRandomElements()
        {
            var rnd = new Random();
            foreach (var array in arrays)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    array[i] = rnd.Next(0, 11);
                }
            }
        }
        private void InitArray()
        {
            Console.Write("Введите количество дочерних массивов массива: ");
            var countArray = Convert.ToInt32(Console.ReadLine());
            arrays = new int[countArray][];
            for (int i = 0; i < arrays.GetLength(0); i++)
            {
                Console.Write($"Введите количество элементов массива {i + 1}: ");
                arrays[i] = new int[Convert.ToInt32(Console.ReadLine())];
            }
        }
    }
}