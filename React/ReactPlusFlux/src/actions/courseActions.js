"use strict";

var Dispatcher = require('../dispatcher/appDispatcher');
var CourseApi = require('../api/courseApi');
var ActionTypes = require('../constants/actionTypes');

var CourseActions = {
	createCourse: function(course) {
		var newCourse = CourseApi.saveCourse(course);

		// dispatcher tells stores that an course was created
		Dispatcher.dispatch({
			actionType: ActionTypes.CREATE_COURSE,
			course: newCourse
		});
	},

	updateCourse: function(course) {
		var updatedCourse = CourseApi.saveCourse(course);

		// dispatcher tells stores that an course was updated
		Dispatcher.dispatch({
			actionType: ActionTypes.UPDATE_COURSE,
			course: updatedCourse
		});
	},

	deleteCourse: function(id) {
		CourseApi.deleteCourse(id);

		// dispatcher tells stores that an course was deleted
		Dispatcher.dispatch({
			actionType: ActionTypes.DELETE_COURSE,
			id: id
		});
	}
};

module.exports = CourseActions;