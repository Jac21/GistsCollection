/*eslint-disable strict */ // disable check on strict check due to global var

var React = require('react');
var Header = require('./common/header.js');
var RouteHandler = require('react-router').RouteHandler;
$ = jQuery = require('jquery');

var App = React.createClass({
	render: function() {

		/*
		// old method without react-router implementation
		var Child;

		switch(this.props.route) {
			case 'about': Child = About; break;
			case 'authors': Child = Authors; break;
			default: Child = Home;
		}
		*/

		return (
			<div>
				<Header/>
				<div className = "container-fluid">
					<RouteHandler/>
				</div>
			</div>
		);
	}
});

module.exports = App;