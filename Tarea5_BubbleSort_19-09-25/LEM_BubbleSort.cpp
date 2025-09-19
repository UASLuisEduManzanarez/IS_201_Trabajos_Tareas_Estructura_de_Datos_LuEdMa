#include <iostream>
using namespace std;

void bubbleSort(int arr[], int size) {
    for (int i = 0; i < size - 1; ++i) {
        for (int j = 0; j < size - i - 1; ++j) {
            if (arr[j] > arr[j + 1]) {
                int temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

void mostrarArreglo(int arr[], int size) {
    for (int i = 0; i < size; ++i) {
        cout << arr[i] << " ";
    }
    cout << endl;
}

int main() {
    int arr[] = {21, 13, 44, 32, 78, 2};
    int size = sizeof(arr) / sizeof(arr[0]);

    cout << "Lista original: ";
    mostrarArreglo(arr, size);

    bubbleSort(arr, size);

    cout << "Lista ordenada: ";
    mostrarArreglo(arr, size);

    return 0;
}