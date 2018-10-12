// http://bigspaceship.github.io/blog/2014/05/14/how-to-create-a-rest-api-with-node-dot-js/

// import express, mongoose, fs modules
var express = require('express');
var mongoose = require('mongoose');
var fs = require('fs');

// init MongoDB and Mongoose connections
var mongoUri = 'mongodb://localhost/noderest';
mongoose.connect(mongoUri);
var db = mongoose.connection;
db.on('error', function() {
	throw new Error('Unable to connect to database at ' + mongoUri);
});

var app = express();

app.configure(function() {
	app.use(express.bodyParser());
});

// import routes and models
require('./models/musicians');
require('./routes')(app);

// set port, start
var port = 3001;
app.listen(port);
console.log("App started on port " + port + "!");