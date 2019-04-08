'use strict';

/*
console.log()
*/

// Ability to use values, much like sprintf
console.log('This is very %s like!', 'C');

// %o for objects
let person = {
  age: 25,
  gender: 'Male',
  disposition: 'Fair',
  gpa: 3.7
};

console.log('The state object for %s is %o', 'Jeremy', person);

// %d for decimals
console.log('Age for the above? %d.', person.age);

// %f for floating-point numbers
console.log('GPA for the above? %f', person.gpa);

/*
console.dir()
*/

// outputs elements structure in an object-like fashion
let element = document.getElementById('secondary-header');

console.dir(element);

/*
console.warn()
*/

// outputs styled warning log statement to console, can be filtered in DevTools
console.warn('This is a plain console.warn() call!');

/* 
Info & debug
*/
console.info("This is very informative!");
console.debug("Debugging a bug!");

/*
console.table()
*/

const transactions = [{
    id: "7cb1-e041b126-f3b8",
    seller: "WAL0412",
    buyer: "WAL3023",
    price: 203450,
    time: 1539688433
  },
  {
    id: "1d4c-31f8f14b-1571",
    seller: "WAL0452",
    buyer: "WAL3023",
    price: 348299,
    time: 1539688433
  },
  {
    id: "b12c-b3adf58f-809f",
    seller: "WAL0012",
    buyer: "WAL2025",
    price: 59240,
    time: 1539688433
  }
];

// much more useful output for raw objects
console.table(transactions);

// can also filter on what columns to display
console.table(transactions, ['id', 'price']);

// can only handle maximum of 1000 rows, can be sorted in the DevTools

/*
console.assert()
*/

// logs, but only in the case where the first argument is falsey
console.assert(person.age === 26);

/*
console.count()
*/

// simply acts as a named counter
for (let i = 0; i < 10; i++) {
  if (i % 2) {
    console.count('odds');
  }
  if (!(i % 5)) {
    console.count('multiplesOfFive');
  }
}

console.countReset('odds');

/*
console.trace()
*/

let addTwo = function (a, b) {
  // output the stacktrace that got us to this point
  console.trace();
  return a + b;
}

let addTwoWrapper = function (a, b) {
  return addTwo(a, b);
}

addTwoWrapper(5, 5);

/*
console.time()
*/

let addThree = function (a, b, c) {
  return a + b + c;
}

console.time();

for (let i = 0; i < 1000; ++i) {
  addThree(i, i + 1, i + 2);
}
console.timeEnd();

/*
console.group()
*/

class MyClass {
  constructor(dataAccess) {
    console.group('Constructor');

    console.log('Constructor executed');

    console.assert(typeof dataAccess === 'object',
      'Potentially incorrect dataAccess object');

    this.initializeEvents();

    console.groupEnd();
  }

  initializeEvents() {
    console.group('events');
    console.log('Initialising events');
    console.groupEnd();
  }
}

let myClass = new MyClass(false);

/* 
CSS 
*/

console.log("Example %cCSS-styled%c %clog!",
  "color: red; font-family: monoscope;",
  "", "color: green; font-size: large; font-weight: bold");