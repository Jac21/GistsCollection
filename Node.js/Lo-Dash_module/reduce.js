"use strict";

// include the Lo-Dash library
var _ = require('lodash');

// take in array of recording orders, calculate
// total number of orders for each article,
// sort by largest number
var calculateOrders = function(orders) {
  var orderArray = [],
    total = 0;

  orders = _.groupBy(orders, 'article');

  _.forEach(orders, function(item, key) {

    // parse for value literal
    key = parseInt(key);

    // reset total
    total = 0;

    // if only 1 article
    if (item.length === 1) {
      total = item[0].quantity;
    } else {
      total = _.reduce(item, function(sum, item) {
        return sum + item.quantity;
      }, 0);
    }

    orderArray.push({
      article: key,
      total_orders: total
    });
  });

  return _.sortBy(orderArray, "total_orders").reverse();
};

module.exports = calculateOrders;
