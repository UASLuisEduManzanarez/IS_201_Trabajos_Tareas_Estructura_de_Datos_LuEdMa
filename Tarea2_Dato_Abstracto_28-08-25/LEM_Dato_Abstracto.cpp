#include <iostream>
#include <string>
using namespace std;

struct persona {
    string nombres;
    string ape1;
    string ape2;
};

int main() {
    persona p;
    p.nombres = "Luis Eduardo";
    p.ape1 = "Manzanarez";
    p.ape2 = "Ramos";

    cout << p.nombres << " " << p.ape1 << " " << p.ape2 << endl;
    return 0;
}