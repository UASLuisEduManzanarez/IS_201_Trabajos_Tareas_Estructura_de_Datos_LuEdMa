class ShellSort {
    
    static displayArr(inputArr) {
        console.log(inputArr.join(" "));
    }

    sort(inputArr) {
        const size = inputArr.length;
        
        for (let gapsize = Math.floor(size / 2); gapsize > 0; gapsize = Math.floor(gapsize / 2)) {
            for (let j = gapsize; j < size; j++) {
                const val = inputArr[j];
                let k = j;
                while (k >= gapsize && inputArr[k - gapsize] > val) {
                    inputArr[k] = inputArr[k - gapsize];
                    k = k - gapsize;
                }
                inputArr[k] = val;
            }
        }
    }
}

// --- Ejecución ---

const inputArr = [36, 34, 43, 11, 15, 20, 28, 45];

process.stdout.write("Arreglo antes de HashSort(Shell) \n");
ShellSort.displayArr(inputArr);

const obj = new ShellSort();
obj.sort(inputArr);

process.stdout.write("Arreglo después HashSort(Shell) \n");
ShellSort.displayArr(inputArr);