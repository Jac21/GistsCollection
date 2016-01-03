"use strict";

var React = require('react');

var Router = require('react-router');
var DefaultRoute = Router.DefaultRoute;
var Route = Router.Route;
var NotFoundRoute = Router.NotFoundRoute;
var Redirect = Router.Redirect;

// Routes needed by the entirety of the application, dictates the routes
// handled by React-Router and used for the navigation throughout app
// flow
var routes = (
	<Route name = "app" path = "/" handler = {require('./components/app')}>
		<DefaultRoute handler = {require('./components/homePage')} />
		<Route name = "authors" handler = {require('./components/authors/authorPage')} />
		<Route name = "addAuthor" path = "author" handler = {require('./components/authors/manageAuthorPage')} />
		<Route name = "about" handler = {require('./components/about/aboutPage')} />
		<NotFoundRoute handler = {require('./components/notFoundPage')} />
		<Redirect from="about-us" to="about" />
		<Redirect from="awthurs" to="authors" />
		<Redirect from="about/*" to="about" />
	</Route>
);

module.exports = routes;