"use strict";

var React = require('react');
var Router = require('react-router');
var routes = require('./routes');

Router.run(routes, /*Router.HistoryLocation,*/ function(Handler) {
	React.render(<Handler/>, document.getElementById('app'));
});

/*
// old method without react-router implementation
(function(win) {
	"use strict";

	// var App...

	function render() {
		var route = win.location.hash.substr(1);
		React.render(<App route={route} />, document.getElementById('app'));
	}

	win.addEventListener('hashchange', render);
	render();

	// React.render(<Home />, document.getElementById('app'));

})(window);
*/