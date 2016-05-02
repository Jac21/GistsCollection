var casper = require("casper").create({
  verbose: true,
  logLevel: 'debug',     // debug, info, warning, error
  pageSettings: {
    loadImages: false,
    loadPlugins: false,
    userAgent: 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4'
  },
  clientScripts: ["/vendor/jquery.min.js"]
});

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