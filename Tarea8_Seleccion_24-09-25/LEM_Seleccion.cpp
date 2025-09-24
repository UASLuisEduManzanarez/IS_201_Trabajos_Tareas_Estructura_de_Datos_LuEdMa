#include <iostream>
using namespace std;

void selection(int a[], int n) { 
    for (int i = 0; i < n; i++) {
        int small = i; 
        for (int j = i + 1; j < n; j++) {
            if (a[small] > a[j]) {
                small = j;
            }
        }
    
        int temp = a[i];
        a[i] = a[small];
        a[small] = temp;
    }
}

void printArr(int a[], int n) { 
    for (int i = 0; i < n; i++) {
        cout << a[i] << " ";
    }
    cout << endl;
}

int main() {
    int a[] = { 99, 34, 32, 78, 55 };
    int n = sizeof(a) / sizeof(a[0]);

    cout << "Arreglo antes de ser ordenado: " << endl;
    printArr(a, n);

    selection(a, n);

    cout << "Arreglo despues de ser ordenado: " << endl;
    printArr(a, n);

    return 0;
}
