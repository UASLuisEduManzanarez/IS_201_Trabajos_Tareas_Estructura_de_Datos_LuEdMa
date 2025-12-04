import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.stream.Collectors;


public class BSTManager {

    // --- 1. Modelo de Datos: Clase Nodo [cite: 16] ---
    private static class Nodo {
        int key;        // int key [cite: 16]
        Nodo left;      // Nodo *left [cite: 16]
        Nodo right;     // Nodo *right [cite: 16]

        public Nodo(int key) {
            this.key = key;
            this.left = null;
            this.right = null;
        }
    }

    // --- 2. Implementación de la Clase BST [cite: 9] ---
    private static class BST {
        private Nodo root;
        private final String FILENAME = "bst_inorder_export.txt";

        // Constructor
        public BST() {
            this.root = null;
        }

        // --- Métodos de Ayuda ---

        private Nodo getMinValueNode(Nodo node) {
            // Encuentra el sucesor inorden (el nodo más a la izquierda)
            Nodo current = node;
            while (current.left != null) {
                current = current.left;
            }
            return current;
        }

        private int heightRecursive(Nodo node) {
            // Calcula la altura del subárbol, donde la altura de un árbol nulo es -1
            if (node == null) {
                return -1;
            }
            int leftHeight = heightRecursive(node.left);
            int rightHeight = heightRecursive(node.right);
            return 1 + Math.max(leftHeight, rightHeight);
        }

        private int sizeRecursive(Nodo node) {
            // Calcula el número de nodos de un subárbol
            if (node == null) {
                return 0;
            }
            return 1 + sizeRecursive(node.left) + sizeRecursive(node.right);
        }

        // --- Operaciones Principales [cite: 17, 9] ---

        public void insert(int key) {
            root = insertRecursive(root, key);
        }

        private Nodo insertRecursive(Nodo root, int key) {
            if (root == null) {
                return new Nodo(key);
            }
            
            if (key < root.key) {
                root.left = insertRecursive(root.left, key);
            } else if (key > root.key) {
                root.right = insertRecursive(root.right, key);
            }
            // Si la clave ya existe, no hacemos nada (no se permiten duplicados)
            return root;
        }

        public SearchResult search(int key) {
            List<Integer> path = new ArrayList<>();
            Nodo current = root;
            
            while (current != null) {
                path.add(current.key); // Registrar la ruta
                if (key == current.key) {
                    return new SearchResult(true, path);
                } else if (key < current.key) {
                    current = current.left;
                } else {
                    current = current.right;
                }
            }
            return new SearchResult(false, path);
        }

        public void delete(int key) {
            root = deleteRecursive(root, key);
        }

        private Nodo deleteRecursive(Nodo root, int key) {
            if (root == null) {
                return root;
            }

            // 1. Recorrer el árbol
            if (key < root.key) {
                root.left = deleteRecursive(root.left, key);
            } else if (key > root.key) {
                root.right = deleteRecursive(root.right, key);
            } else {
                // 2. Nodo encontrado (root.key == key) [cite: 11]

                // Caso 1: Cero o un hijo
                if (root.left == null) {
                    return root.right;
                } else if (root.right == null) {
                    return root.left;
                }

                // Caso 2: Dos hijos [cite: 11]
                Nodo temp = getMinValueNode(root.right); // Obtener sucesor inorden
                root.key = temp.key; // Copiar el contenido
                root.right = deleteRecursive(root.right, temp.key); // Eliminar el sucesor
            }
            return root;
        }

        // --- Recorridos [cite: 10, 18] ---

        public List<Integer> inorder() {
            List<Integer> result = new ArrayList<>();
            inorderRecursive(root, result);
            return result;
        }

        private void inorderRecursive(Nodo node, List<Integer> result) {
            if (node != null) {
                inorderRecursive(node.left, result);
                result.add(node.key);
                inorderRecursive(node.right, result);
            }
        }

        public List<Integer> preorder() {
            List<Integer> result = new ArrayList<>();
            preorderRecursive(root, result);
            return result;
        }

        private void preorderRecursive(Nodo node, List<Integer> result) {
            if (node != null) {
                result.add(node.key);
                preorderRecursive(node.left, result);
                preorderRecursive(node.right, result);
            }
        }

        public List<Integer> postorder() {
            List<Integer> result = new ArrayList<>();
            postorderRecursive(root, result);
            return result;
        }

        private void postorderRecursive(Nodo node, List<Integer> result) {
            if (node != null) {
                postorderRecursive(node.left, result);
                postorderRecursive(node.right, result);
                result.add(node.key);
            }
        }
        
        // --- Utilidad [cite: 18, 19] ---

        public int height() {
            return heightRecursive(root); // La altura de un árbol de un solo nodo es 0
        }

        public int size() {
            return sizeRecursive(root);
        }

        public void exportInorder() {
            try {
                List<Integer> inorderList = inorder();
                // Convertir la lista a una cadena separada por espacios
                String content = inorderList.stream()
                                            .map(String::valueOf)
                                            .collect(Collectors.joining(" "));
                
                FileWriter writer = new FileWriter(FILENAME);
                writer.write(content + "\n");
                writer.close();
                System.out.println(" Recorrido Inorden guardado exitosamente en '" + FILENAME + "'.");
            } catch (IOException e) {
                System.out.println(" Error al exportar Inorden: " + e.getMessage());
            }
        }
    } // Fin de la Clase BST

    // Clase auxiliar para devolver resultados de búsqueda
    private static class SearchResult {
        boolean found;
        List<Integer> path;

        public SearchResult(boolean found, List<Integer> path) {
            this.found = found;
            this.path = path;
        }
    }

    // --- 3. Interfaz de Consola [cite: 20] ---
    public static void main(String[] args) {
        BST bst = new BST();
        Scanner scanner = new Scanner(System.in);
        System.out.println("=== Gestor de Números con Árbol Binario de Búsqueda (Java) ===");
        printHelp();

        while (true) {
            System.out.print("BST > ");
            try {
                String fullCommand = scanner.nextLine().trim();
                if (fullCommand.isEmpty()) {
                    continue;
                }

                String[] parts = fullCommand.toLowerCase().split("\\s+", 2);
                String command = parts[0];
                String argString = parts.length > 1 ? parts[1].trim() : "";

                switch (command) {
                    case "insert":
                        handleInsert(bst, argString);
                        break;
                    case "search":
                        handleSearch(bst, argString);
                        break;
                    case "delete":
                        handleDelete(bst, argString);
                        break;
                    case "inorder":
                    case "preorder":
                    case "postorder":
                        handleTraversal(bst, command); // [cite: 28]
                        break;
                    case "height":
                        System.out.println(" Altura del árbol: " + bst.height()); // [cite: 29]
                        break;
                    case "size":
                        System.out.println(" Número de nodos: " + bst.size()); // [cite: 30]
                        break;
                    case "export":
                        bst.exportInorder(); // [cite: 31]
                        break;
                    case "help":
                        printHelp(); // [cite: 32]
                        break;
                    case "exit":
                        System.out.println(" ¡Adiós! Saliendo del gestor de BST."); // [cite: 34]
                        scanner.close();
                        return;
                    default:
                        System.out.println(" Comando desconocido: '" + command + "'. Usa 'help' para ver la lista de comandos.");
                }
            } catch (Exception e) {
                System.out.println(" Error inesperado: " + e.getMessage());
            }
        }
    }

    private static int parseKey(String argString) throws IllegalArgumentException {
        try {
            return Integer.parseInt(argString);
        } catch (NumberFormatException e) {
            throw new IllegalArgumentException("El argumento debe ser un número entero válido.");
        }
    }

    private static void handleInsert(BST bst, String argString) {
        try {
            int key = parseKey(argString);
            bst.insert(key);
            System.out.println(" Número " + key + " insertado."); // [cite: 25]
        } catch (IllegalArgumentException e) {
            System.out.println(" Error: " + e.getMessage() + " Sintaxis: insert <número>");
        }
    }

    private static void handleSearch(BST bst, String argString) {
        try {
            int key = parseKey(argString);
            SearchResult result = bst.search(key);
            String pathStr = result.path.stream()
                                        .map(String::valueOf)
                                        .collect(Collectors.joining(" -> "));

            if (result.found) {
                System.out.println(" Número " + key + " encontrado.");
                System.out.println("   Ruta de búsqueda: " + pathStr); // [cite: 26]
            } else {
                System.out.println(" Número " + key + " NO encontrado.");
                System.out.println("   Ruta seguida: " + pathStr);
            }
        } catch (IllegalArgumentException e) {
            System.out.println(" Error: " + e.getMessage() + " Sintaxis: search <número>");
        }
    }

    private static void handleDelete(BST bst, String argString) {
        try {
            int key = parseKey(argString);
            // Verificar si el nodo existe antes de intentar la eliminación
            if (!bst.search(key).found) {
                System.out.println(" Error: El número " + key + " no se encuentra en el árbol.");
                return;
            }
            bst.delete(key);
            System.out.println("✅ Número " + key + " eliminado."); // [cite: 27]
        } catch (IllegalArgumentException e) {
            System.out.println(" Error: " + e.getMessage() + " Sintaxis: delete <número>");
        }
    }
    
    private static void handleTraversal(BST bst, String command) {
        List<Integer> result = new ArrayList<>();
        switch (command) {
            case "inorder":
                result = bst.inorder();
                break;
            case "preorder":
                result = bst.preorder();
                break;
            case "postorder":
                result = bst.postorder();
                break;
        }
        String resultStr = result.stream()
                                 .map(String::valueOf)
                                 .collect(Collectors.joining(" "));
        System.out.println(" Recorrido " + command.substring(0, 1).toUpperCase() + command.substring(1) + ": " + resultStr);
    }

    private static void printHelp() {
        System.out.println("\nComandos mínimos disponibles:");
        System.out.printf("  * %-10s - %s\n", "insert <N>", "insertar número. [cite: 25]");
        System.out.printf("  * %-10s - %s\n", "search <N>", "buscar número y mostrar ruta si existe. [cite: 26]");
        System.out.printf("  * %-10s - %s\n", "delete <N>", "eliminar número. [cite: 27]");
        System.out.printf("  * %-10s - %s\n", "inorder", "mostrar recorrido inorden. [cite: 28]");
        System.out.printf("  * %-10s - %s\n", "preorder", "mostrar recorrido preorden. [cite: 28]");
        System.out.printf("  * %-10s - %s\n", "postorder", "mostrar recorrido postorden. [cite: 28]");
        System.out.printf("  * %-10s - %s\n", "height", "mostrar la altura del árbol. [cite: 29]");
        System.out.printf("  * %-10s - %s\n", "size", "mostrar número de nodos. [cite: 30]");
        System.out.printf("  * %-10s - %s\n", "export", "guardar recorrido inorden en archivo. [cite: 31]");
        System.out.printf("  * %-10s - %s\n", "help", "listar comandos. [cite: 32]");
        System.out.printf("  * %-10s - %s\n", "exit", "salir. [cite: 34]");
        System.out.println();
    }
}