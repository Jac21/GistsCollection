"use strict";

var React = require('react');
var Router = require('react-router');
var AuthorForm = require('./authorForm.js');
var AuthorApi = require('../../api/authorApi');
var toastr = require('toastr');

var ManageAuthorPage = React.createClass({
	// array reference to react router navigation mixin
	mixins: [
		Router.Navigation
	],

	getInitialState: function() {
		return {
			author: {id: '', firstName: '', lastName: ''}
		};
	},

	setAuthorState: function(event) {
		var field = event.target.name;
		var value = event.target.value;
		this.state.author[field] = value;
		return this.setState({author: this.state.author});
	},

	saveAuthor: function(event) {
		// prevent default browser form save
		event.preventDefault();

		// call api's save author function
		AuthorApi.saveAuthor(this.state.author);

		// show toastr success message on transition
		toastr.success('Author ' + this.state.author.firstName + ' ' + this.state.author.lastName + ' saved');

		this.transitionTo('authors');
	},

	render: function() {
		return (
			<AuthorForm 
				author={this.state.author} 
				onChange = {this.setAuthorState}
				onSave = {this.saveAuthor} />
		);
	}
});

module.exports = ManageAuthorPage;