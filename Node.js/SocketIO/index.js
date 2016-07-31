// includes
var express = require('express');
var app = express();
var http = require('http').Server(app);
var io = require('socket.io')(http);

// serve up static files from public directory
app.use(express.static('public'));

// get index.html on root URL load
app.get('/', function(req, res) {
	res.sendFile(__dirname + '/index.html');
});

// socket io connection actions
io.on('connection', function(socket) {
	console.log('a user is connected');
	io.emit('user connected');
	
		socket.on('disconnect', function() {
			io.emit('user disconnected');
			console.log('user disconnected');
		});

		socket.on('chat message', function(msg) {
			io.emit('chat message', msg);
			console.log('message: ' + msg);
		});
});

// set port variable, listen on said port
var port = 8080;

http.listen(port, function() {
	console.log("Server listening on port " + port);
});