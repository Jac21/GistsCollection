var Hapi = require('hapi');
var server = new Hapi.Server();
var Path = require('path');

var Inert = require('inert');

var Vision = require('vision');

// serve static files
server.register(Inert, function(err) {
	if (err) throw err;
});

// render templates
server.register(Vision, function(err) {
	if (err) throw err;
});

server.connection({
	host: 'localhost',
	port: Number(process.argv[2] || 8080)
});

server.route({path: '/', method: 'GET', handler: {
	view: "index.html"
}});

server.views({
	engines: {
		html: require('handlebars')
	},
	path: Path.join(__dirname, 'templates')
});

server.start(function () {
	console.log('Server running at: ', server.info.uri);
});

