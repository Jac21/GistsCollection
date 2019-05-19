"use strict";

var React = require('react');
var Link = require('react-router').Link;

// Defualt 404 page
var NotFoundPage = React.createClass({
	render: function() {
		return (
			<div>
				<h1>Page Not Found =(</h1>
					<p>Whoops! Sorry bout that, nothing to see here!</p>
					<Link to="app" className="btn btn-primary btn-lg">Back to Home</Link>
			</div>
		);
	}
});

module.exports = NotFoundPage;