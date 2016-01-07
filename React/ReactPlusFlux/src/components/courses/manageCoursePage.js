"use strict";

var React = require('react');
var Router = require('react-router');
var CourseForm = require('./courseForm.js');
var toastr = require('toastr');

var ManageCoursePage = React.createClass({
	// array reference to react router navigation mixin
	mixins: [
		Router.Navigation
	],

	render: function() {
		return (
			<CourseForm />
		);
	}
});

module.exports = ManageCoursePage;