"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// setup views
var views = require('co-views');

var render = views(__dirname + '/views', {
  ext: 'ejs'
});

// user object
var user = {
  name: {
    first: "Jeremy",
    last: "Cantu"
  },
  species: "human",
  age: 22
};

app.use(function* (next) {
  if (this.path !== '/') {
    return yield next;
  }

  this.body = yield render('user', {user : user});
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
