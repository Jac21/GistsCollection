"use strict";

// include the Lo-Dash library
var _ = require('lodash');

// take array of words, append the word "Chained to each",
// convert to upper case, and sort by alphabetical order
var chainWords = function(collection) {
  return _.chain(collection).map(function(item) {
      return item + 'Chained';
    }).map(function(item) {
      return item.toUpperCase();
    })
    .sortBy()
    .value();
};

module.exports = chainWords;
