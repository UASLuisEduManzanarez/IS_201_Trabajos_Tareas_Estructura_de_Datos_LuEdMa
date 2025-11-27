import java.util.ArrayList;
import java.util.List;

public class BucketSort {

    public static void insertionSort(List<Float> a) {
        int n = a.size();
        for (int i = 1; i < n; i++) {
            float temp = a.get(i);
            int j = i - 1;
            
            while (j >= 0 && temp < a.get(j)) {
                a.set(j + 1, a.get(j));
                j--;
            }

            a.set(j + 1, temp);
        }
    }

    public static void printArr(List<Float> arr) {
        for (int i = 0; i < arr.size(); i++) {
            System.out.print(arr.get(i));
            if (i != arr.size() - 1) {
                System.out.print(" ");
            }
        }
        System.out.println();
    }
    public static void bucketSort(List<Float> inputArr) {
        int s = inputArr.size();
        List<List<Float>> bucketArr = new ArrayList<>(s);
        for (int i = 0; i < s; i++) {
            bucketArr.add(new ArrayList<Float>());
        }

        for (float j : inputArr) {
            int bi = (int)(s * j);
            bucketArr.get(bi).add(j); 
        }

        for (List<Float> bukt : bucketArr) {
            insertionSort(bukt); 
        }

        int idx = 0;
        for (List<Float> bukt : bucketArr) {
            for (float j : bukt) {
                inputArr.set(idx, j);
                idx += 1;
            }
        }
    }

    
    public static void main(String[] args) {
        List<Float> arr = new ArrayList<>();
        arr.add(0.77f);
        arr.add(0.16f);
        arr.add(0.28f);
        arr.add(0.25f);
        arr.add(0.71f);
        arr.add(0.93f);
        arr.add(0.22f);
        arr.add(0.11f);
        arr.add(0.24f);
        arr.add(0.67f);

        System.out.print("Arreglo antes de Bucket Sort: ");
        printArr(arr);

        
        bucketSort(arr);

        System.out.print("Arreglo después de Bucket Sort:");
        printArr(arr);
    }
}