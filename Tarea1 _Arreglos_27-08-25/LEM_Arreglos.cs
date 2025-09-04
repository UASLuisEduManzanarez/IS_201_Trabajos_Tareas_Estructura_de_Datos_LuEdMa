using System;

class Program {
    static void Main() {
        int[] nums = {10, 20, 30, 40, 50};

        numeros[3] = 55;

        foreach (int n in numeros)
            Console.WriteLine(n);

        Console.Write("¿Cuantos numeros? ");
        
        int Size = int.Parse(Console.ReadLine());
        int[] arr = new int[Size];
        
        for(int i = 0; i < Size; i++) {
            Console.Write($"Ingresar valor {i+1}: ");
            arr[i] = int.Parse(Console.ReadLine());
        }
    }
}