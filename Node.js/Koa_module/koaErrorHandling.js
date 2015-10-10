"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// error-handling middleware
app.use(errorHandler());

// error handled routing
app.use(function* (next) {
  if (this.path === '/error') {
    throw new Error('ooops');
  }

  this.body = 'OK';
});

function errorHandler() {
  return function* (next) {
    // try catch all downstream errors here
    try {
      yield next;
    } catch (err) {
      // set response status
      this.status = 500;

      // set response body
      this.body = 'internal server error';

      //can emit on app for log
      this.app.emit('error', err, this);
    }
  };
}

// take port number from
// console input
app.listen(process.argv[2]);
