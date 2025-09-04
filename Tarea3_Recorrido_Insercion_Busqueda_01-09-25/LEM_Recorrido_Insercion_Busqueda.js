
let Nombres = ["Luis", "Romina", "Chema", "Carlitos"];

console.log("Recorrido por el array.");
for (let j = 0; j < Nombres.length; j++) {
    process.stdout.write(Nombres[j] + "  ");
}
console.log("\n");

const readline = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

readline.question("Escribe el nombre a insertar: ", function(insert) {
    Nombres.splice(1, 0, insert);

    for (let j = 0; j < Nombres.length; j++) {
        process.stdout.write(Nombres[j] + "  ");
    }
    console.log("\n");

    readline.question("Escribe el nombre que quieres buscar: ", function(buscarnom) {
        let exist = false;
        for (let j of Nombres) {
            if (j === buscarnom) {
                console.log("Si existe el nombre");
                exist = true;
                break;
            }
        }

        if (!exist) {
            console.log("No existe el nombre");
        }

        readline.close();
    });
});
