using System;

namespace Exo4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] changedDimensions = changeTableDimension();

            int[,] tab = new int[changedDimensions[0], changedDimensions[1]];
            printTable(tab, changedDimensions[0], changedDimensions[1]);
        }

        private static int[] changeTableDimension()
        {
            int[] newDimensions = new int[2];

            Console.WriteLine("How many rows ?");
            int.TryParse(Console.ReadLine(), out newDimensions[0]);

            Console.WriteLine("How many columns ?");
            int.TryParse(Console.ReadLine(), out newDimensions[1]);

            return newDimensions;
        }

        private static void printTable(int[,] tab, int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    tab[i, j] = 100 * (i + j);
                    Console.WriteLine("tab[" + i + ", " + j + "] = " + tab[i, j]);
                }
            }
        }
    }
}
