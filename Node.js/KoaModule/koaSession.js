"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// include session module
var session = require('koa-session');

// set keys for signed cookies, session
app.keys = ['secret', 'keys'];
app.use(session(app));

// set incremental signed cookie in the view,
// respond with the number of total views
app.use(function* () {
  var n = ~~this.session.view + 1;
  this.session.view = n;
  this.body = n + ' views';
});

// take port number from
// console input
app.listen(process.argv[2]);
