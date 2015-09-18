"use strict";

var request = require('request'),
	async = require('async');

//command-line argument variables
var urls = process.argv.slice(2);
//var urls = [process.argv[2], process.argv[3]];

/*
Calls an asynchronous iterator function on an array,
collects the results and passes them to the callback
*/

async.map(urls, function(url, eachCallback){
	request(url, function(err, response, body) {
			//ignore data, return any errors
			return eachCallback(err, body);
		});
},
function(err, results){
	if (err) { return console.log(err); }
	console.log(results);
});