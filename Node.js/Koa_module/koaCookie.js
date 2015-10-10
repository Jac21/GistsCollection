"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// set keys for signed cookies
app.keys = ['secret', 'keys'];

// set incremental signed cookie in the view,
// respond with the number of total views
app.use(function* () {
  var n = ~~this.cookies.get('view', {signed : true}) + 1;
  this.cookies.set('view', n, {signed : true});
  this.body = n + ' views';
});

// take port number from
// console input
app.listen(process.argv[2]);
