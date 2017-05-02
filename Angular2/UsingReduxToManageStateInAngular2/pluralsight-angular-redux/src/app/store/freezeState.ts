// Object.freeze({}) demo
const person = Object.freeze({
  name:"Jeremy",
  surname:"Cantu",
  address: { // IS NOT FROZEN!
    city:"Austin",
    country:"USA"
  }
});

// totally legal
person.address.city = "Somewhere";


// helper function to freeze all objects within
// a parent object that Object.freeze({}) misses
function deepFreeze(o) {
  Object.freeze(o);

  Object.getOwnPropertyNames(o).forEach((prop) => {
    if(
      o.hasOwnProperty(prop)
      && o[prop] != null
      && typeof o[prop] === 'object'
      && !Object.isFrozen(o[prop])) {
        deepFreeze(o[prop]);
      }
  });

  return o;
}

// deepFreeze demo
const frozenPerson = deepFreeze({
  name:"Eskimo",
  surname:"Bob",
  address: { // can't modify
    city:"Igloo",
    country:"Antarctica"
  }
});

export default function freezeState(store) {
    return (next) => (action) => {
        const result = next(action);
        const state = store.getState();
        deepFreeze(state);
        return result;
    }
}