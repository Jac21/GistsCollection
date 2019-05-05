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

// Conditionally add properties into an object literal
const condition = false;

const objToConditionallyAddTo = {
  ...condition && {
    prop: value
  },
};

// example
const formValues = {
  state: 'The State'
};

const state = formValues['state']; // 'The State'
const priority = formValues['priority']; // undefined

const query = {
  collection: 'Users',
  sort: 'ASC',
  ...state && {
    state
  },
  ...priority && {
    priority
  }
};

console.log(query); // {collection: "Users", sort: "ASC", state: "The State"}

// Create empty object

let emptyObject = Object.create(null);

// dict.__proto__ === "undefined"
// No object properties exist until you add them

console.log(emptyObject);

// Get query string parameters
// Assuming "?post=1234&action=edit"

var urlParams = new URLSearchParams(window.location.search);

console.log(urlParams.has('post')); // true
console.log(urlParams.get('action')); // "edit"
console.log(urlParams.getAll('action')); // ["edit"]
console.log(urlParams.toString()); // "?post=1234&action=edit"
console.log(urlParams.append('active', '1')); // "?post=1234&action=edit&active=1"