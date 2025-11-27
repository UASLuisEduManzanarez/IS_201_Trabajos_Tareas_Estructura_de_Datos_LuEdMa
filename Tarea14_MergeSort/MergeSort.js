function merge(a, l, m, r) {
    const a1 = m - l + 1;
    const a2 = r - m;

    const L = new Array(a1);
    const R = new Array(a2);

    for (let i = 0; i < a1; i++)
        L[i] = a[l + i];
    for (let j = 0; j < a2; j++)
        R[j] = a[m + 1 + j];

    let i = 0, j = 0, k = l;

    while (i < a1 && j < a2) {
        if (L[i] <= R[j]) {
            a[k] = L[i];
            i++;
        } else {
            a[k] = R[j];
            j++;
        }
        k++;
    }

    while (i < a1) {
        a[k] = L[i];
        i++;
        k++;
    }

    while (j < a2) {
        a[k] = R[j];
        j++;
        k++;
    }
}

function mergeSort(a, l, r) {
    if (l < r) {
        const m = Math.floor(l + (r - l) / 2);
        mergeSort(a, l, m);
        mergeSort(a, m + 1, r);
        merge(a, l, m, r);
    }
}


const a = [39, 28, 44, 11];

console.log("Antes de Merge Sort:");
console.log(a.join(" "));

mergeSort(a, 0, a.length - 1);

console.log("Después de Merge Sort:");
console.log(a.join(" "));