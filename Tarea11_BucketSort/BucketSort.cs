using System;
using System.Collections.Generic;
using System.Linq;

public class BucketSortTranslator
{
    
    public static void InsertionSort(List<float> a)
    {
        int n = a.Count;
        for (int i = 1; i < n; i++)
        {
            float temp = a[i];
            int j = i - 1;
            
            
            while (j >= 0 && temp < a[j])
            {
                a[j + 1] = a[j];
                j--;
            }
            a[j + 1] = temp;
        }
    }

    
    public static void PrintArr(List<float> arr)
    {
        for (int i = 0; i < arr.Count; i++)
        {
           
            Console.Write(arr[i]);
            if (i != arr.Count - 1)
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine();
    }

    
    public static void BucketSort(List<float> inputArr)
    {
        
        int s = inputArr.Count;
        List<List<float>> bucketArr = new List<List<float>>(s);
        for (int i = 0; i < s; i++)
        {
            bucketArr.Add(new List<float>());
        }

        foreach (float j in inputArr)
        {
            int bi = (int)(s * j);
            bucketArr[bi].Add(j); 
        }

        foreach (List<float> bukt in bucketArr)
        {
            InsertionSort(bukt); 
        }

        int idx = 0;
        foreach (List<float> bukt in bucketArr)
        {
            foreach (float j in bukt)
            {
                inputArr[idx] = j;
                idx += 1;
            }
        }
    }

    
    public static void Main(string[] args)
    {
        
        List<float> arr = new List<float> {0.77f, 0.16f, 0.28f, 0.25f, 0.71f, 0.93f, 0.22f, 0.11f, 0.24, 0.67f};

        Console.Write("Arreglo antes de Bucket Sort: ");
        PrintArr(arr);

        
        BucketSort(arr);

        Console.Write("Arreglo después de Bucket Sort:");
        PrintArr(arr);
    }
}