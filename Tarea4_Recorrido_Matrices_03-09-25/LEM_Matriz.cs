using System;

class Program
{
    static void Main()
    {
        int[,] nums = {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };

        Console.WriteLine("Matriz:");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("[ ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(nums[i, j] + " ");
            }
            Console.WriteLine("]");
        }

        Console.WriteLine("\nPor renglones:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(nums[i, j] + " ");
            }
        }

        Console.WriteLine("\nPor columnas:");
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write(nums[i, j] + " ");
            }
        }
    }
}