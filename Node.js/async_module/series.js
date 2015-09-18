"use strict";

var request = require('request'),
	async = require('async'),
	fs = require('fs');

// command line args
var urlOne, urlTwo;

urlOne = process.argv[2];
urlTwo = process.argv[3];

/*
Similar to waterfall function, although data processed
from function to function will not be passed once it
completed, collects all results in an array and pass
it to an optional callback that runs after all
functions have completed
*/

async.series({
	requestOne: function(seriesCallback){
		request(urlOne, function(err, response, body) {
			if (err) {
				return console.log(err);
			}

			return seriesCallback(err, body);
		});
	},
	requestTwo: function(seriesCallback){
		request(urlTwo, function(err, response, body) {
			if (err) {
				return console.log(err);
			}

			return seriesCallback(err, body);
		});
	}
}, function(err, results){
	if (err) { return console.log(err); }
	console.log(results);
});