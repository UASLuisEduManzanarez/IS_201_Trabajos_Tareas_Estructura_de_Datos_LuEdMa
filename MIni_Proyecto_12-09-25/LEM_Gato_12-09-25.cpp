#include <iostream>
using namespace std;

bool Ganador(char tablero[3][3], char Jug) {  //Funcion para determinar ganador
    for (int i = 0; i < 3; i++)
        if (tablero[i][0] == Jug && tablero[i][1] == Jug && tablero[i][2] == Jug)  //Recorrer y comparar filas con el jugador
            return true;

    for (int j = 0; j < 3; j++)
        if (tablero[0][j] == Jug && tablero[1][j] == Jug && tablero[2][j] == Jug)  //Recorrer y comparar columnas con el jugador
            return true;
    if (tablero[0][0] == Jug && tablero[1][1] == Jug && tablero[2][2] == Jug) //Recorrer y comparar diagonal de izquierda a derecha
        return true;
    if (tablero[0][2] == Jug && tablero[1][1] == Jug && tablero[2][0] == Jug)  //Recorrer y comparar diagonal de derecha a izquierda
        return true;

    return false;
}

bool Empate(char tablero[3][3]) { //Funcion para determinar empate
    for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
            if (tablero[i][j] != 'X' && tablero[i][j] != 'O') //Recorre la matriz y compara filas y columnas para verificar que ninguno ganó
                return false;
    return true;
}

void MostrarTab(char tablero[3][3]) { //Funcion para mostrar el tablero
    cout << "\n";
    for (int i = 0; i < 3; i++) {
        cout << " ";
        for (int j = 0; j < 3; j++) {
            cout << tablero[i][j];
            if (j < 2) cout << " || "; //Caracteres de diseño para dibujar(verticales)
        }
        cout << "\n";
        if (i < 2) cout << "---++---++---\n";  //Caracteres de diseño para dibujar(horizontales)
    }
    cout << "\n";
}

int main() {
    system("color 05");  //Color moradito
    char again='y';
    do {
        system("cls");  //Limpiar pantalla
        char tablero[3][3] = { {'1','2','3'}, {'4','5','6'}, {'7','8','9'} };  //Definicion de la matriz
        char JugadorAct = 'X';  //Inicializando al jugador actual (X)
        int Casilla;  //Variable para la eleccion de los jugadores

        cout << "----------------------------GATITO---------------------------\n";
        cout << "----------------------------BY LEM---------------------------\n";
        cout << "EMPIEZA EL JUEGO DEL GATITO\n";
        cout << "ESCOGE CASILLA PARA COMENZAR (1-9)\n";

        while (true) {
            MostrarTab(tablero);

            cout << "LE TOCA AL JUGADOR:  " << JugadorAct << ". ESCOGE CASILLA (1-9): ";
            cin >> Casilla;

            if (Casilla < 1 || Casilla > 9) { //Validacion para que solo se ingresen numeros del 1 al 9
                cout << "ESA CASILLA NO EXISTE, selecciona de 1-9\n";
                continue;
            }

            int Fila = (Casilla - 1) / 3;  //8 casillas disponibles al ingresar el movimiento
            int Columna = (Casilla - 1) % 3;  //Lo mismo pero en columnas

            if (tablero[Fila][Columna] == 'X' || tablero[Fila][Columna] == 'O') { //Validacion para saber si la casilla esta ocupada
                cout << " ¡OCUPADA¡ ESCOGE OTRA CASILLA, \n";
                continue; //Volver al inicio del while
            }

            tablero[Fila][Columna] = JugadorAct;

            if (Ganador(tablero, JugadorAct)) { //Muestra al ganador dependiendo de lo seleccionado
                MostrarTab(tablero);
                cout << "VICTORIA PARA: " << JugadorAct << "!\n";
                break;
            }
            else if (Empate(tablero)) { //Mostrar empate
                MostrarTab(tablero);
                cout << "NO GANO NADIE JAJAJA EMPATEEE\n";
                break;
            }

            if (JugadorAct == 'X') { //Alterna el jugador actual, en cada turno (X y 0)
                JugadorAct = 'O';
            }
            else {
                JugadorAct = 'X';
            }
        }
        cout << "¿SE AVIENTAN OTRO JUEGO SI O QUE? y/n"; //Opcion para volver a jugar
        cin >> again;
    } while (again == 'y');
    return 0;
}