"use-strict";

// Perfomance.now
const timeStampZero = performance.now();

for (let i = 0; i < 1000; i++) {
  i += i
};

const timeStampOne = performance.now();
console.log(timeStampOne - timeStampZero, 'milliseconds');

// Console.time
console.time('test');

for (let i = 0; i < 1000; i++) {
  i += i;
}

console.timeEnd('test');