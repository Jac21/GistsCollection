/* 

Simple little script to output the necessay node module information required
for a high-level package.json file

*/

var fs = require("fs");

(function () {

	// filename passed in through command-line arg,
	// print usage if needed
	var filename = process.argv[2] + '.json';
	if (process.argv.length < 3) {
		console.log("USAGE: 'node packageJsonCreator.js <output-filename>'");
		process.exit();
	}

	// read existing node_modules directory content and subdirectories
	fs.readdir("./node_modules", function (err, dirs) {
		if (err) {
			return console.log(err);
		}

		// iterate through directories/modules
		dirs.forEach(function (dir) {

			// ignore .bin folder
			if (dir.indexOf(".") !== 0) {

				// find module's specific package.json; if it exists,
				// append and log its name and version number to a json file
				var packageJsonFile = "./node_modules/" + dir + "/package.json";

				if (fs.existsSync(packageJsonFile)) {

					fs.readFile(packageJsonFile, function (err, data) {
						if (err) {
							console.log(err);
						} else {
							var json = JSON.parse(data);

							// proper formatting
							var output = (JSON.stringify(json.name, null, 4) + ': ' + JSON.stringify(json.version, null, 4) + ',\n');

							// append data to file to avoid overwriting
							fs.appendFile(filename, output, function (err) {
								if (err) {
									return console.log(err)
								}
							});

							console.log(output);
						}
					});
				}
			}
		});
	});
})();