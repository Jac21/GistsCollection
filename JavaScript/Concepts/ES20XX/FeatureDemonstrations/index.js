"use strict";

// Binary and octal literals
const binaryZero = 0b0;
const binaryOne = 0b1;
const binary255 = 0b11111111;
const binaryLong = 0b111101011101101;

console.log(binary255); // 255

// Number.isNaN()
console.log(Number.isNaN(255)); // false

// Exponent (power) operator
var powerOne = 2 ** 2;
var powerTwo = 3 ** 2;
var powerThree = 3 ** 3;

console.log(powerOne); // 4
console.log(powerTwo); // 9
console.log(powerThree); // 27

// Array.prototype.includes()
console.log([1, 2, 3].includes(2))   // true
console.log([1, 2, 3].includes(true)) // false

// Regex

// Lookbehind matches (match on previous chars)

/*
Look ahead:  (?=abc)
Look behind: (?<=abc)

Look ahead negative:  (?!abc)
Look behind negative: (?<!abc)
*/
const regex = /(?<=\$)\d+/;
const text = 'This cost $400';
console.log(text.match(regex)); // 400

// Named capture groups
const getNameParts = /(?<first>\w+)\s(?<last>\w+)/g;
const name = "Weyland Smithers";
const subMatches = getNameParts.exec(name);

const { first, last } = subMatches.groups
console.log(first) // 'Weyland'
console.log(last) // 'Smithers'

// Array.prototype.flat() & flatMap()
const multiDimensional = [
  [1, 2, 3],
  [4, 5, 6],
  [7, [8, 9]]
];

console.log(multiDimensional.flat(2)); // [1, 2, 3, 4, 5, 6, 7, 8, 9]

// Unbound catches
try {
  // something throws
} catch {
  // don't have to do catch(e)
}

// String trim methods
const padded = '     Hello World   ';
console.log(padded.trimStart()); // 'Hello World   ';
console.log(padded.trimEnd()); // '      Hello World';