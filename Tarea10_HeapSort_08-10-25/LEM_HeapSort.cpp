#include <iostream>
using namespace std;

void heapify(int arr[], int n, int i) {
    int largest = i;        
    int left = 2 * i + 1;   
    int right = 2 * i + 2; 


    if (left < n && arr[left] > arr[largest])
        largest = left;

    if (right < n && arr[right] > arr[largest])
        largest = right;

    if (largest != i) {
        int temp = arr[i];
        arr[i] = arr[largest];
        arr[largest] = temp;

        heapify(arr, n, largest);
    }
}

void heapsort(int arr[], int n) {
    for (int i = n / 2 - 1; i >= 0; i--)
        heapify(arr, n, i);

    for (int i = n - 1; i > 0; i--) {
        int temp = arr[0];
        arr[0] = arr[i];
        arr[i] = temp;

        heapify(arr, i, 0);
    }
}

int main() {
    int arr[] = {15, 3, 66, 6, 44, 50};
    int n = sizeof(arr) / sizeof(arr[0]);

    cout << "Antes de ordenar los elementos del arreglo: ";
    for (int i = 0; i < n; i++)
        cout << arr[i] << " ";
    cout << endl;

    heapsort(arr, n);

    cout << "\nDespués de ordenar el arreglo: ";
    for (int i = 0; i < n; i++)
        cout << arr[i] << " ";
    cout << endl;

    return 0;
}
