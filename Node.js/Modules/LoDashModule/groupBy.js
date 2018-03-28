"use strict";

// include the Lo-Dash library
var _ = require('lodash');

var findCommentCount = function(comments) {
  var counted = [];

  comments = _.groupBy(comments, 'username');

  _.forEach(comments, function(item, name) {
    counted.push({
      username : name,
      comment_count : _.size(item)
    });
  });

  return _.sortBy(counted, "comment_count").reverse();
};

module.exports = findCommentCount;
