def bubbleSort(arr, size):
    for i in range(size - 1):
        for j in range(size - i - 1):
            if arr[j] > arr[j + 1]:
                temp = arr[j]
                arr[j] = arr[j + 1]
                arr[j + 1] = temp

def mostrarArreglo(arr, size):
    for i in range(size):
        print(arr[i], end=" ")
    print()

def main():
    arr = [21, 13, 44, 32, 78, 2]
    size = len(arr)

    print("Lista original: ", end="")
    mostrarArreglo(arr, size)

    bubbleSort(arr, size)

    print("Lista ordenada: ", end="")
    mostrarArreglo(arr, size)

if __name__ == "__main__":
    main()
