public class persona {
    String nombres;
    String ape1;
    String ape2;

    public static void main(String[] args) {
        persona p = new persona();
        p.nombres = "Luis Eduardo";
        p.ape1 = "Manzanarez";
        p.ape2 = "Ramos";

        System.out.println(p.nombres + " " + p.ape1 + " " + p.ape2);
    }
}