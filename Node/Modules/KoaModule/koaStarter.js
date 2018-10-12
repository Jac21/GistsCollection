"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// handler that can anonymously
// generate functions, prints
// 'hello koa'
app.use(function* () {
  this.body = 'hello koa';
});

// take port number from
// console input
app.listen(process.argv[2]);
