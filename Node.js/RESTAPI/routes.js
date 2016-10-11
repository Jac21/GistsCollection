// routes for express to utilize, uses controller for methods
module.exports = function(app) {
	var musicians = require('./controllers/musicians');

	app.get('/musicians', musicians.findAll);
	app.get('/musicians/:id', musicians.findById);
	app.get('/import', musicians.import);

	app.post('/musicians', musicians.add);

	app.put('/musicians/:id', musicians.update);

	app.delete('/musicians/:id', musicians.delete);
}