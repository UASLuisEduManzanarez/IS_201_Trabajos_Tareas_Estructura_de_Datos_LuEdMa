function bubbleSort(arr, size) {
    for (let i = 0; i < size - 1; i++) {
        for (let j = 0; j < size - i - 1; j++) {
            if (arr[j] > arr[j + 1]) {
                let temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

function mostrarArreglo(arr, size) {
    for (let i = 0; i < size; i++) {
        process.stdout.write(arr[i] + " ");
    }
    console.log();
}

function main() {
    let arr = [21, 13, 44, 32, 78, 2];
    let size = arr.length;

    process.stdout.write("Lista original: ");
    mostrarArreglo(arr, size);

    bubbleSort(arr, size);

    process.stdout.write("Lista ordenada: ");
    mostrarArreglo(arr, size);
}

main();
