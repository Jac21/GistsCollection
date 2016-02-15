module.exports = {
	entry: "./index.js",
	output: {
		path: __dirname,
		filename: "bundle.js",
		publicPath: "/static/"
	},
	module: {
		    loaders: [
		    	{ test: /\.js$/, exclude: /node_modules/, loader: "babel-loader"}
		    ]
	}
};