#include <iostream>
#include <cstdlib>
#include <vector>
#include <string>
using namespace std;

void insertionSort(vector<float>& a) {
    int n = a.size();
    for (int i = 1; i < n; i++) {
        float temp = a[i]; // float en lugar de int
        int j = i - 1;
        while (j >= 0 && temp < a[j]) {
            a[j + 1] = a[j];
            j--;
        }
        a[j + 1] = temp;
    }
}

void printArr(const vector<float>& arr) {
    for (size_t i = 0; i < arr.size(); ++i) {
        cout << arr[i] << (i == arr.size() - 1 ? "" : " ");
    }
    cout << endl;
}

void bucket_sort(vector<float>& inputArr) {
    int s = inputArr.size();
    
    vector<vector<float>> bucketArr(s);

    for (float j : inputArr) {
        int bi = static_cast<int>(s * j);
        bucketArr[bi].push_back(j);
    }

    for (auto& bukt : bucketArr) {
        insertionSort(bukt); 
    }

    int idx = 0;
    for (const auto& bukt : bucketArr) {
        for (float j : bukt) {
            inputArr[idx] = j;
            idx += 1;
        }
    }
}

int main() {
    vector<float> arr = {0.77f, 0.16f, 0.28f, 0.25f, 0.71f, 0.93f, 0.22f, 0.11f, 0.24f, 0.67f};
    
    cout << "Arreglo antes de Bucket Sort:" << std::endl;
    printArr(arr);
    
    bucket_sort(arr);
    
    cout << "Arreglo despues de Bucket Sort:" << std::endl;
    printArr(arr);
    system("pause");
    return 0;
}