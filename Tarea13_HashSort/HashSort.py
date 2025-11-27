class ShellSort:
    
    @staticmethod
    def display_arr(input_arr):
        print(" ".join(map(str, input_arr)))

    def sort(self, input_arr):
        size = len(input_arr)
        
        gapsize = size // 2
        while gapsize > 0:
            for j in range(gapsize, size):
                val = input_arr[j]
                k = j
                while k >= gapsize and input_arr[k - gapsize] > val:
                    input_arr[k] = input_arr[k - gapsize]
                    k = k - gapsize
                input_arr[k] = val
            gapsize //= 2

if __name__ == "__main__":
    input_arr = [36, 34, 43, 11, 15, 20, 28, 45]
    print("Arreglo antes de HashSort(Shell) ")
    ShellSort.display_arr(input_arr)

    obj = ShellSort()
    obj.sort(input_arr)

    print("Arreglo después HashSort(Shell) ")
    ShellSort.display_arr(input_arr)