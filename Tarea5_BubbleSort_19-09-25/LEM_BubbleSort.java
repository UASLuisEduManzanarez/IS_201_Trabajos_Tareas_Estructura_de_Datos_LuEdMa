public class Main {
    
    public static void bubbleSort(int[] arr, int size) {
        for (int i = 0; i < size - 1; i++) {
            for (int j = 0; j < size - i - 1; j++) {
                if (arr[j] > arr[j + 1]) {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    public static void mostrarArreglo(int[] arr, int size) {
        for (int i = 0; i < size; i++) {
            System.out.print(arr[i] + " ");
        }
        System.out.println();
    }

    public static void main(String[] args) {
        int[] arr = {21, 13, 44, 32, 78, 2};
        int size = arr.length;

        System.out.print("Lista original: ");
        mostrarArreglo(arr, size);

        bubbleSort(arr, size);

        System.out.print("Lista ordenada: ");
        mostrarArreglo(arr, size);
    }
}