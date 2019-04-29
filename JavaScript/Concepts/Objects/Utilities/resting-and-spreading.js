"use strict";

/**
 * Creates a clone of an object, 
 * adds new properties via an object spread. 
 * 
 * @param {object} obj Object to clone.
 * @param {object} addedProperty Property to add.
 * @returns {object} Cloned object with added property.
 */
const deepCloneAndAddProperty = (obj, addedProperty) => {
    // grab object key from added property
    var objectKey = Object.keys(addedProperty)[0];

    // add new property and return
    return {
        ...obj,
        [objectKey]: addedProperty[objectKey]
    };
};

// examples
const user = {
    id: 100,
    name: 'Jeremy Cantu'
};

const userWithPassword = deepCloneAndAddProperty(user, {
    password: 'Password!'
});

console.log(userWithPassword);

/**
 * Merges two objects using the spread operator
 * 
 * @param {object} objOne Object to merge.
 * @param {object} objTwo Second obj to merge.
 * @returns {object} Merged object.
 */
const mergeObjects = (objOne, objTwo) => {
    // merge both objects and return
    return {
        ...objOne,
        ...objTwo
    };
};

// examples
const userOne = {
    id: 100,
    name: 'Jeremy Merged'
};

const userWithPasswordTwo = {
    id: 100,
    password: 'PasswordM3rg3d!'
};

const mergedUserObjects = mergeObjects(userOne, userWithPasswordTwo);

console.log(mergedUserObjects);

/**
 * Dynamically excludes properties using computed object property names
 * 
 * @param {object} prop Property to dynamically remove.
 * @returns {object} Object sans property specified.
 */
const removeProperty = prop => ({
    // dynamic destructuring
    [prop]: _,
    ...rest
}) => rest;

// examples 
const userToRemovePropsFrom = {
    id: 100,
    name: 'Howard Moon',
    password: 'Password!'
};

const removePassword = removeProperty('password');
const removeId = removeProperty('id');

console.log(removePassword(userToRemovePropsFrom)); //=> { id: 100, name: 'Howard Moon' }
console.log(removeId(userToRemovePropsFrom)); //=> { name: 'Howard Moon', password: 'Password!' }

/**
 * Get unique values from an array
 * 
 * @param {array} arr Array to derive unique values from.
 * @returns {array} Return array with unique values.
 */
const createUniqueValuesArrayFromArray = arr => {
    return [...new Set(arr)];
};

// examples 
const arrayWithNonUniqueValues = [1, 2, 3, 3]

const arrayWithUniqueValues = createUniqueValuesArrayFromArray(arrayWithNonUniqueValues);

console.log(arrayWithUniqueValues);
