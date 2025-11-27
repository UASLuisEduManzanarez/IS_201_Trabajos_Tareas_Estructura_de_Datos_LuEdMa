import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class HashSort {
    
    public static void displayArr(List<Integer> inputArr) {
        for (int k : inputArr) {
            System.out.print(k + " ");
        }
        System.out.println();
    }

    public void sort(List<Integer> inputArr) {
        int size = inputArr.size();
        
        for (int gapsize = size / 2; gapsize > 0; gapsize /= 2) {
            for (int j = gapsize; j < size; j++) {
                int val = inputArr.get(j);
                int k = j;
                while (k >= gapsize && inputArr.get(k - gapsize) > val) {
                    inputArr.set(k, inputArr.get(k - gapsize));
                    k = k - gapsize;
                }
                inputArr.set(k, val);
            }
        }
    }

    public static void main(String[] args) {
        List<Integer> inputArr = new ArrayList<>(Arrays.asList(36, 34, 43, 11, 15, 20, 28, 45));
        System.out.println("Arreglo antes de HashSort(Shell) ");
        HashSort.displayArr(inputArr);

        HashSort obj = new HashSort();
        obj.sort(inputArr);

        System.out.println("Arreglo después HashSort(Shell) ");
        HashSort.displayArr(inputArr);
    }
}