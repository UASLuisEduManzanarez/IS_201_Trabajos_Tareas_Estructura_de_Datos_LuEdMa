import tkinter as tk
from tkinter import ttk
import time
import random
import math
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg

# --- 1. Implementación de Algoritmos de Ordenamiento en Python (Sin Cambios) ---

def bubble_sort(arr):
    n = len(arr)
    for i in range(n):
        intercambio = False
        for j in range(0, n - i - 1):
            if arr[j] > arr[j + 1]:
                arr[j], arr[j + 1] = arr[j + 1], arr[j]
                intercambio = True
        if not intercambio:
            break
    return arr

def insertion_sort(arr):
    n = len(arr)
    for i in range(1, n):
        key = arr[i]
        j = i - 1
        while j >= 0 and arr[j] > key:
            arr[j + 1] = arr[j]
            j = j - 1
        arr[j + 1] = key
    return arr

def selection_sort(arr):
    n = len(arr)
    for i in range(n - 1):
        min_idx = i
        for j in range(i + 1, n):
            if arr[j] < arr[min_idx]:
                min_idx = j
        arr[min_idx], arr[i] = arr[i], arr[min_idx]
    return arr

def merge_sort(arr):
    if len(arr) <= 1:
        return arr
    
    mid = len(arr) // 2
    left = merge_sort(arr[:mid])
    right = merge_sort(arr[mid:])
    
    return merge(left, right)

def merge(left, right):
    result = []
    left_index, right_index = 0, 0
    
    while left_index < len(left) and right_index < len(right):
        if left[left_index] < right[right_index]:
            result.append(left[left_index])
            left_index += 1
        else:
            result.append(right[right_index])
            right_index += 1
            
    result.extend(left[left_index:])
    result.extend(right[right_index:])
    return result

def quick_sort_util(items, left, right):
    if len(items) > 1:
        pivot = items[(left + right) // 2]
        i = left
        j = right
        while i <= j:
            while items[i] < pivot:
                i += 1
            while items[j] > pivot:
                j -= 1
            if i <= j:
                items[i], items[j] = items[j], items[i]
                i += 1
                j -= 1
        
        if left < j:
            quick_sort_util(items, left, j)
        if i < right:
            quick_sort_util(items, i, right)
    return items

def quick_sort(arr):
    return quick_sort_util(arr[:], 0, len(arr) - 1)

def heapify(arr, n, i):
    largest = i
    left = 2 * i + 1
    right = 2 * i + 2

    if left < n and arr[left] > arr[largest]:
        largest = left
    if right < n and arr[right] > arr[largest]:
        largest = right
    
    if largest != i:
        arr[i], arr[largest] = arr[largest], arr[i]
        heapify(arr, n, largest)

def heap_sort(input_arr):
    arr = input_arr[:]
    n = len(arr)
    
    for i in range(n // 2 - 1, -1, -1):
        heapify(arr, n, i)
        
    for i in range(n - 1, 0, -1):
        arr[i], arr[0] = arr[0], arr[i]
        heapify(arr, i, 0)
    return arr

def shell_sort(arr):
    n = len(arr)
    gap = n // 2
    while gap > 0:
        for i in range(gap, n):
            temp = arr[i]
            j = i
            while j >= gap and arr[j - gap] > temp:
                arr[j] = arr[j - gap]
                j -= gap
            arr[j] = temp
        gap //= 2
    return arr

def bucket_sort(input_arr):
    if len(input_arr) == 0:
        return input_arr[:]
    
    arr = input_arr[:]
    min_val = min(arr)
    max_val = max(arr)
    
    if min_val == max_val:
        return arr
    
    bucket_count = len(arr)
    buckets = [[] for _ in range(bucket_count)]
    
    val_range = max_val - min_val
    
    for val in arr:
        index = math.floor(((val - min_val) / val_range) * (bucket_count - 1))
        buckets[index].append(val)
        
    sorted_arr = []
    for bucket in buckets:
        insertion_sort(bucket)
        sorted_arr.extend(bucket)
        
    return sorted_arr

def counting_sort_for_radix(arr, exp):
    n = len(arr)
    output = [0] * n
    count = [0] * 10

    for i in range(n):
        index = (arr[i] // exp) % 10
        count[index] += 1

    for i in range(1, 10):
        count[i] += count[i - 1]

    i = n - 1
    while i >= 0:
        index = (arr[i] // exp) % 10
        output[count[index] - 1] = arr[i]
        count[index] -= 1
        i -= 1

    for i in range(n):
        arr[i] = output[i]

def radix_sort(input_arr):
    if len(input_arr) == 0:
        return input_arr[:]
    
    arr = input_arr[:]
    max_val = max(arr)
    
    exp = 1
    while max_val // exp > 0:
        counting_sort_for_radix(arr, exp)
        exp *= 10
        
    return arr

# --- 2. Generación de Arreglos de Prueba (Sin Cambios) ---

def generar_arreglo(tamano, tipo):
    arr = [random.randint(0, tamano * 10) for _ in range(tamano)]
    
    if tipo == "Ordenado":
        arr.sort()
    elif tipo == "Inversos":
        arr.sort(reverse=True)
    elif tipo == "Medianamente Ordenado":
        arr.sort()
        swaps = int(tamano * 0.1)
        for _ in range(swaps):
            idx1 = random.randint(0, tamano - 1)
            idx2 = random.randint(0, tamano - 1)
            arr[idx1], arr[idx2] = arr[idx2], arr[idx1]
    
    return arr

# --- 3. Constantes de la Simulación (Sin Cambios) ---

ALGORITMOS = [
    {"nombre": "Bubble Sort", "fn": bubble_sort},
    {"nombre": "Insertion Sort", "fn": insertion_sort},
    {"nombre": "Selection Sort", "fn": selection_sort},
    {"nombre": "Merge Sort", "fn": merge_sort},
    {"nombre": "Quick Sort", "fn": quick_sort},
    {"nombre": "Heap Sort", "fn": heap_sort},
    {"nombre": "Shell Sort", "fn": shell_sort},
    {"nombre": "Bucket Sort", "fn": bucket_sort},
    {"nombre": "Radix Sort", "fn": radix_sort}
]

ALGORITMOS_N2 = ["Bubble Sort", "Insertion Sort", "Selection Sort"]
TAMANO_LIMITE_N2 = 20000 

ESCENARIOS_DATA = {
    "Tamaños": [100, 1000, 10000, 100000],
    "Tipos": ["Ordenado", "Medianamente Ordenado", "Inversos"]
}

# Generar la lista de claves de escenarios como antes, para guardar los resultados
ESCENARIOS_KEYS = [
    f"Tamaño: {t}, Tipo: {s}" 
    for t in ESCENARIOS_DATA["Tamaños"] 
    for s in ESCENARIOS_DATA["Tipos"]
]

# --- 4. Lógica de la Interfaz Gráfica (Tkinter) ---

class ComparadorAlgoritmos(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title("Proyecto de Ordenamiento (9 Algoritmos)")
        self.geometry("1000x750")
        self.todos_los_resultados = {}
        self.fig, self.ax = plt.subplots(figsize=(8, 5))
        self.canvas = None
        
        self.estilos_ui()
        self.crear_widgets()
        self.configurar_selectores_iniciales()

    def estilos_ui(self):
        style = ttk.Style(self)
        style.theme_use('clam')
        style.configure('TButton', font=('Helvetica', 12), padding=10)
        style.configure('TLabel', font=('Helvetica', 10), padding=5)
        style.configure('TCombobox', font=('Helvetica', 12), padding=5)

    def crear_widgets(self):
        # Frame Principal
        main_frame = ttk.Frame(self, padding="15 15 15 15")
        main_frame.pack(fill='both', expand=True)
        
        # Título
        ttk.Label(main_frame, text="Proyecto de Ordenamiento (9 Algoritmos)", font=('Helvetica', 18, 'bold')).pack(pady=5)
        
        # Descripción
        desc_text = "Presiona el botón para ejecutar las 12 pruebas (4 tamaños x 3 pre-órdenes).\n"
        desc_text += "Nota: Esto puede tardar varios minutos. Mira la consola (Terminal) para ver el progreso."
        ttk.Label(main_frame, text=desc_text, anchor='w').pack(fill='x', pady=5)
        
        # Botón de Ejecución
        self.run_button = ttk.Button(main_frame, text="Ejecutar Pruebas y Recopilar Datos", command=self.iniciar_pruebas, style='TButton')
        self.run_button.pack(pady=10)
        
        # Frame para Selectores y Botón de Actualización
        selector_btn_frame = ttk.Frame(main_frame)
        selector_btn_frame.pack(pady=10)
        self.selector_btn_frame = selector_btn_frame # Guardar referencia para ocultar/mostrar
        
        # Selector de Tamaño
        ttk.Label(selector_btn_frame, text="Tamaño (N):").pack(side=tk.LEFT, padx=(0, 5))
        self.tamano_selector = ttk.Combobox(selector_btn_frame, state='readonly', width=12)
        self.tamano_selector['values'] = [str(t) for t in ESCENARIOS_DATA["Tamaños"]]
        self.tamano_selector.pack(side=tk.LEFT, padx=5)

        # Selector de Tipo
        ttk.Label(selector_btn_frame, text="Pre-Orden:").pack(side=tk.LEFT, padx=(10, 5))
        self.tipo_selector = ttk.Combobox(selector_btn_frame, state='readonly', width=20)
        self.tipo_selector['values'] = ESCENARIOS_DATA["Tipos"]
        self.tipo_selector.pack(side=tk.LEFT, padx=5)
        
        # Botón para Actualizar la Vista
        self.actualizar_btn = ttk.Button(selector_btn_frame, text="Actualizar Gráfico y Tabla", command=self.actualizar_vista_evento)
        self.actualizar_btn.pack(side=tk.LEFT, padx=20)
        
        # Ocultar inicialmente el frame de selectores
        selector_btn_frame.pack_forget()

        # Contenedor de Gráfico
        chart_frame = ttk.Frame(main_frame, relief='groove', padding=10)
        chart_frame.pack(pady=20, fill='x')
        self.ax.set_title("Gráfico de Rendimiento (vacío)")
        self.canvas = FigureCanvasTkAgg(self.fig, master=chart_frame)
        self.canvas_widget = self.canvas.get_tk_widget()
        self.canvas_widget.pack(fill='both', expand=True)

        # Contenedor de Resultados (Tabla)
        self.results_frame = ttk.Frame(main_frame)
        self.results_frame.pack(pady=10, fill='x')

    def configurar_selectores_iniciales(self):
        """Configura el valor inicial de los ComboBox."""
        if ESCENARIOS_DATA["Tamaños"]:
            self.tamano_selector.set(str(ESCENARIOS_DATA["Tamaños"][0]))
        if ESCENARIOS_DATA["Tipos"]:
            self.tipo_selector.set(ESCENARIOS_DATA["Tipos"][0])
        
    def iniciar_pruebas(self):
        self.run_button.config(state=tk.DISABLED, text="Ejecutando... (mira la consola)")
        self.selector_btn_frame.pack_forget() # Ocultar mientras se ejecuta
        self.todos_los_resultados = {}
        
        # Limpiar resultados anteriores
        for widget in self.results_frame.winfo_children():
            widget.destroy()
        
        self.ax.clear()
        self.ax.set_title("Gráfico de Rendimiento (Ejecutando...)")
        self.canvas.draw()
        
        self.after(50, self.ejecutar_pruebas_en_hilo_principal)

    def ejecutar_pruebas_en_hilo_principal(self):
        print("Iniciando pruebas...")
        
        escenarios_a_probar = [
            {"tamano": t, "tipo": s} 
            for t in ESCENARIOS_DATA["Tamaños"] 
            for s in ESCENARIOS_DATA["Tipos"]
        ]
        
        for i, escenario in enumerate(escenarios_a_probar):
            tamano, tipo = escenario["tamano"], escenario["tipo"]
            key = f"Tamaño: {tamano}, Tipo: {tipo}"
            print(f"\n--- Ejecutando Escenario: {key} ({i+1}/{len(escenarios_a_probar)}) ---")
            
            arr_original = generar_arreglo(tamano, tipo)
            resultados_escenario = []
            
            for algo in ALGORITMOS:
                nombre = algo["nombre"]
                fn = algo["fn"]
                
                if nombre in ALGORITMOS_N2 and tamano > TAMANO_LIMITE_N2:
                    print(f"  Omitiendo {nombre} para {tamano} elementos (demasiado lento).")
                    resultados_escenario.append({"metodo": nombre, "tiempo": float('inf')})
                    continue
                
                arr_test = arr_original[:]
                
                t0 = time.perf_counter()
                fn(arr_test) 
                t1 = time.perf_counter()
                
                tiempo = (t1 - t0) * 1000
                print(f"  {nombre}: {tiempo:.4f} ms")
                
                resultados_escenario.append({"metodo": nombre, "tiempo": tiempo})
                self.update_idletasks()

            self.todos_los_resultados[key] = resultados_escenario
            self.update_idletasks()

        print("\nPruebas completadas.")
        
        self.run_button.config(state=tk.NORMAL, text="Ejecutar Pruebas y Recopilar Datos")
        
        # Mostrar el frame de selectores después de la ejecución
        self.selector_btn_frame.pack(pady=10)
        
        # Actualizar la vista con el primer escenario por defecto
        self.actualizar_vista_evento()

    def actualizar_vista_evento(self):
        """Función que recupera las selecciones y llama a actualizar_vista."""
        tamano = self.tamano_selector.get()
        tipo = self.tipo_selector.get()
        escenario_key = f"Tamaño: {tamano}, Tipo: {tipo}"
        self.actualizar_vista(escenario_key)

    def actualizar_vista(self, escenario_key):
        """Actualiza la tabla y el gráfico para el escenario seleccionado."""
        if escenario_key not in self.todos_los_resultados:
            # Esto puede ocurrir si se intenta actualizar antes de la ejecución o con una clave inválida
            for widget in self.results_frame.winfo_children():
                widget.destroy()
            self.ax.clear()
            self.ax.set_title(f"Gráfico de Rendimiento (Datos no encontrados para: {escenario_key})")
            self.canvas.draw()
            return

        resultados = self.todos_los_resultados[escenario_key]
        resultados_ordenados = sorted(resultados, key=lambda x: x['tiempo'])

        # --- Generar Tabla de Resultados ---
        for widget in self.results_frame.winfo_children():
            widget.destroy()
            
        ttk.Label(self.results_frame, text=f"Resumen: {escenario_key} (Mejor a Peor)", font=('Helvetica', 14, 'bold')).pack(pady=5)
        
        # Crear la tabla usando Treeview
        tree = ttk.Treeview(self.results_frame, columns=('Posicion', 'Metodo', 'Tiempo'), show='headings')
        tree.heading('Posicion', text='Posición (Ranking)')
        tree.heading('Metodo', text='Método')
        tree.heading('Tiempo', text='Tiempo (ms)')
        
        tree.column('Posicion', width=100, anchor='center')
        tree.column('Metodo', width=200, anchor='w')
        tree.column('Tiempo', width=200, anchor='e')

        for index, res in enumerate(resultados_ordenados):
            tiempo_str = f"{res['tiempo']:.4f}" if res['tiempo'] != float('inf') else "N/A (Omitido por O(n^2))"
            tree.insert('', 'end', values=(index + 1, res['metodo'], tiempo_str), tags=('striped', 'normal'))
            
        tree.tag_configure('striped', background='#fdfdfd')
        tree.pack(fill='x', expand=False)

        # --- Generar Gráfico de Barras ---
        datos_grafico = [r for r in resultados if r['tiempo'] != float('inf')]
        datos_grafico.sort(key=lambda x: x['metodo'])
        
        metodos = [r['metodo'] for r in datos_grafico]
        tiempos = [r['tiempo'] for r in datos_grafico]
        
        self.ax.clear()
        
        colores_mpl = plt.cm.Set1.colors 
        bar_colors = colores_mpl[:len(metodos)]
        
        self.ax.bar(metodos, tiempos, color=bar_colors, edgecolor='black')
        
        self.ax.set_title(f"Comparativa de Rendimiento para:\n{escenario_key}", fontsize=14)
        self.ax.set_ylabel('Tiempo (ms)')
        self.ax.tick_params(axis='x', rotation=45)
        self.fig.tight_layout()
        self.canvas.draw()

if __name__ == "__main__":
    app = ComparadorAlgoritmos()
    app.mainloop()