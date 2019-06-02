"use strict";

// Basic(s)
const obj = { answer: 42 };

const str = JSON.stringify(obj);
console.log(str);

// Deep clone with JSON.parse()
const newObj = {
  question: 'Really?',
  answer: 43
};

const clone = JSON.parse(JSON.stringify(newObj));

console.log(clone);

// Errors and Edge Cases

// Cyclical objects get errors thrown, wrap
// in try-catch
const cyclicalObj = {};
cyclicalObj.prop = cyclicalObj;

try {
  JSON.stringify(cyclicalObj);
} catch (Error) {
  console.error(Error);
};

// edge case
const edgeCaseObjOne = {
  nan: parseInt('not a number'),
  inf: Number.POSITIVE_INFINITY
};

JSON.stringify(edgeCaseObjOne); // '{"nan":null,"inf":null}'

// edge case
const edgeCaseObjTwo = {
  fn: function () { },
  undef: undefined
};

// Empty object. `JSON.stringify()` removes functions and `undefined`.
JSON.stringify(edgeCaseObjTwo); // '{}'

// Pretty Printing
const objWithSpacing = {
  a: 1, b: 2, c: 3, d: { e: 4 }
};

const objWithSpacingStringified = JSON.stringify(objWithSpacing, null, 2);
console.log(objWithSpacingStringified);

// Replacers
const starfleetData = {
  name: 'Jean-Luc Picard',
  password: 'stargazer',
  nested: {
    hashedPassword: 'c3RhcmdhemVy'
  }
};

// '{"name":"Jean-Luc Picard","nested":{}}'
const starfleetDataCleaned = JSON.stringify(starfleetData, function replacer(key, value) {
  // This function gets called 5 times. `key` will be equal to:
  // '', 'name', 'password', 'nested', 'hashedPassword'
  if (key.match(/password/i)) {
    return undefined;
  }
  return value;
});

console.log(starfleetDataCleaned);

// The toJSON() Function
const toJsonObj = {
  name: 'Jean-Luc Picard',
  nested: {
    test: 'not in output',
    toJSON: () => 'test'
  }
};

// '{"name":"Jean-Luc Picard","nested":"test"}'
JSON.stringify(toJsonObj);