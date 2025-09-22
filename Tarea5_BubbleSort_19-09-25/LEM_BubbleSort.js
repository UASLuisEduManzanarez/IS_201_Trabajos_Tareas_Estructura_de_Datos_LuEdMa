function bubbleSort(arr) {
    let n = arr.length;
    for (let i = 0; i < n; i++) {
        let swap = false;
        for (let j = 0; j < n - i - 1; j++) {
            if (arr[j] > arr[j + 1]) {
                let temp = arr[j];
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

let arr = Array.from({ length: 10 }, () => Math.floor(Math.random() * 100) + 1);

console.log("Arreglo original:", arr);

bubbleSort(arr);

console.log("Arreglo ordenado:", arr);
