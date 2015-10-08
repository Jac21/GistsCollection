"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// instead of router, koa can check
// specific path, or skip that
// particular middleware (return yield)
app.use(function* (next) {
  if (this.path !== '/') {
    return yield next;
  }

  this.body = 'hello koa';
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
