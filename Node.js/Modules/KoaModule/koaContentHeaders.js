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

app.use(function* (next) {
  if (this.request.is('application/json')) {
    this.body = {message : 'hi!'};
  } else {
    this.body = 'ok';
  };
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
