using System;

class Program
{
    static void Selection(int[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            int small = i; // índice del elemento más pequeño
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[small] > a[j])
                {
                    small = j; // actualiza el índice del elemento más pequeño
                }
            }

            // intercambia los elementos
            int temp = a[i];
            a[i] = a[small];
            a[small] = temp;
        }
    }

    static void PrintArr(int[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            Console.Write(a[i] + " ");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        int[] a = { 99, 34, 32, 78, 55 };

        Console.WriteLine("Arreglo antes de ser ordenado:");
        PrintArr(a);

        Selection(a);

        Console.WriteLine("Arreglo después de ser ordenado:");
        PrintArr(a);
    }
}
