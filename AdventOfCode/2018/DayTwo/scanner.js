'use strict';

// import the file-system and path modules
const fs = require('fs');
const path = require('path');

// sort and return an array with groupings of repeated characters
const assembleRepetitionsArray = (str) => {
    const repetitionsArray =
        str.toLowerCase()
        .split("")
        .sort()
        .join("")
        .match(/(.)\1+/g);

    return repetitionsArray;
};

// boolean-return function to determine if assembled array
// contains an entry with a specified character length
const anyContainSpecifiedLength = (arr, characterLength) => {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].length === characterLength) {
            return true;
        }
    }

    return false;
};

const scanner = {
    baseDir: path.join(__dirname, './input')
};

scanner.calculateChecksum = (file) => {
    let twoCharacterOccurenceCount = 0;
    let threeCharacterOccurenceCount = 0;

    fs.readFileSync(`${scanner.baseDir}/${file}.txt`, 'utf8')
        .toString()
        .split('\n')
        .forEach(function (line) {
            var tempLineArray = assembleRepetitionsArray(line);

            if (tempLineArray !== null) {
                if (anyContainSpecifiedLength(tempLineArray, 2)) {
                    twoCharacterOccurenceCount += 1;
                }

                if (anyContainSpecifiedLength(tempLineArray, 3)) {
                    threeCharacterOccurenceCount += 1;
                }
            }
        });

    return twoCharacterOccurenceCount * threeCharacterOccurenceCount;
};

scanner.determineCorrectBoxCharacters = (file) => {
    let stringArray = fs
        .readFileSync(`${scanner.baseDir}/${file}.txt`, 'utf8')
        .toString()
        .split('\n');

    for (let i = 0; i < stringArray.length; i++) {
        for (let j = i + 1; j < stringArray.length; j++) {
            const spreadCharactersOne = [...stringArray[i]]
            const spreadCharactersTwo = [...stringArray[j]]

            // find best box
            let diff = spreadCharactersOne.
            reduce((a, c, i) =>
                a + (c === spreadCharactersTwo[i] ? 0 : 1), 0)

            if (diff === 1) {
                console.log(stringArray[i]);
                console.log(stringArray[j]);
            }
        }
    }
};

console.log(scanner.calculateChecksum('input'));
scanner.determineCorrectBoxCharacters('input');