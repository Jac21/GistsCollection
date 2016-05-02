// instantiate casper
var casper = require('casper').create();

casper.start("https://www.google.com/", function() {
	this.echo(this.getTitle());
});

casper.then(function() {
	this.echo(this.getCurrentUrl());
});

casper.run(function() {
	// echo to console, don't forget to exit()!
	this.echo("Suite has ended").exit();
});