const readline = require('readline');

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

let nums = [10, 20, 30, 40, 50];

console.log("Primer Elemento:", nums[0]);

nums[3] = 55;

console.log("arreglo inicial:");
for (let n of nums) {
  console.log(n);
}

rl.question("¿Cuantos números?: ", (answer) => {
  let Size = parseInt(answer);
  let arr = [];
  let i = 0;

  function pedirNum() {
    if (i < Size) {
      rl.question(`Ingresar Valor: ${i + 1}: `, (valor) => {
        arr.push(parseInt(valor));
        i++;
        pedirNum();
      });
    } else {
      console.log("valores ingresados:", arr);
      rl.close();
    }
  }

  pedirNum();
});