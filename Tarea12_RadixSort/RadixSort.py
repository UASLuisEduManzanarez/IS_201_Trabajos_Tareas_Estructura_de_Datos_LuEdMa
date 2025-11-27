def counting_sort(arr, exp):
    s = len(arr)
    output_array = [0] * s
    count_array = [0] * 10 

    for j in range(s):
        idx = (arr[j] // exp) % 10 
        count_array[idx] += 1

    for j in range(1, 10):
        count_array[j] += count_array[j - 1]

    for j in range(s - 1, -1, -1):
        idx = (arr[j] // exp) % 10
        output_array[count_array[idx] - 1] = arr[j]
        count_array[idx] -= 1

    for j in range(s):
        arr[j] = output_array[j]

def radix_sort(arr):
    max1 = max(arr)
    
    exp = 1
    while max1 // exp > 0:
        counting_sort(arr, exp)
        exp *= 10

if __name__ == "__main__":
    
    arr = [171, 46, 76, 91, 803, 25, 3, 67]

    print("Arreglo antes de Radix Sort:")
    print(*(arr))

    radix_sort(arr)

    print("Después de Radix Sort:")
    print(*(arr))