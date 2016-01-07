"use strict";

var React = require('react');
var Input = require('../common/textInput.js');

var CourseForm = React.createClass({

	/*
	propTypes: {
		course: React.PropTypes.object.isRequired,
		onSave: React.PropTypes.func.isRequired,
		onChange: React.PropTypes.func.isRequired,
		errors: React.PropTypes.object
	},*/

	render: function() {
		return (
			<form>
				<h1>Edit Course</h1>

			</form>
		);
	}
});

module.exports = CourseForm;

				/*<Input
					name = "courseTitle"
					label = "Title"
					value = {this.props.course.courseTitle}
					onChange = {this.props.onChange}
					error = {this.props.errors.courseTitle}
					placeholder = "Enter here..." />

				<Input
					name = "courseAuthor"
					label = "Author"
					value = {this.props.course.courseAuthor}
					onChange = {this.props.onChange}
					error = {this.props.errors.courseAuthor}
					placeholder = "Enter here..." />

				<Input
					name = "category"
					label = "Category"
					value = {this.props.course.category}
					onChange = {this.props.onChange}
					error = {this.props.errors.category}
					placeholder = "Enter here..." />

				<Input
					name = "courseLength"
					label = "Length"
					value = {this.props.course.courseLength}
					onChange = {this.props.onChange}
					error = {this.props.errors.courseLength}
					placeholder = "Enter here..." />

				<input type="submit" value = "Save" className = "btn btn-default" 
				onClick = {this.props.onSave} />*/