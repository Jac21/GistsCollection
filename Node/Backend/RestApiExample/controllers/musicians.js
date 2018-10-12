// import mongoose and the Musician model
var mongoose = require('mongoose'),
Musician = mongoose.model('Musician');

// return all Musician data
// localhost:3001/musicians
exports.findAll = function(req, res) {
	Musician.find({}, function(err, results) {
		return res.send(results);
	});
};

// return particular Musician
// localhost:3001/musicians/{id}
exports.findById = function(req, res) {
	var id = req.params.id;
	Musician.findOne({'_id':id}, function(err, result) {
		return res.send(result);
	});
};

// import Musician data into MongoDB
// localhost:3001/import
exports.import = function(req, res) {
	Musician.create(
		{ "name": "Ben", "band": "DJ Code Red", "instrument": "Reason" },
    { "name": "Mike D.","band": "Kingston Kats", "instrument": "drums" },
    { "name": "Eric", "band": "Eric", "instrument": "piano" },
    { "name": "Paul", "band": "The Eyeliner", "instrument": "guitar" }
  , function(err) {
  	if(err) {
  		return console.log(err);
  	}

  	return res.send(202);
  });
};

// POST a new Musician
// localhost:3001/musicians
exports.add = function(req, res) {
	Musician.create(req.body, function(err, musician) {
		if(err) {
			return console.log(err);
		}

		return res.send(musician);
	});
};

// PUT updates into an existing musicians
// localhost:3001/musicians/{id}
exports.update = function(req, res) {
	var id = req.params.id;
	var updates = req.body;

	Musician.update({"_id":id}, req.body,
		function(err, numberAffected) {
			if (err) {
				return console.log(err);
			}

			console.log("Updated %d musicians", numberAffected);

			return res.send(202);
	});
};

// DELETE an existing musician
// localhost:3001/musicians/{id}
exports.delete = function(req, res){
  var id = req.params.id;
  Musician.remove({'_id':id},function(result) {
    return res.send(result);
  });
};