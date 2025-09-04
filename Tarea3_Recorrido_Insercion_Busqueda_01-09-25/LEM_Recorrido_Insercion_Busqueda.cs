using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> Nombres = new List<string> { "Luis", "Romina", "Chema", "Carlitos" };

        Console.WriteLine("Recorrido por el array.");
        for (int j = 0; j < Nombres.Count; j++)
        {
            Console.Write(Nombres[j] + "  ");
        }
        Console.WriteLine();

        Console.WriteLine("\nInsertar Nombre");
        Console.Write("Escribe el nombre a insertar: ");
        string insert = Console.ReadLine();
        Nombres.Insert(1, insert);

        for (int j = 0; j < Nombres.Count; j++)
        {
            Console.Write(Nombres[j] + "  ");
        }
        Console.WriteLine();

        Console.WriteLine("Busqueda Lineal");
        Console.Write("Escribe el nombre que quieres buscar: ");
        string buscarnom = Console.ReadLine();

        bool exist = false;
        foreach (string j in Nombres)
        {
            if (j == buscarnom)
            {
                Console.WriteLine("Si existe el nombre");
                exist = true;
                break;
            }
        }

        if (!exist)
        {
            Console.WriteLine("No existe el nombre");
        }
    }
}
