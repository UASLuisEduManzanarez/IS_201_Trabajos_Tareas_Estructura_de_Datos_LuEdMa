#include <iostream>
using namespace std;

int main() {
    int nums[3][3] = {
        {1, 2, 3},
        {4, 5, 6},
        {7, 8, 9}
    };

    cout << "Matriz:" << endl;
    for (int i = 0; i < 3; i++) {
        cout << "[ ";
        for (int j = 0; j < 3; j++) {
            cout << nums[i][j] << " ";
        }
        cout << "]" << endl;
    }

    cout << "\n Por renglones:" << endl;
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            cout << nums[i][j] << " ";
        }
    }

    cout << "\n Por columnas:" << endl;
    for (int j = 0; j < 3; j++) {
        for (int i = 0; i < 3; i++) {
            cout << nums[i][j] << " ";
        }
    }

    return 0;
}