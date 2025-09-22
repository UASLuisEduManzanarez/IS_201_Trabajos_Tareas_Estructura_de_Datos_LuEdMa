#include <iostream>
#include <cstdlib>  
#include <ctime>    

using namespace std;

void bubbleSort(int arr[], int n) {
    for (int i = 0; i < n; i++) {
        bool swap = false;
        for (int j = 0; j < n - i - 1; j++) {
            if (arr[j] > arr[j + 1]) {
                int temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
                swap = true;
            }
        }
        if (!swap) {
            break; 
        }
    }
}

int main() {
    const int SIZE = 10;
    int arr[SIZE];

    srand(time(0)); 
    for (int i = 0; i < SIZE; i++) {
        arr[i] = rand() % 100 + 1;
    }

    cout << "Arreglo original: ";
    for (int i = 0; i < SIZE; i++) {
        cout << arr[i] << " ";
    }
    cout << endl;

    bubbleSort(arr, SIZE);

    cout << "Arreglo ordenado: ";
    for (int i = 0; i < SIZE; i++) {
        cout << arr[i] << " ";
    }
    cout << endl;

    return 0;
}
