using System;
using System.Collections.Generic;
using System.Linq;

public class ShellSort
{
    public static void DisplayArr(List<int> inputArr)
    {
        foreach (int k in inputArr)
        {
            Console.Write(k + " ");
        }
        Console.WriteLine();
    }

    public void Sort(List<int> inputArr)
    {
        int size = inputArr.Count;
        
        for (int gapsize = size / 2; gapsize > 0; gapsize /= 2)
        {
            for (int j = gapsize; j < size; j++)
            {
                int val = inputArr[j];
                int k = j;
                while (k >= gapsize && inputArr[k - gapsize] > val)
                {
                    inputArr[k] = inputArr[k - gapsize];
                    k = k - gapsize;
                }
                inputArr[k] = val;
            }
        }
    }

    public static void Main(string[] args)
    {
        List<int> inputArr = new List<int> { 36, 34, 43, 11, 15, 20, 28, 45 };
        Console.WriteLine("Arreglo antes de HashSort(Shell) ");
        ShellSort.DisplayArr(inputArr);

        ShellSort obj = new ShellSort();
        obj.Sort(inputArr);

        Console.WriteLine("Arreglo después HashSort(Shell) ");
        ShellSort.DisplayArr(inputArr);
    }
}