import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        int[] nums = {10, 20, 30, 40, 50};

        nums[3] = 55;

        System.out.println("Arreglo fijo:");
        for (int n : nums)
            System.out.println(n);

        Scanner read = new Scanner(System.in);
        System.out.print("¿Cuántos números?: ");
        int Size = read.nextInt();
        int[] arr = new int[Size];

        for (int i = 0; i < Size; i++) {
            System.out.print("Ingresar Valor " + (i+1) + ": ");
            arr[i] = read.nextInt();
        }

        System.out.println("\nValores ingresados:");
        for (int i : arr) {
            System.out.println(i);
        }

        read.close();
    }
}
