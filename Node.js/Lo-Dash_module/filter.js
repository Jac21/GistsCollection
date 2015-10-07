"use strict";

// include the Lo-Dash library
var _ = require('lodash');

// take an array of freelancers and their incomes,
// filter the under and overperformers on the
// average income of each freelancer
var calculateAverageIncome = function(freelancers) {
  var average,
    underperform,
    overperform;

  freelancers = _.sortBy(freelancers, "income");

  average = _.reduce(freelancers, function(sum, num) {
    return sum + num.income;
  }, 0);

  average = average / freelancers.length;

  // filter on under and overperformers via average
  underperform = _.filter(freelancers, function(num) {
    return num.income <= average;
  });

  overperform = _.filter(freelancers, function(num) {
    return num.income > average;
  });

  return {
    average: average,
    underperform: underperform,
    overperform: overperform
  };

};

module.exports = calculateAverageIncome;
