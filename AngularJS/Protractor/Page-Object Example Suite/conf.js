// conf.js
exports.config = {
	framework: 'jasmine2',

	// The address of a running selenium server.
	seleniumAddress: 'http://localhost:4444/wd/hub',

	specs: ['spec.js'],

	// Capabilities to be passed to the webdriver instance.
	multiCapabilities: [{
		browserName: 'firefox'
	}, {
		browserName: 'chrome'
	}],

	// Spec patterns are relative to the location of the spec file. They may
  	// include glob patterns.
  	// e.g., protractor conf.js --suite homepage
  	suites: {
    	homepage: 'spec.js'
  	}
}