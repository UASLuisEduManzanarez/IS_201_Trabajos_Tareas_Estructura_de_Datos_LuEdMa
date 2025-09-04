using System;

class persona {
    public string Nombres;
    public string Ape1;
    public string Ape2;
}

class Program {
    static void Main() {
        persona p = new persona();
        p.Nombres = "Luis Eduardo";
        p.Ape1 = "Manzanarez";
        p.Ape2 = "Ramos";

        Console.WriteLine($"{p.Nombres} {p.Ape1} {p.Ape2}");
    }
}