const nums = [
  [1, 2, 3],
  [4, 5, 6],
  [7, 8, 9]
];

console.log("Matriz:");
for (let i = 0; i < 3; i++) {
  let fila = "[ ";
  for (let j = 0; j < 3; j++) {
    fila += nums[i][j] + " ";
  }
  fila += "]";
  console.log(fila);
}

console.log("\nPor renglones:");
let porRenglones = "";
for (let i = 0; i < 3; i++) {
  for (let j = 0; j < 3; j++) {
    porRenglones += nums[i][j] + " ";
  }
}
console.log(porRenglones);

console.log("\nPor columnas:");
let porColumnas = "";
for (let j = 0; j < 3; j++) {
  for (let i = 0; i < 3; i++) {
    porColumnas += nums[i][j] + " ";
  }
}
console.log(porColumnas);