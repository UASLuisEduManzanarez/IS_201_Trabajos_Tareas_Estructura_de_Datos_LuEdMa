#include <iostream>
#include <cstdlib> 
#include <ctime>  

using namespace std;

int main() {
    srand(time(0));

    const int x = 3;
    const int y = 3;
    const int z = 3; 

    int cubo[x][y][z];
    
    cout << "EXAMEN 1 EJERCICIO 3 BUSCAR VALOR (3x3x3)";
    cout << "CUBO CON NUMEROS ALEATORIOS" << endl;
    for (int i = 0; i < x; ++i) {
        for (int j = 0; j < y; ++j) {
            for (int k = 0; k < z; ++k) {
                cubo[i][j][k] = 1 + rand() % 1000;
            }
        }
    }

    cout << "\nCubo Generado (por capas)" << endl;
    for (int k = 0; k < z; ++k) {
        cout << "Capa " << k + 1 << ":" << endl;
        for (int i = 0; i < x; ++i) {
            for (int j = 0; j < y; ++j) {
                cout << cubo[i][j][k] << "\t";
            }
            cout << endl;
        }
        cout << endl; 
    }

    int search;
    cout << "\nIngrese el numero que desea buscar en la matriz: ";
    while (!(cin >> search)) {
        cout << "Entrada invalida. Por favor, ingrese solo numeros.\n";
        cout << "Ingrese el numero que desea buscar en la matriz: ";
        cin.clear();
        cin.ignore(1000, '\n');
    }

    bool encontrado = false;

    for (int i = 0; i < x; ++i) {
        for (int j = 0; j < y; ++j) {
            for (int k = 0; k < z; ++k) {
                if (cubo[i][j][k] == search) {
                    encontrado = true;
                    break;
                }
            }
            if (encontrado) {
                break;
            }
        }
        if (encontrado) {
            break;
        }
    }

    cout << "\n--- Resultado ---" << endl;
    if (encontrado) {
        cout << "El numero " << search << " SI se encontro en la matriz." << endl;
    } else {
        cout << "El numero " << search << " NO se encontro en la matriz." << endl;
    }

    return 0;
}
