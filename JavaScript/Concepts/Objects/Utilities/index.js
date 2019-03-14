"use strict";

/**
 * Creates a deep clone of an object via recursion. 
 * 
 * @param {object} obj Object to clone.
 * @returns {object} Cloned object.
 */
const deepClone = obj => {
  // Use Object.assign() and an empty object ({}) 
  // to create a shallow clone of the original. 
  let clone = Object.assign({}, obj);

  // Use Object.keys() and Array.prototype.forEach() to determine 
  // which key-value pairs need to be deep cloned.
  Object.keys(clone).forEach(
    key => clone[key] = typeof obj[key] === 'object' ? deepClone(obj[key]) : obj[key]
  );

  return Array.isArray(obj) && obj.length ?
    (clone.length = obj.length) && Array.from(clone) :
    Array.isArray(obj) ?
    Array.from(obj) :
    clone;
};

// examples
const a = {
  foo: 'bar',
  obj: {
    a: 1,
    b: 2
  }
};

const b = deepClone(a); // a !== b, a.obj !== b.obj

console.log(b);

/**
 * Deep freezes an object.
 * Calls Object.freeze(obj) recursively on all unfrozen properties 
 * of passed object that are instanceof object.
 * 
 * @param {object} obj Object to recursively freeze.
 * @returns {object} Frozen object.
 */
const deepFreeze = obj =>
  Object.keys(obj).forEach(
    prop =>
    !(obj[prop] instanceof Object) ||
    Object.isFrozen(obj[prop]) ? null : deepFreeze(obj[prop])
  ) || Object.freeze(obj);

// examples
const o = deepFreeze(a);
console.log(Object.isFrozen(o[0])); // true