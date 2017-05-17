// Musician Schema, using mongoose

var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var MusicianSchema = new Schema({
	name: String,
	band: String,
	instrument: String
});

mongoose.model('Musician', MusicianSchema);