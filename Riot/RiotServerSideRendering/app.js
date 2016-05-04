var express = require('express'),
		swig = require('swig'),
		riot = require('riot'),
		hello = require('./hello-world.tag'),
		app = express();

app.engine('html', swig.renderFile);

app.set('view engine', 'html');
app.set('views',__dirname + '/views');

app.use(express.static(__dirname + '/public'));

app.get('/', function(req, res) {
	var startingName = "John";
	var tagRender = riot.render(hello, {firstName: startingName});
	res.render('index', {tagContent:tagRender, firstName:startingName});
});

var portNumber = 3001;
app.listen(portNumber, function() {
	console.log("Server listening on port number " + portNumber);
});