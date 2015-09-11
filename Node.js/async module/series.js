"use strict";

var request = require('request'),
	async = require('async'),
	fs = require('fs');

var urlOne, urlTwo;

urlOne = process.argv[2];
urlTwo = process.argv[3];

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