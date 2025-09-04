class persona:
    def __init__(self, nombres, ape1, ape2):
        self.nombres = nombres
        self.ape1 = ape1
        self.ape2 = ape2

# Uso
p = persona("Luis Eduardo", "Manzanarez", "Ramos")
print(p.nombres, p.ape1, p.ape2)