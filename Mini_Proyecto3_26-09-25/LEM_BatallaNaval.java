import java.util.Scanner;

public class LEM_BatallaNaval {
    static final int N = 10;
    static char[][] tablero1 = new char[N][N];
    static char[][] tablero2 = new char[N][N];
    static Scanner sc = new Scanner(System.in);

    static String[] nombresBarcos = {"Portaaviones", "Acorazado", "Crucero", "Submarino", "Destructor"};
    static int[] tamanosBarcos = {5, 4, 3, 3, 2};

    static final String RESET = "\u001B[0m";
    static final String ROJO = "\u001B[31m";
    static final String AZUL = "\u001B[34m";

    public static void main(String[] args) {

        llenarTablero(tablero1);
        llenarTablero(tablero2);

        System.out.println("=== BATALLA NAVAL - COLOCACIÓN DE BARCOS ===");

        System.out.println("\n>>> Jugador 1 coloca sus barcos:");
        colocarTodosLosBarcos(tablero1, AZUL, 'S');

        System.out.println("\n>>> Jugador 2 coloca sus barcos:");
        colocarTodosLosBarcos(tablero2, ROJO, 'B');

        System.out.println("\n=== TABLERO FINAL JUGADOR 1 ===");
        mostrarTablero(tablero1, AZUL);

        System.out.println("\n=== TABLERO FINAL JUGADOR 2 ===");
        mostrarTablero(tablero2, ROJO);
    }

    static void llenarTablero(char[][] tablero) {
        for (int fila = 0; fila < N; fila++) {
            for (int col = 0; col < N; col++) {
                tablero[fila][col] = '.';
            }
        }
    }

    static void mostrarTablero(char[][] tablero, String color) {
        System.out.print("   ");
        for (int col = 1; col <= N; col++) {
            System.out.print(col + " ");
        }
        System.out.println();

        for (int fila = 0; fila < N; fila++) {
            System.out.print((char) ('A' + fila) + "  ");
            for (int col = 0; col < N; col++) {
                char casilla = tablero[fila][col];
                if (casilla == '.') {
                    System.out.print(". ");
                } else {
                    System.out.print(color + casilla + RESET + " ");
                }
            }
            System.out.println();
        }
    }

    static void colocarTodosLosBarcos(char[][] tablero, String color, char simbolo) {
        for (int i = 0; i < nombresBarcos.length; i++) {
            boolean colocado = false;

            while (!colocado) {
                mostrarTablero(tablero, color);
                System.out.println("Coloca tu " + nombresBarcos[i] + " (tamaño " + tamanosBarcos[i] + ")");
                System.out.print("Coordenada inicial (ejemplo A1): ");
                String entrada = sc.next().toUpperCase();
                System.out.print("Orientación (H = Horizontal, V = Vertical): ");
                char orientacion = sc.next().toUpperCase().charAt(0);

                int fila = entrada.charAt(0) - 'A';
                int col = Integer.parseInt(entrada.substring(1)) - 1;

                if (esPosicionValida(tablero, fila, col, orientacion, tamanosBarcos[i])) {
                    colocarBarco(tablero, fila, col, orientacion, simbolo, tamanosBarcos[i]);
                    colocado = true;
                } else {
                    System.out.println("⚠️ No se puede colocar el barco en esa posición, intenta otra vez.");
                }
            }
        }
    }

    static boolean esPosicionValida(char[][] tablero, int fila, int col, char orientacion, int tamaño) {
        if (fila < 0 || fila >= N || col < 0 || col >= N) return false;

        if (orientacion == 'H') {
            if (col + tamaño > N) return false;
            for (int j = col; j < col + tamaño; j++) {
                if (tablero[fila][j] != '.') return false;
            }
        } else if (orientacion == 'V') {
            if (fila + tamaño > N) return false;
            for (int i = fila; i < fila + tamaño; i++) {
                if (tablero[i][col] != '.') return false;
            }
        } else {
            return false;
        }
        return true;
    }

    static void colocarBarco(char[][] tablero, int fila, int col, char orientacion, char simbolo, int tamaño) {
        if (orientacion == 'H') {
            for (int j = col; j < col + tamaño; j++) {
                tablero[fila][j] = simbolo;
            }
        } else {
            for (int i = fila; i < fila + tamaño; i++) {
                tablero[i][col] = simbolo;
            }
        }
    }
}
