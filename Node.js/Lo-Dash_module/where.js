"use strict";

// include the Lo-Dash library
var _ = require('lodash');

// find the "active" members in a collection of users,
// return that object
// [{"id" : 22, "username" : "example", "active" : true}]
var findActive = function(users) {
  return _.where(users, {
    active: true
  });
};

module.exports = findActive;
