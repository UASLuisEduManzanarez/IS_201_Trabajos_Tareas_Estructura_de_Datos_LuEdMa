#include <iostream>
#include <vector>
#include <string>

using namespace std;

int main() {
    
    vector<string> Nombres = {"Luis", "Romina", "Chema", "Carlitos"};

    cout << "Recorrido por el array." << endl;
    for (size_t j = 0; j < Nombres.size(); j++) {
        cout << Nombres[j] << "  ";
    }
    cout << endl;

    string insert;
    cout << "\nInsertar Nombre" << endl;
    cout << "Escribe el nombre a insertar: ";
    getline(cin, insert);  

    Nombres.insert(Nombres.begin() + 1, insert);

    for (size_t j = 0; j < Nombres.size(); j++) {
        cout << Nombres[j] << "  ";
    }
    cout << endl;

    cout << "Busqueda Lineal" << endl;
    string buscarnom;
    cout << "Escribe el nombre que quieres buscar: ";
    getline(cin, buscarnom);

    bool exist = false;
    for (auto j : Nombres) {
        if (j == buscarnom) {
            cout << "Si existe el nombre" << endl;
            exist = true;
            break;
        }
    }

    if (!exist) {
        cout << "No existe el nombre" << endl;
    }

    return 0;
}