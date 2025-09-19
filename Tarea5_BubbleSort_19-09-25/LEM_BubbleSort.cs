using System;

class Program
{
    static void BubbleSort(int[] arr, int size)
    {
        for (int i = 0; i < size - 1; i++)
        {
            for (int j = 0; j < size - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    static void MostrarArreglo(int[] arr, int size)
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        int[] arr = { 21, 13, 44, 32, 78, 2 };
        int size = arr.Length;

        Console.Write("Lista original: ");
        MostrarArreglo(arr, size);

        BubbleSort(arr, size);

        Console.Write("Lista ordenada: ");
        MostrarArreglo(arr, size);
    }
}
