var Hapi = require('hapi');
var server = new Hapi.Server();
var Path = require('path');
var fs = require('fs');
var rot13 = require('rot13-transform');

var Inert = require('inert');
var Vision = require('vision');
var H2o2 = require('h2o2');
var Joi = require('joi');

// serve static files
server.register(Inert, function(err) {
	if (err) throw err;
});

// render templates
server.register(Vision, function(err) {
	if (err) throw err;
});

// configure proxies
server.register(H2o2, function(err) {
	if (err) throw err;
});

server.connection({
	host: 'localhost',
	port: Number(process.argv[2] || 8080)
});

var routeConfig = {
	path: '/chickens/{breed}',
	method: 'GET',
	handler: function(request, reply) {
		reply()
	},
	config: {
		validate: {
			params: {
				with: Joi.string().required(),
				parameters: Joi.string().required
			}
		}
	}
}

server.route({path: '/chickens/{breed}', method: 'GET', handler: function(request, reply) {
	path: routeConfig
}});

server.views({
	engines: {
		html: require('handlebars')
	},
	path: Path.join(__dirname, 'templates'),
	helpersPath: Path.join(__dirname, 'helpers')
});

server.start(function () {
	console.log('Server running at: ', server.info.uri);
});

