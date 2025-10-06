//Primera revision 26-09-2025 Luis Eduardo Manzanarez Ramos
import java.util.Scanner;

public class LEM_BatallaNava_Final {

    static final int TAM = 10; 
    static char[][] tab1 = new char[TAM][TAM];
    static char[][] tab2 = new char[TAM][TAM];
    static char[][] dis1 = new char[TAM][TAM];
    static char[][] dis2 = new char[TAM][TAM];
    static Scanner sc = new Scanner(System.in);

    static String[] nombres = {"Portaaviones", "Acorazado", "Crucero", "Submarino", "Destructor"};
    static int[] tamanos = {5, 4, 3, 3, 2};
    static char[] simbolos = {'P', 'A', 'C', 'S', 'D'};

    public static void main(String[] args) {
        iniciarTablero(tab1);
        iniciarTablero(tab2);
        iniciarTablero(dis1);
        iniciarTablero(dis2);

        System.out.println("=== BATALLA NAVAL ===");

        System.out.println("\nJugador 1 coloca sus barcos:");
        colocarFlota(tab1);

        System.out.println("\nJugador 2 coloca sus barcos:");
        colocarFlota(tab2);

        boolean fin = false;
        while (!fin) {
            System.out.println("\n--- TURNO JUGADOR 1 ---");
            fin = turno(tab2, dis1, "Jugador 1");
            if (fin) break;

            System.out.println("\n--- TURNO JUGADOR 2 ---");
            fin = turno(tab1, dis2, "Jugador 2");
        }

        System.out.println("\nJuego terminado.");
    }

    static void iniciarTablero(char[][] t) {
        for (int i = 0; i < TAM; i++) {
            for (int j = 0; j < TAM; j++) {
                t[i][j] = '.';
            }
        }
    }

    static void mostrarTablero(char[][] t) {
        System.out.print("  ");
        for (int j = 1; j <= TAM; j++) System.out.print(j + " ");
        System.out.println();
        for (int i = 0; i < TAM; i++) {
            System.out.print((char) ('A' + i) + " ");
            for (int j = 0; j < TAM; j++) {
                System.out.print(t[i][j] + " ");
            }
            System.out.println();
        }
    }

    static void colocarFlota(char[][] tab) {
        for (int b = 0; b < nombres.length; b++) {
            boolean colocado = false;
            while (!colocado) {
                mostrarTablero(tab);
                System.out.println("Coloca tu " + nombres[b] + " (tamaño " + tamanos[b] + ")");
                System.out.print("Coordenada inicial (ej. A1): ");
                String coord = sc.next().toUpperCase();
                System.out.print("Orientación (H o V): ");
                char orient = sc.next().toUpperCase().charAt(0);

                int fila = coord.charAt(0) - 'A';
                int col = Integer.parseInt(coord.substring(1)) - 1;

                if (puedeColocar(tab, fila, col, orient, tamanos[b])) {
                    ponerBarco(tab, fila, col, orient, tamanos[b], simbolos[b]);
                    colocado = true;
                } else {
                    System.out.println("No se puede colocar ahí, intenta otra vez.");
                }
            }
        }
    }

    static boolean puedeColocar(char[][] t, int f, int c, char o, int tam) {
        if (f < 0 || c < 0 || f >= TAM || c >= TAM) return false;

        if (o == 'H') {
            if (c + tam > TAM) return false;
            for (int j = c; j < c + tam; j++) if (t[f][j] != '.') return false;
        } else if (o == 'V') {
            if (f + tam > TAM) return false;
            for (int i = f; i < f + tam; i++) if (t[i][c] != '.') return false;
        } else return false;

        return true;
    }

    static void ponerBarco(char[][] t, int f, int c, char o, int tam, char simbolo) {
        if (o == 'H') {
            for (int j = c; j < c + tam; j++) t[f][j] = simbolo;
        } else {
            for (int i = f; i < f + tam; i++) t[i][c] = simbolo;
        }
    }

    static boolean turno(char[][] enemigo, char[][] disparos, String jugador) {
        mostrarTablero(disparos);
        System.out.print(jugador + ", elige una coordenada para disparar (ej. B3): ");
        String coord = sc.next().toUpperCase();
        int f = coord.charAt(0) - 'A';
        int c = Integer.parseInt(coord.substring(1)) - 1;

        if (f < 0 || f >= TAM || c < 0 || c >= TAM) {
            System.out.println("Coordenada inválida. Pierdes tu turno.");
            return false;
        }

        if (disparos[f][c] == 'X' || disparos[f][c] == 'O') {
            System.out.println("Ya disparaste ahí.");
            return false;
        }

        if (enemigo[f][c] != '.' && enemigo[f][c] != 'X') {
            System.out.println("Impacto en el barco enemigo!");
            char tipo = enemigo[f][c];
            enemigo[f][c] = 'X';
            disparos[f][c] = 'X';

            if (barcoHundido(enemigo, tipo)) {
                String nombre = obtenerNombre(tipo);
                System.out.println("Hundiste el " + nombre + " enemigo!");
            }
        } else {
            System.out.println("Fallaste.");
            disparos[f][c] = 'O';
        }

        if (todosHundidos(enemigo)) {
            System.out.println("\n" + jugador + " ha ganado!");
            return true;
        }

        return false;
    }

    static boolean barcoHundido(char[][] t, char tipo) {
        for (int i = 0; i < TAM; i++)
            for (int j = 0; j < TAM; j++)
                if (t[i][j] == tipo)
                    return false;
        return true;
    }

    static String obtenerNombre(char tipo) {
        for (int i = 0; i < simbolos.length; i++) {
            if (simbolos[i] == tipo)
                return nombres[i];
        }
        return "Barco";
    }

    static boolean todosHundidos(char[][] t) {
        for (int i = 0; i < TAM; i++)
            for (int j = 0; j < TAM; j++)
                if (t[i][j] == 'P' || t[i][j] == 'A' || t[i][j] == 'C' || t[i][j] == 'S' || t[i][j] == 'D')
                    return false;
        return true;
    }
}
