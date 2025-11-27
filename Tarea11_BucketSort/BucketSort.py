def insertion_sort(a):
    n = len(a)
    for i in range(1, n):
        temp = a[i]
        j = i - 1
        
        while j >= 0 and temp < a[j]:
            a[j + 1] = a[j]
            j -= 1
        a[j + 1] = temp

def print_arr(arr):
    print(" ".join(map(str, arr)))

def bucket_sort(input_arr):
    s = len(input_arr)
    
    bucket_arr = []
    
    for _ in range(s):
        bucket_arr.append([])

    for j in input_arr:
        bi = int(s * j)
        bucket_arr[bi].append(j) 

    for bukt in bucket_arr:
        insertion_sort(bukt) 

    idx = 0
    for bukt in bucket_arr:
        for j in bukt:
            input_arr[idx] = j
            idx += 1

if __name__ == "__main__":
    
    arr = [0.77, 0.16, 0.28, 0.25, 0.71, 0.93, 0.22, 0.11, 0.24, 0.67]

    print("Arreglo antes de Bucket Sort: ", end="")
    print_arr(arr)

    bucket_sort(arr)

    print("Arreglo después de Bucket Sort: ", end="")
    print_arr(arr)