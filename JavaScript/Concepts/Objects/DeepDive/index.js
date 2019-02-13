"use strict";

// Accessor Properties
const accessorObj = {
  get name() {
    return this._name;
  },
  set name(value) {
    this._name = value;
  },
  get id() {
    return this._id.toString(2); // Returns binary of a number
  },
  set id(value) {
    this._id = value;
  }
};

accessorObj.name = "Jeremy";
console.log(`Accessor Object get name: ${accessorObj.name}`);

accessorObj.id = "21";
console.log(`Accessor Object get id: ${accessorObj.id}`);

// Object Property Descriptors
const object = {
  x: 5,
  y: 6
};

console.log(Object.getOwnPropertyDescriptor(object, 'x'));

/* 
{ 
  value: 5, 
  writable: true, 
  enumerable: true, 
  configurable: true 
}
*/

// Working with Descriptors
const obj = {};

Object.defineProperty(obj, 'id', {
  value: 42
});
console.log(obj);
console.log(obj.id);

Object.defineProperty(obj, 'name', {
  value: 'Jeremy',
  writable: false,
  enumerable: true,
  configurable: true
});

console.log(Object.keys(obj));

// Protecting Objects
const protectedObj = {
  id: 43
};

Object.preventExtensions(protectedObj);
console.log(`protectedObj with 'preventExtensions' applied isExtensible? => ${Object.isExtensible(protectedObj)}`);

const sealedObj = {
  id: 44
};

Object.seal(sealedObj);
console.log(`sealedObj with 'seal' applied isExtensible? => ${Object.isExtensible(sealedObj)}`);

const frozenObj = {
  id: 45
};

Object.freeze(frozenObj);
console.log(`frozenObj with 'freeze' applied isFrozen? => ${Object.isFrozen(frozenObj)}`);
