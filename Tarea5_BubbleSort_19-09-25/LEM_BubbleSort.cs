using System;

class Program
{
    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n; i++)
        {
            bool swap = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swap = true;
                }
            }
            if (!swap)
            {
                break; 
            }
        }
    }

    static void Main()
    {
        const int SIZE = 10;
        int[] arr = new int[SIZE];
        Random rnd = new Random();

        for (int i = 0; i < SIZE; i++)
        {
            arr[i] = rnd.Next(1, 101);
        }

        Console.WriteLine("Arreglo original: " + string.Join(" ", arr));

        BubbleSort(arr);

        Console.WriteLine("Arreglo ordenado: " + string.Join(" ", arr));
    }
}
