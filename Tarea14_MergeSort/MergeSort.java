import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class MergeSort {

    private static void merge(List<Integer> a, int l, int m, int r) {
        int a1 = m - l + 1;
        int a2 = r - m;

        List<Integer> L = new ArrayList<>(a1);
        List<Integer> R = new ArrayList<>(a2);

        for (int i = 0; i < a1; i++)
            L.add(a.get(l + i));
        for (int j = 0; j < a2; j++)
            R.add(a.get(m + 1 + j));

        int i = 0, j = 0, k = l;

        while (i < a1 && j < a2) {
            if (L.get(i) <= R.get(j)) {
                a.set(k, L.get(i));
                i++;
            } else {
                a.set(k, R.get(j));
                j++;
            }
            k++;
        }

        while (i < a1) {
            a.set(k, L.get(i));
            i++;
            k++;
        }

        while (j < a2) {
            a.set(k, R.get(j));
            j++;
            k++;
        }
    }

    public static void mergeSort(List<Integer> a, int l, int r) {
        if (l < r) {
            int m = l + (r - l) / 2;
            mergeSort(a, l, m);
            mergeSort(a, m + 1, r);
            merge(a, l, m, r);
        }
    }

    public static void main(String[] args) {
        List<Integer> a = new ArrayList<>(Arrays.asList(39, 28, 44, 11));

        System.out.println("Antes de Merge Sort:");
        for (int x : a) System.out.print(x + " ");
        System.out.println();

        mergeSort(a, 0, a.size() - 1);

        System.out.println("Después de Merge Sort:");
        for (int x : a) System.out.print(x + " ");
        System.out.println();
    }
}