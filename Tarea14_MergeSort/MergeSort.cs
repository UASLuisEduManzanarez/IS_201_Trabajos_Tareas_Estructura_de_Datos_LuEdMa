using System;
using System.Collections.Generic;
using System.Linq;

public class MergeSort
{
    private static void Merge(List<int> a, int l, int m, int r)
    {
        int a1 = m - l + 1;
        int a2 = r - m;

        List<int> L = new List<int>(a1);
        List<int> R = new List<int>(a2);

        for (int i = 0; i < a1; i++)
            L.Add(a[l + i]);
        for (int j = 0; j < a2; j++)
            R.Add(a[m + 1 + j]);

        int i = 0, j = 0, k = l;

        while (i < a1 && j < a2)
        {
            if (L[i] <= R[j])
            {
                a[k] = L[i];
                i++;
            }
            else
            {
                a[k] = R[j];
                j++;
            }
            k++;
        }

        while (i < a1)
        {
            a[k] = L[i];
            i++;
            k++;
        }

        while (j < a2)
        {
            a[k] = R[j];
            j++;
            k++;
        }
    }

    public static void MergeSort(List<int> a, int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            MergeSort(a, l, m);
            MergeSort(a, m + 1, r);
            Merge(a, l, m, r);
        }
    }

    public static void Main(string[] args)
    {
        List<int> a = new List<int> { 39, 28, 44, 11 };

        Console.WriteLine("Antes de Merge Sort:");
        foreach (int x in a) Console.Write(x + " ");
        Console.WriteLine();

        MergeSort(a, 0, a.Count - 1);

        Console.WriteLine("Después de Merge Sort:");
        foreach (int x in a) Console.Write(x + " ");
        Console.WriteLine();
    }
}