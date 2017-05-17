"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// include co-body to parse
// request body
var parse = require('co-body');

// instead of router, koa can check
// specific path, or skip that
// particular middleware (return yield)
app.use(function* (next) {
  if (this.method !== 'POST') {
    return yield next;
  }

  // max body size limit at 1 kilobyte
  var body = yield parse(this, {
    limit: '1kb'
  });

  // if body.name doesn't exist
  if (!body.name) this.throw(400, '.name required');

  // return body.name in all caps
  this.body = body.name.toUpperCase();
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
