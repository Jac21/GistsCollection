"use strict";

var request = require('request'),
	async = require('async');

//command-line argument variables
var urls = process.argv.slice(2);
//var urls = [process.argv[2], process.argv[3]];

/* call the function multiple times with different inputs,
only want to check if errors pop up without
caring about return data 
*/

async.each(urls, function(url, eachCallback){
	request(url, function(err, response, body) {
			//ignore data, return any errors
			return eachCallback(err);
		});
},
function(err){
	console.log(err);
});