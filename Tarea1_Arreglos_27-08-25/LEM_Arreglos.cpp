#include <iostream>
#include <vector>
using namespace std;

int main() {
    int nums[5] = {10, 20, 30, 40, 50};

    cout << "Primer elemento: " << nums[0] << endl;

    nums[3] = 55;

    cout << "Array: ";
    for(int i = 0; i < 5; i++) {
        cout << nums[i] << " ";
    }
    cout << endl;

    int n;
    cout << "¿Cuantos numeros?: ";
    cin >> n;

    vector<int> arr(n); 
    for(int i = 0; i < n; i++) {
        cout << "Ingresar valor: " << (i+1) << ": ";
        cin >> arr[i];
    }

    cout << "Valores ingresados: ";
    for(int i = 0; i < n; i++) {
        cout << arr[i] << " ";
    }
    cout << endl;

    return 0;
}