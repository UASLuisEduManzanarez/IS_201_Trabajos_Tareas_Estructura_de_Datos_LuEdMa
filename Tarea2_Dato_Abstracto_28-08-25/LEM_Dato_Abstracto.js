class persona {
  constructor(nombres, ape1, ape2) {
    this.nombres = nombres;
    this.ape1 = ape1;
    this.ape2 = ape2;
  }
}

// Uso
let p = new persona("Luis Eduardo", "Manzanarez", "Ramos");
console.log(p.nombres, p.ape1, p.ape2);