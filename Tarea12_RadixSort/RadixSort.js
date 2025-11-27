function countingSort(arr, exp) {
    const s = arr.length;
    const outputArray = new Array(s).fill(0);
    const countArray = new Array(10).fill(0);

    for (let j = 0; j < s; j++) {
        const idx = Math.floor((arr[j] / exp) % 10);
        countArray[idx]++;
    }

    for (let j = 1; j < 10; j++)
        countArray[j] += countArray[j - 1];

    for (let j = s - 1; j >= 0; j--) {
        const idx = Math.floor((arr[j] / exp) % 10);
        outputArray[countArray[idx] - 1] = arr[j];
        countArray[idx]--;
    }

    for (let j = 0; j < s; j++)
        arr[j] = outputArray[j];
}

function radixSort(arr) {
    
    const max1 = Math.max.apply(null, arr);
    
    for (let exp = 1; Math.floor(max1 / exp) > 0; exp *= 10)
        countingSort(arr, exp);
}


const arr = [171, 46, 76, 91, 803, 25, 3, 67];

console.log("Arreglo antes de Radix Sort:");
console.log(arr.join(" "));

radixSort(arr);

console.log("Después de Radix Sort:");
console.log(arr.join(" "));