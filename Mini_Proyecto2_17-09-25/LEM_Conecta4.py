import os

FILAS = 6
COLUMNAS = 7

# Función para mostrar el tablero
def mostrar_tablero(tablero):
    print("\n   ", end="")
    for c in range(COLUMNAS):
        print(f"{c+1}   ", end="")
    print()

    for i in range(FILAS):
        print(" | ", end="")
        for j in range(COLUMNAS):
            if tablero[i][j] == 'R':
                print("\033[31mO\033[0m | ", end="")  # Rojo
            elif tablero[i][j] == 'A':
                print("\033[33mO\033[0m | ", end="")  # Amarillo
            else:
                print(". | ", end="")  # Vacío
        print()
    print()

# Función para colocar ficha en una columna
def colocar_ficha(tablero, col, jugador):
    if col < 0 or col >= COLUMNAS:
        return False, None
    for i in range(FILAS-1, -1, -1):
        if tablero[i][col] == '.':
            tablero[i][col] = jugador
            return True, i
    return False, None

# Función para verificar ganador
def ganador(tablero, fila, col, jugador):
    direcciones = [
        (0, 1),   # horizontal
        (1, 0),   # vertical
        (1, 1),   # diagonal \
        (1, -1)   # diagonal /
    ]
    for cambio_fila, cambio_columna in direcciones:
        conteo = 1
        # Hacia un lado
        x, y = fila + cambio_fila, col + cambio_columna
        while 0 <= x < FILAS and 0 <= y < COLUMNAS and tablero[x][y] == jugador:
            conteo =conteo + 1
            x = x + cambio_fila
            y = y + cambio_columna
        # Hacia el otro lado
        x, y = fila - cambio_fila, col - cambio_columna
        while 0 <= x < FILAS and 0 <= y < COLUMNAS and tablero[x][y] == jugador:
            conteo = conteo + 1
            x = x - cambio_fila
            y = y - cambio_columna
        if conteo >= 4:
            return True
    return False

# Función para verificar empate
def empate(tablero):
    for j in range(COLUMNAS):
        if tablero[0][j] == '.':
            return False
    return True

def main():
    again = 'y'
    while again.lower() == 'y':
        os.system('cls' if os.name == 'nt' else 'clear')  # Limpiar pantalla
        tablero = [['.' for _ in range(COLUMNAS)] for _ in range(FILAS)]
        jugador_act = 'R'

        print("----------------------------CONECTA 4---------------------------")
        print("----------------------------BY LEM------------------------------")
        print("EMPIEZA EL JUEGO DEL CONECTA 4")
        print("ESCOGE UNA COLUMNA (1-7)")

        while True:
            mostrar_tablero(tablero)
            try:
                col = int(input(f"LE TOCA AL JUGADOR: {'Rojo (O)' if jugador_act == 'R' else 'Amarillo (O)'} . ESCOGE COLUMNA (1-7): "))
            except ValueError:
                print("POR FAVOR INGRESA UN NÚMERO VÁLIDO")
                continue

            if col < 1 or col > COLUMNAS:
                print("ESA COLUMNA NO EXISTE, selecciona de 1-7")
                continue

            exito, fila_colocada = colocar_ficha(tablero, col - 1, jugador_act)
            if not exito:
                print("ESA COLUMNA ESTA LLENA, ESCOGE OTRA")
                continue

            if ganador(tablero, fila_colocada, col - 1, jugador_act):
                mostrar_tablero(tablero)
                print(f"VICTORIA PARA EL JUGADOR {'ROJO' if jugador_act == 'R' else 'AMARILLO'}!")
                break
            elif empate(tablero):
                mostrar_tablero(tablero)
                print("NO GANO NADIE, EMPATEEE")
                break

            jugador_act = 'A' if jugador_act == 'R' else 'R'

        again = input("¿SE AVIENTAN OTRO JUEGO SI O QUE? y/n: ")

if __name__ == "__main__":
    main()