function selection(a) {
    for (let i = 0; i < a.length; i++) {
        let small = i; // índice del elemento más pequeño
        for (let j = i + 1; j < a.length; j++) {
            if (a[small] > a[j]) {
                small = j; // actualiza el índice del elemento más pequeño
            }
        }
        // intercambia los elementos
        let temp = a[i];
        a[i] = a[small];
        a[small] = temp;
    }
}

function printArr(a) {
    let result = "";
    for (let i = 0; i < a.length; i++) {
        result += a[i] + " ";
    }
    console.log(result.trim());
}

let a = [99, 34, 32, 78, 55];

console.log("Arreglo antes de ser ordenado:");
printArr(a);

selection(a);

console.log("Arreglo después de ser ordenado:");
printArr(a);
