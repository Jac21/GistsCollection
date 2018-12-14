'use strict';

// The function declaration/definition/statement
function foo() {
  console.log("Basic function declaration");
};

// The function expression
const myFunctionExpression = function () {
  console.log("Function expression syntax. If you try to reference a function expression before calling it, your code will fail.");
};

// The arrow function expression
() => console.log("Arrow function expression");

// The function constructor
const myStrangeFunc = new Function("a", "console.log(a + ' with function constructors')");
myStrangeFunc("Fun"); // logs --> "Fun with Functions"

// The generator function expression.
function* generator(i) {
  yield i;
  yield i + 10;
}

var gen = generator(10);

console.log(gen.next().value);
// expected output: 10

console.log(gen.next().value);
// expected output: 20

// IIFE's and the anonymous function
(function () {
  console.log("IIFE!");
})();

// calls
foo();
myFunctionExpression();