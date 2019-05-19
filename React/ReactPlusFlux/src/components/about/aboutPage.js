"use strict";

var React = require('react');

var About = React.createClass({
	statics: {

		// defines a hook into React that triggers when the router is being transitioned 
		// to the particular page/component (useful for auth, etc.)
		willTransitionTo: function(transition, params, query, callback) {
			if (!confirm('Are you sure you want to read a page that\'s this boring?')) {
				transition.about();
			} else {
				callback();
			}
		},

		// defines a hook into React that triggers when the router is being transitioned 
		// from the particular page/component (useful for form-checking, etc.)
		willTransitionFrom: function(transition, component) {
			if (!confirm('Are you sure you want to leave a page that\'s this exciting?')) {
				transition.about();
			}
		}
	},

	render: function() {
		return (
			<div>
				<h1>About</h1>
				<p>
					This application was built using the following technologies:
					<ul>
						<li>React</li>
						<li>React Router</li>
						<li>Flux</li>
						<li>Node</li>
						<li>Gulp</li>
						<li>Browserify</li>
						<li>Bootstrap</li>
					</ul>
				</p>
			</div>
		);
	}
});

module.exports = About;