import java.util.ArrayList;
import java.util.List;
import java.util.Arrays;

public class RadixSort {

    private static void countingSort(List<Integer> arr, int exp) {
        int s = arr.size();
        int[] outputArray = new int[s];
        int[] countArray = new int[10]; 

        for (int j = 0; j < s; j++) {
            int idx = (arr.get(j) / exp) % 10;
            countArray[idx]++;
        }

        for (int j = 1; j < 10; j++)
            countArray[j] += countArray[j - 1];

        for (int j = s - 1; j >= 0; j--) {
            int idx = (arr.get(j) / exp) % 10;
            outputArray[countArray[idx] - 1] = arr.get(j);
            countArray[idx]--;
        }

        for (int j = 0; j < s; j++)
            arr.set(j, outputArray[j]);
    }

    private static int getMax(List<Integer> arr) {
        int max = arr.get(0);
        for (int i = 1; i < arr.size(); i++) {
            if (arr.get(i) > max) {
                max = arr.get(i);
            }
        }
        return max;
    }

    public static void radixSort(List<Integer> arr) {
        int max1 = getMax(arr);
        
        for (int exp = 1; max1 / exp > 0; exp *= 10)
            countingSort(arr, exp);
    }

    public static void main(String[] args) {
        List<Integer> arr = new ArrayList<>(Arrays.asList(171, 46, 76, 91, 803, 25, 3, 67));

        System.out.println("Arreglo antes de Radix Sort:");
        for (int n : arr) System.out.print(n + " ");
        System.out.println();

        radixSort(arr);

        System.out.println("Después de Radix Sort:");
        for (int n : arr) System.out.print(n + " ");
        System.out.println();
    }
}