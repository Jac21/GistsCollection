"use strict";

var request = require('request'),
	async = require('async'),
	fs = require('fs');

//takes an array of functions
async.waterfall([
	//read a file and pull the URL from the file
	//passing it to the 2nd function
	function(waterfallCallback) {
		fs.readFile(process.argv[2], function(err, data) {
			if (err) {
				return waterfallCallback(err);
			}

			//file read success
			return waterfallCallback(null, data.toString());
		});
	},
	//make an HTTP GET request to that URL
	function(url, waterfallCallback) {
		request(url, function(err, response, body) {
			if (err) {
				return waterfallCallback(err);
			}

			//send the body to the next function
			return waterfallCallback(null, body);
		});
	}
	//final function
	], function(err, result){
		if(err) {
			return console.error(err);
		}
		console.log(result);
	});