"use strict";

var React = require('react');
var Router = require('react-router');
var CourseForm = require('./courseForm.js');
var CourseActions = require('../../actions/courseActions.js');
var CourseStore = require('../../stores/courseStore.js');
var toastr = require('toastr');

var ManageCoursePage = React.createClass({
	// array reference to react router navigation mixin
	mixins: [
		Router.Navigation
	],

	statics: {
		willTransitionFrom: function(transition, component) {
			if(component.state.dirty && !confirm('Leave without saving?')) {
				transition.abort();
			} 
		}
	},

	getInitialState: function() {
		return {
			course: {id: '', title: '', author: '', category: '', length: ''},
			errors: {},
			dirty: false
		};
	},

	componentWillMount: function() {
		// from the past '/course:id'
		var courseId = this.props.params.id; 

		if (courseId) {
			this.setState({course: CourseStore.getCoursesById(courseId)});
		}
	},

	setCourseState: function(event) {
		this.setState({dirty: true});
		var field = event.target.name;
		var value = event.target.value;
		this.state.course[field] = value;
		return this.setState({course: this.state.course});
	},

	courseFormIsValid: function() {
		var formIsValid = true;

		// clear any previous errors
		this.state.errors = {};

		if(this.state.course.title.length < 3) {
			this.state.errors.firstName = "Course title must be at least 3 characters long";
			formIsValid = false;
		}

		this.setState({errors: this.state.errors});
		return formIsValid;
	},

	saveCourse: function(event) {
		// prevent default browser form save
		event.preventDefault();

		// validation
		if(!this.courseFormIsValid()) {
			return;
		}

		if(this.state.course.id) {
			CourseActions.updateCourse(this.state.course);
		} else {
			CourseActions.updateCourse(this.state.course);
		}

		this.setState({dirty: false});

		// show toastr success message on transition
		toastr.success('Course \"' + this.state.course.title + '\" saved');
		this.transitionTo('courses');
	},

	render: function() {
		return (
			<CourseForm 
				course={this.state.course} 
				onChange = {this.setCourseState}
				onSave = {this.saveCourse} 
				errors = {this.state.errors} />
		);
	}
});

module.exports = ManageCoursePage;