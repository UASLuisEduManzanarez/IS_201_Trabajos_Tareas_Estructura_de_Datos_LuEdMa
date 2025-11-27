function insertionSort(a) {
    const n = a.length;
    for (let i = 1; i < n; i++) {
        const temp = a[i];
        let j = i - 1;
        
        while (j >= 0 && temp < a[j]) {
            a[j + 1] = a[j];
            j--;
        }
        a[j + 1] = temp;
    }
}

function printArr(arr) {
    console.log(arr.join(" "));
}

function bucketSort(inputArr) {
    const s = inputArr.length;
    
    const bucketArr = [];
    
    for (let i = 0; i < s; i++) {
        bucketArr.push([]);
    }

    for (const j of inputArr) {
        const bi = Math.floor(s * j);
        bucketArr[bi].push(j); 
    }

    for (const bukt of bucketArr) {
        insertionSort(bukt); 
    }

    let idx = 0;
    for (const bukt of bucketArr) {
        for (const j of bukt) {
            inputArr[idx] = j;
            idx += 1;
        }
    }
}



const arr = [0.77, 0.16, 0.28, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67];

process.stdout.write("Arreglo antes de Bucket Sort: ");
printArr(arr);

bucketSort(arr);

process.stdout.write("Arreglo después de Bucket Sort: ");
printArr(arr);