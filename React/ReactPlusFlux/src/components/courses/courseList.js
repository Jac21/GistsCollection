"use strict";

var React = require('react');
var Router = require('react-router');
var Link = Router.Link;
var CourseActions = require('../../actions/courseActions');
var toastr = require('toastr');

var CourseList = React.createClass({

	propTypes: {
		courses: React.PropTypes.array.isRequired
	},

	deleteCourse: function(id, event) {
		event.preventDefault();
		CourseActions.deleteCourse(id);
		toastr.success('Course Deleted');
	},

	render: function() {
		var createCourseRow = function(course) {
			return (
				<tr key = {course.id}>
					<td><a href = '#'>Watch</a></td>
					<td><a href = '#' onClick={this.deleteCourse.bind(this, course.id)}>Delete</a></td>
					<td>{course.title}</td>
					<td>{course.author.name}</td>
					<td>{course.category}</td>
					<td>{course.length}</td>
				</tr>
			);
		};

		return (
			<div>
				<table className = "table">
					<thead>
						<th></th>
						<th></th>
						<td><b>Title</b></td>
						<td><b>Author</b></td>
						<td><b>Category</b></td>
						<td><b>Length</b></td>
					</thead>
					<tbody>
						{this.props.courses.map(createCourseRow, this)}
					</tbody>
				</table>
			</div>
		);
	}
});

module.exports = CourseList;