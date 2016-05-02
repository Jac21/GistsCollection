var casper = require('casper').create({
  verbose: true,
  logLevel: 'debug',
  clientScripts: ["vendor/jquery.min.js", "vendor/lodash.js"]
});

var links = [];

// function getLinks() {
//   // var links = document.querySelectorAll('.b_algo a');
//   var links = $('.b_algo a')
//   return Array.prototype.map.call(links, function(e) {
//     return e.getAttribute('href');
//   });
// }

function getLinks() {
  // var links = document.querySelectorAll('.b_algo a');
  var links = $('.b_algo a')
  return _.map(links, function(e) {
    return e.getAttribute('href');
  });
}

casper.start('http://bing.com/', function() {
  // search for 'casperjs' from google form
  this.fill('form[action="/search"]', {
    q: 'casperjs'
  }, true);
});

casper.then(function() {
  // aggregate results for the 'casperjs' search
  links = this.evaluate(getLinks);
  // now search for 'phantomjs' by filling the form again
  this.fill('form[action="/search"]', {
    q: 'phantomjs'
  }, true);
});

casper.then(function() {
  // aggregate results for the 'phantomjs' search
  links = links.concat(this.evaluate(getLinks));
});

casper.run(function() {
// echo results in some readable fashion
  this.echo(links.length + ' links found:');
  this.echo(' - ' + links.join('\n - ')).exit();
});