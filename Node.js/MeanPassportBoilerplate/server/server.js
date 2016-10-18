#!/usr/bin/env node

/*
	Assistance with setting up authentication backend:
		http://mherman.org/blog/2015/07/02/handling-user-authentication-with-the-mean-stack/#.V_9UQr-DnCR
		http://mherman.org/blog/2015/01/31/local-authentication-with-passport-and-express-4/#.VZCK9xNViko
*/
var debug = require('debug')('passport-mongo');
var app = require('./app');


app.set('port', process.env.PORT || 3000);


var server = app.listen(app.get('port'), function() {
  debug('Express server listening on port ' + server.address().port);
});
