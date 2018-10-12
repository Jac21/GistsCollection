var Hapi = require('hapi');
var server = new Hapi.Server();

server.connection({
	host: 'localhost',
	port: Number(process.argv[2] || 8080)
});

server.route({
	path: '/upload',
	method: 'POST',
	config: {
			handler: function(request, reply) {
				var body = '';
				request.payload.file.on('data', function(data) {
					body += data
				});

				request.payload.file.on('end', function() {
					var result = {
						description: request.payload.description,
						file: {
							data: body,
							filename: request.payload.file.hapi.filename,
							headers: request.payload.file.hapi.headers
						}
					};
					reply(JSON.stringify(result));
				});
		},
		payload: {
			output: 'stream',
			parse: true,
			allow: 'multipart/form-data'
		}
	}
});

server.start(function () {
	console.log('Server running at: ', server.info.uri);
});

