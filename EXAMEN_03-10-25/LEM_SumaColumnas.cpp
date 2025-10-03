#include <iostream>
using namespace std;

int main() {
    
    const int x = 3;
    const int y = 3;

    int matriz[x][y];

    cout << "EXAMEN 1 EJERCICIO 2: SUMA COLUMNAS(3X3)\n" << endl;
    cout << "Ingresar numeros: " << endl;

    for (int i = 0; i < x; ++i) {
        for (int j = 0; j < y; ++j) {
            cout << "Fila: " << i + 1 << ", Columna " << j + 1 << ": ";
            while (!(cin >> matriz[i][j])) {
                cout << "Entrada invalida. Por favor, ingrese solo numeros.\n";
                cout << "Fila: " << i + 1 << ", Columna " << j + 1 << ": ";
                cin.clear();
                cin.ignore(1000, '\n');
            }
        }
    }

    cout << "\nMatriz ingresada:" << endl;
    for (int i = 0; i < x; ++i) {
        for (int j = 0; j < y; ++j) {
            cout << matriz[i][j] <<"\t"; 
        }
        cout << endl;
    }

    cout << "\n" << endl;

    for (int j = 0; j < y; ++j) {
        int sumaColumna = 0;
        for (int i = 0; i < x; ++i) {
            sumaColumna += matriz[i][j];
        }
        cout << "Suma columna: " << j + 1 << " = " << sumaColumna << endl;
    }

    return 0;
}
