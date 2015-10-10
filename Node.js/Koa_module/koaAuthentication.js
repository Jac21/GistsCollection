"use strict";

// include the koa module, set it
// to an 'app' variable
var koa = require('koa');
var app = koa();

// include co-body to parse
// request body
var parse = require('co-body');

// include session module
var session = require('koa-session');

var form = '<form action = "/login" method="POST">\
  <input name = "username" type = "text" value = "username">\
  <input name = "password" type = "password" placeholder = "The password is \'password\'">\
  <button type = "submit">Submit</button>\
</form>';

// set keys for signed cookies, session
app.keys = ['secret1', 'secret2', 'secret3'];
app.use(session(app));

// if 'this.session.authenticated' exists, see "hello world"
app.use(function* home(next) {
  if (this.request.path !== '/') {
    return yield next;
  } else if (this.session.authenticated) {
    return this.body = 'hello world';
  }

  this.status = 401;
});

// if successful, logged in user should get redirected
// to '/'

app.use(function* login(next){
  if (this.request.path !== '/login') return yield next;
  if (this.request.method === "GET") return this.body = form;
  if (this.request.method !== "POST") return;

  var body = yield parse(this);
  if (body.username !== 'username' || body.password !== 'password') {
    return this.status = 400;
  }

  this.session.authenticated = true;
  this.redirect('/');
});

//logout

app.use(function* logout(next) {
  if (this.request.path !== '/logout') return yield next;
  this.session.authenticated = false;
  this.redirect('/login');
})

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
