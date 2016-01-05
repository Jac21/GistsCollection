"use strict";

var React = require('react');
var Router = require('react-router');
var Link = require('react-router').Link;
var AuthorActions = require('../../actions/authorActions.js');
var AuthorStore = require('../../stores/authorStore.js');
var AuthorList = require('./authorList.js');

var AuthorPage = React.createClass({
	getInitialState: function() {
		return {
			authors: AuthorStore.getAllAuthors()
		};
	},

	componentWillMount: function() {
		AuthorStore.addChangeListener(this._onChange);
	},

	// clean up when component is unmounted
	componentWillUnmount: function() {
		AuthorStore.removeChangeListener(this._onChange);
	},

	_onChange: function() {
		this.setState({authors: AuthorStore.getAllAuthors()});
	},

	render: function() {

		return (
			<div>
				<h1>Authors</h1>
				<Link to="addAuthor" className = "btn btn-default">Add Author</Link>
				<AuthorList authors = {this.state.authors} />
			</div>
		);
	}
});

module.exports = AuthorPage;