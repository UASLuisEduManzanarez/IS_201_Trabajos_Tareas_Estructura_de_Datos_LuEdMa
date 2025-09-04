import java.util.ArrayList;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        ArrayList<String> Nombres = new ArrayList<>();
        Nombres.add("Luis");
        Nombres.add("Romina");
        Nombres.add("Chema");
        Nombres.add("Carlitos");

        System.out.println("Recorrido por el array.");
        for (int j = 0; j < Nombres.size(); j++) {
            System.out.print(Nombres.get(j) + "  ");
        }
        System.out.println();

        System.out.println("\nInsertar Nombre");
        System.out.print("Escribe el nombre a insertar: ");
        String insert = sc.nextLine();
        Nombres.add(1, insert);

        for (int j = 0; j < Nombres.size(); j++) {
            System.out.print(Nombres.get(j) + "  ");
        }
        System.out.println();

        System.out.println("Busqueda Lineal");
        System.out.print("Escribe el nombre que quieres buscar: ");
        String buscarnom = sc.nextLine();

        boolean exist = false;
        for (String j : Nombres) {
            if (j.equals(buscarnom)) {
                System.out.println("Si existe el nombre");
                exist = true;
                break;
            }
        }

        if (!exist) {
            System.out.println("No existe el nombre");
        }

        sc.close();
    }
}
