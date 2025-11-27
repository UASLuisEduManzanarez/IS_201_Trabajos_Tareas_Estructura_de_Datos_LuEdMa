def merge(a, l, m, r):
    a1 = m - l + 1
    a2 = r - m

    L = [0] * a1
    R = [0] * a2

    for i in range(a1):
        L[i] = a[l + i]
    for j in range(a2):
        R[j] = a[m + 1 + j]

    i, j, k = 0, 0, l

    while i < a1 and j < a2:
        if L[i] <= R[j]:
            a[k] = L[i]
            i += 1
        else:
            a[k] = R[j]
            j += 1
        k += 1

    while i < a1:
        a[k] = L[i]
        i += 1
        k += 1

    while j < a2:
        a[k] = R[j]
        j += 1
        k += 1

def merge_sort(a, l, r):
    if l < r:
        m = l + (r - l) // 2
        merge_sort(a, l, m)
        merge_sort(a, m + 1, r)
        merge(a, l, m, r)

if __name__ == "__main__":
    a = [39, 28, 44, 11]

    print("Antes de Merge Sort:")
    print(*(a))

    merge_sort(a, 0, len(a) - 1)

    print("Después de Merge Sort:")
    print(*(a))