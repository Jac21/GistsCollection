var Hapi = require('hapi');
var server = new Hapi.Server();

var Inert = require('inert');

// serve static files
server.register(Inert, function(err) {
	if (err) throw err;
})

server.connection({
	host: 'localhost',
	port: Number(process.argv[2] || 8080)
});

server.route({path: '/', method: 'GET', handler: {
	file: "index.html"
}});

server.start(function () {
	console.log('Server running at: ', server.info.uri);
});

