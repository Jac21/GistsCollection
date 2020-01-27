"use strict";

// We can use the yield keyword in a generator function to 
// "pause" the execution.
function* generatorFunction() {
  yield '✨';
  console.log('First log!');
  yield '🎉'
  console.log('Second log!');
  return 'Done!';
}

const genObj = generatorFunction();

console.log(genObj.next()); // {value: "✨", done: false}
genObj.next(); // First log!
genObj.next(); // Second log!

// iterators
function* getEmojis() {
  yield '🎸';
  yield '🎹';
  yield '🎻';
  yield '🎺';
}

const genObjEmojis = getEmojis();
for (let item of genObjEmojis) {
  console.log(item);
}

// yield* delegate
const words = ['jeremy', 'cantu'];

function* genFunc() {
  yield 'word';
  yield* words;
  yield 'word-2'
}

const genObjThree = genFunc();
console.log([...genObjThree]);