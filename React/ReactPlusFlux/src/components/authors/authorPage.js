"use strict";

var React = require('react');
var Router = require('react-router');
var Link = require('react-router').Link;
var AuthorApi = require('../../api/authorApi.js');
var AuthorList = require('./authorList.js');

var AuthorPage = React.createClass({
	getInitialState: function() {
		return {
			authors: []
		};
	},

	componentDidMount: function() {
		//  returns true if the component is rendered into the DOM, false 
		// otherwise. You can use this method to guard asynchronous calls 
		// to setState() or forceUpdate()
		if (this.isMounted()) { 
			this.setState({ authors: AuthorApi.getAllAuthors() });
		}
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