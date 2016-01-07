"use strict";

var React = require('react');
var Router = require('react-router');
var Link = require('react-router').Link;
var CourseActions = require('../../actions/courseActions.js');
var CourseStore = require('../../stores/courseStore.js');
var CourseList = require('./courseList.js');

var CoursePage = React.createClass({
	getInitialState: function() {
		return {
			courses: CourseStore.getAllCourses()
		};
	},

	componentWillMount: function() {
		CourseStore.addChangeListener(this._onChange);
	},

	// clean up when component is unmounted
	componentWillUnmount: function() {
		CourseStore.removeChangeListener(this._onChange);
	},

	_onChange: function() {
		this.setState({authors: CourseStore.getAllCourses()});
	},

	render: function() {

		return (
			<div>
				<h1>Courses</h1>
				<Link to="addCourse" className = "btn btn-default">Add Course</Link>
				<CourseList courses = {this.state.courses} />
			</div>
		);
	}
});

module.exports = CoursePage;