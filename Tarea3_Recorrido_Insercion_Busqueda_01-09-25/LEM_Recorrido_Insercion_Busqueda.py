Nombres = ["Luis", "Romina", "Chema", "Carlitos"]

print("Recorrido por el array.")
for j in range(len(Nombres)):
    print(Nombres[j], end="  ")

print("\nInsertar Nombre")
insert = input("\nEscribe el nombre a insertar ") 
Nombres.insert(1, insert)
print()

for j in range(len(Nombres)):
    print(Nombres[j], end="  ")

print("Busqueda Lineal")
buscarnom = input("Escribe el nombre que quieres buscar: ") 
exist = False 

for j in Nombres:
    if j == buscarnom:
        print("Si existe el nombre")
        exist = True
        break
    
if exist == False:
    print("No existe el nombre")