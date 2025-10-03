#include <iostream>
using namespace std;

int main()
{
    const int TAM = 10;
    int numeros[TAM];
    int par = 0;
    int impar =0;
    cout << "EXAMEN 1 EJERCICIO 1: CONTEO DE PARES E IMPARES\n";

    cout << "Ingrese 10 numeros\n";
    for (int i = 0; i < TAM; i++) {
        cout << "Numero " << i + 1 << ": ";
        while (!(cin >> numeros[i])) {
            cout << "Entrada invalida. Por favor, ingrese solo numeros.\n";
            cout << "Numero " << i + 1 << ": ";
            cin.clear();
            cin.ignore(1000, '\n');
        }

        if (numeros[i] % 2 == 0) {
            par += 1;
        } else {
            impar += 1;
        }
    }
    cout << "\n\n";
    cout << "NUMEROS INGRESADOS \n";
    for (int i = 0; i < TAM; i++) {
        cout << numeros[i] << " ";
    }
    cout << "\n\n";
    cout << "Cantidad de numeros pares: " << par;
    cout << "\n\n";
    cout << "Cantidad de numeros impares: " << impar;
   
    cout << endl;
    return 0;
}