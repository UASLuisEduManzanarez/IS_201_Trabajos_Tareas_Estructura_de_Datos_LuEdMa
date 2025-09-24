#include <iostream>
using namespace std;

int main()
{
    const int TAM = 10;
    int numeros[TAM];

    cout << "Ingrese 10 numeros\n";
    for (int i = 0; i < TAM; i++) {
        cout << "Numero: " << i + 1 << "\n";
        cin >> numeros[i];
    }
    cout << "\n\n";
    cout << "Numeros en el mismo orden: \n";
    for (int i = 0; i < TAM; i++) {
        cout << "Numero: " << i + 1 << "  " << numeros[i] << " \n";
    }
    cout << "\n\n";
    cout << "Numeros en el orden inverso: \n";
    for (int i = TAM - 1; i >= 0; i--) {
        cout << "Numero: " << i + 1 << "  " << numeros[i] << " \n";
    }
    cout << endl;
    return 0;
}


