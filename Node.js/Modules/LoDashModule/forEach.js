"use strict";

// include the Lo-Dash library
var _ = require("lodash");

// pass in a collection of cities with a population
// value, sort through each city and add a size
//property based on the population size 
var forEachSize = function(collection) {
    return _.forEach(collection, function(value, index, collection) {
        var size;

        if (value.population > 1) {
            size = "big";
        } else if (value.population > 0.5 && value.population < 1) {
            size = "med";
        } else {
            size = "small";
        }

        value.size = size;
        collection[index] = value;
    });
};

module.exports = forEachSize;
