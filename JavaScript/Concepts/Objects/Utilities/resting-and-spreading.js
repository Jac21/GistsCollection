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