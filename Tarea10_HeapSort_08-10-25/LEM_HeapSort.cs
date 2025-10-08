using System;

class HeapSort
{
    // Función para ajustar el subárbol con raíz en el índice i
    static void Heapify(int[] arr, int n, int i)
    {
        int largest = i;        // Suponemos que la raíz es el mayor
        int left = 2 * i + 1;   // Hijo izquierdo
        int right = 2 * i + 2;  // Hijo derecho

        // Si el hijo izquierdo existe y es mayor que la raíz
        if (left < n && arr[left] > arr[largest])
            largest = left;

        // Si el hijo derecho existe y es mayor que el mayor actual
        if (right < n && arr[right] > arr[largest])
            largest = right;

        // Si el mayor no es la raíz, intercambiamos y seguimos ajustando recursivamente
        if (largest != i)
        {
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;

            // Llamada recursiva
            Heapify(arr, n, largest);
        }
    }

    // Función principal de heapsort
    static void HeapSortAlg(int[] arr)
    {
        int n = arr.Length;

        // Construir heap máximo
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i);

        // Extraer elementos uno por uno
        for (int i = n - 1; i > 0; i--)
        {
            // Mover la raíz (máximo) al final
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            // Llamar heapify en el heap reducido
            Heapify(arr, i, 0);
        }
    }

    static void Main()
    {
        int[] arr = { 15, 3, 66, 6, 44, 50 };
        int n = arr.Length;

        Console.Write("Antes de ordenar los elementos del arreglo: ");
        for (int i = 0; i < n; i++)
            Console.Write(arr[i] + " ");
        Console.WriteLine();

        HeapSortAlg(arr);

        Console.Write("\nDespués de ordenar el arreglo: ");
        for (int i = 0; i < n; i++)
            Console.Write(arr[i] + " ");
        Console.WriteLine();
    }
}
