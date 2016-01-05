"use strict";

var React = require('react');
var Router = require('react-router');
var AuthorForm = require('./authorForm.js');
var AuthorActions = require('../../actions/authorActions.js');
var AuthorStore = require('../../stores/authorStore.js');
var toastr = require('toastr');

var ManageAuthorPage = React.createClass({
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
			author: {id: '', firstName: '', lastName: ''},
			errors: {},
			dirty: false
		};
	},

	componentWillMount: function() {
		// from the past '/author:id'
		var authorId = this.props.params.id; 

		if (authorId) {
			this.setState({author: AuthorStore.getAuthorById(authorId)});
		}
	},

	setAuthorState: function(event) {
		this.setState({dirty: true});
		var field = event.target.name;
		var value = event.target.value;
		this.state.author[field] = value;
		return this.setState({author: this.state.author});
	},

	authorFormIsValid: function() {
		var formIsValid = true;

		// clear any previous errors
		this.state.errors = {};

		if(this.state.author.firstName.length < 3) {
			this.state.errors.firstName = "First name must be at least 3 characters long";
			formIsValid = false;
		}

		if(this.state.author.lastName.length < 3) {
			this.state.errors.lastName = "Last name must be at least 3 characters long";
			formIsValid = false;
		}

		this.setState({errors: this.state.errors});
		return formIsValid;
	},

	saveAuthor: function(event) {
		// prevent default browser form save
		event.preventDefault();

		// validation
		if(!this.authorFormIsValid()) {
			return;
		}

		if(this.state.author.id) {
			AuthorActions.updateAuthor(this.state.author);
		} else {
			AuthorActions.createAuthor(this.state.author);
		}

		this.setState({dirty: false});

		// show toastr success message on transition
		toastr.success('Author ' + this.state.author.firstName + ' ' + this.state.author.lastName + ' saved');
		this.transitionTo('authors');
	},

	render: function() {
		return (
			<AuthorForm 
				author={this.state.author} 
				onChange = {this.setAuthorState}
				onSave = {this.saveAuthor} 
				errors = {this.state.errors} />
		);
	}
});

module.exports = ManageAuthorPage;