public class LEM_HeapSort {

    static void heapify(int arr[], int n, int i) {
        int largest = i;       
        int left = 2 * i + 1;   
        int right = 2 * i + 2;  

        if (left < n && arr[left] > arr[largest])
            largest = left;

        if (right < n && arr[right] > arr[largest])
            largest = right;

        if (largest != i) {
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;

            heapify(arr, n, largest);
        }
    }

    static void heapsort(int arr[]) {
        int n = arr.length;

        for (int i = n / 2 - 1; i >= 0; i--)
            heapify(arr, n, i);

        for (int i = n - 1; i > 0; i--) {
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            heapify(arr, i, 0);
        }
    }

    public static void main(String[] args) {
        int arr[] = {15, 3, 66, 6, 44, 50};
        int n = arr.length;

        System.out.print("Antes de ordenar los elementos del arreglo: ");
        for (int i = 0; i < n; i++)
            System.out.print(arr[i] + " ");
        System.out.println();

        heapsort(arr);

        System.out.print("\nDespués de ordenar el arreglo: ");
        for (int i = 0; i < n; i++)
            System.out.print(arr[i] + " ");
        System.out.println();
    }
}
