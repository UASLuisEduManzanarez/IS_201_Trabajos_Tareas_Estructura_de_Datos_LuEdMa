nums = [
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9]
]

print("Matriz:")
for i in range(3):
    fila = "[ "
    for j in range(3):
        fila += str(nums[i][j]) + " "
    fila += "]"
    print(fila)

print("\nPor renglones:")
for i in range(3):
    for j in range(3):
        print(nums[i][j], end=" ")
print()

print("\nPor columnas:")
for j in range(3):
    for i in range(3):
        print(nums[i][j], end=" ")
print()