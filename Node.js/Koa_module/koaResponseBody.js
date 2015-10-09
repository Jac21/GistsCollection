"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// include co-body to parse
// request body
var parse = require('co-body');

// incldue filestream module
var fs = require('fs');

// when 'stream' requested,
// respond with file object
// found in argv[3]
app.use(function* (next) {
  if (this.path !== '/stream') {
    return yield next;
  }

  this.body = fs.createReadStream(process.argv[3]);
});


// if json, respond with
// {foo : 'bar'}
app.use(function* (next) {
  if (this.path !== '/json') {
    return yield next;
  }

  this.body = {foo : 'bar'};
});

app.use(function* (next) {
  if (this.path !== '/404') {
    return yield next;
  }

  this.body = 'page not found';
});

app.use(function* (next) {
  if (this.path !== '/500') {
    return yield next;
  }

  this.body = 'internal server error'
});

// take port number from
// console input
app.listen(process.argv[2]);
