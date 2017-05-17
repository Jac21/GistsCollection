"use strict";

// include the Lo-Dash library
var _ = require('lodash');

// take a simple variable of userinfo, and output a
// simple greeting with the username and number of
// total logins, e.g., 'Hello Jeremy (logins: 5)'
var templateGreeting = function(userInfo) {
  return _.template('Hello <%= name %> (logins: <%= login.length %>)')(userInfo);
};

module.exports = templateGreeting;
