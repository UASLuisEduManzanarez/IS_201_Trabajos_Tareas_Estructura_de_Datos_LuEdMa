// Función para ajustar el subárbol con raíz en el índice i
function heapify(arr, n, i) {
    let largest = i;         // Suponemos que la raíz es el mayor
    let left = 2 * i + 1;    // Hijo izquierdo
    let right = 2 * i + 2;   // Hijo derecho

    // Si el hijo izquierdo existe y es mayor que la raíz
    if (left < n && arr[left] > arr[largest])
        largest = left;

    // Si el hijo derecho existe y es mayor que el mayor actual
    if (right < n && arr[right] > arr[largest])
        largest = right;

    // Si el mayor no es la raíz, intercambiamos y seguimos ajustando recursivamente
    if (largest !== i) {
        [arr[i], arr[largest]] = [arr[largest], arr[i]]; // Intercambio

        heapify(arr, n, largest); // Llamada recursiva
    }
}

// Función principal de heapsort
function heapSort(arr) {
    let n = arr.length;

    // Construir heap máximo
    for (let i = Math.floor(n / 2) - 1; i >= 0; i--)
        heapify(arr, n, i);

    // Extraer elementos uno por uno
    for (let i = n - 1; i > 0; i--) {
        // Mover la raíz (máximo) al final
        [arr[0], arr[i]] = [arr[i], arr[0]];

        // Llamar heapify en el heap reducido
        heapify(arr, i, 0);
    }
}

// Ejemplo de uso
let arr = [15, 3, 66, 6, 44, 50];

console.log("Antes de ordenar los elementos del arreglo:", arr);
heapSort(arr);
console.log("Después de ordenar el arreglo:", arr);
