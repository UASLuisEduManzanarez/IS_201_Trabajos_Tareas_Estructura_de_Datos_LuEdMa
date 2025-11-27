using System;
using System.Collections.Generic;
using System.Linq;

public class RadixSortTranslator
{
    private static void CountingSort(List<int> arr, int exp)
    {
        int s = arr.Count;
        int[] outputArray = new int[s];
        int[] countArray = new int[10];

        for (int j = 0; j < s; j++)
        {
            int idx = (arr[j] / exp) % 10;
            countArray[idx]++;
        }

        for (int j = 1; j < 10; j++)
            countArray[j] += countArray[j - 1];

        for (int j = s - 1; j >= 0; j--)
        {
            int idx = (arr[j] / exp) % 10;
            outputArray[countArray[idx] - 1] = arr[j];
            countArray[idx]--;
        }


        for (int j = 0; j < s; j++)
            arr[j] = outputArray[j];
    }

    public static void RadixSort(List<int> arr)
    {
        int max1 = arr.Max();
        
        for (int exp = 1; max1 / exp > 0; exp *= 10)
            CountingSort(arr, exp);
    }

    public static void Main(string[] args)
    {
        List<int> arr = new List<int> { 171, 46, 76, 91, 803, 25, 3, 67 };

        Console.WriteLine("Arreglo antes de Radix Sort:");
        foreach (int n in arr)
        {
            Console.Write(n + " ");
        }
        Console.WriteLine(); 

        RadixSort(arr);

        Console.WriteLine("Después de Radix Sort:");
        foreach (int n in arr)
        {
            Console.Write(n + " ");
        }
        Console.WriteLine();
    }
}