"use strict";

// include the Lo-Dash library
var _ = require('lodash');

// pass in a collection of items, sort by the quantity
// amount of each item in descending order
var sortItems = function(items) {
  return _.sortBy(items, 'quantity').reverse();
};

module.exports = sortItems;
