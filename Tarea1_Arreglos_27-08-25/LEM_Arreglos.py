nums = [10, 20, 30, 40, 50]


nums[3] = 55

for n in nums:
    print(n)

size = int(input("¿Cuantos numeros tendra el arreglo?"))
arr = []
for i in range(size):
    valor = int(input(f"Ingresar valor: {i+1}: "))
    arr.append(valor)
print(arr)