"use strict";

// include the Lo-Dash library
var _ = require('lodash');

// take a list of daily average temperatures from different
// towns, sort them into a warm group if they have at least
// one temp greater than 19, hot if greater every day
var everyTemp = function(temps) {
  var results = {
    hot: [],
    warm: []
  };

  function checkTemp(temps) {
    return temps > 19;
  }

  _.forEach(temps, function(town, name) {
    if (_.every(town, checkTemp)) {
      results.hot.push(name);
    } else if (_.some(town, checkTemp)) {
      results.warm.push(name);
    }
  });

  return results;
}

module.exports = everyTemp;
