using System;
using System.Collections.Generic;

namespace SortMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList = new List<int>() { 5, 6, 10, 1, 2, 3, 78, 12, 13 };

            PrintList(myList);

            var myListSorted = Sorting.MergeSorting(myList);

            PrintList(myListSorted);
        }

        /// <summary>
        /// Print a list as follow : "1, 2, 3, 4, 5"
        /// </summary>
        /// <typeparam name="T">The type of the list to print</typeparam>
        /// <param name="list">The list to print to the console</param>
        private static void PrintList<T>(List<T> list)
        {
            var counter = 0;

            foreach (var item in list)
            {
                counter++;

                Console.Write(item);

                if (counter != list.Count)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine("");
                }
            }
        }
    }
}
