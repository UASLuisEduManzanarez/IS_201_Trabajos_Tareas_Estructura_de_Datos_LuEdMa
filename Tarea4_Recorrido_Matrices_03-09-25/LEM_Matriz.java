public class LEM_Matriz {
    public static void main(String[] args) {
        int[][] nums = {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };

        System.out.println("Matriz:");
        for (int i = 0; i < 3; i++) {
            System.out.print("[ ");
            for (int j = 0; j < 3; j++) {
                System.out.print(nums[i][j] + " ");
            }
            System.out.println("]");
        }

        System.out.println("\nPor renglones:");
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                System.out.print(nums[i][j] + " ");
            }
        }

        System.out.println("\nPor columnas:");
        for (int j = 0; j < 3; j++) {
            for (int i = 0; i < 3; i++) {
                System.out.print(nums[i][j] + " ");
            }
        }
    }
}