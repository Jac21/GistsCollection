// npm install -g protractor
// spec.js

// Page-object model test suite of the Protractor Demo site
var ProtractorDemoPage = function() {

	// Elements
	var firstNumber = element(by.model('first'));
	var secondNumber = element(by.model('second'));
	var goButton = element(by.id('gobutton'));
	var latestResult = element(by.binding('latest'));
	var history = element.all(by.repeater('result in memory'));

	this.get = function() {
		browser.get('http://juliemr.github.io/protractor-demo/');
	};

	// Addition function for calculator application functionality
	this.add = function(a, b) {
		firstNumber.sendKeys(a);
		secondNumber.sendKeys(b);
		goButton.click();
	};

	/////////////////////////////////////////////////////////////
	// Exposure methods for page elements
	this.getLatestResultText = function() {
		return latestResult.getText();
	};

	this.getHistoryCount = function() {
		return history.count();
	};

	this.getHistoryFirst = function() {
		return history.first();
	};

	this.getHistoryLast = function() {
		return history.last();
	};
};

describe('Protractor Demo App Tests', function() {

	// page object init
	var protractorDemoPage = new ProtractorDemoPage();

	// Start off new browser instance for each test
	beforeEach(function() {
		protractorDemoPage.get();
	});

	/* Tests */

	it('should have a title', function() {
		expect(browser.getTitle()).toEqual('Super Calculator');
	});

	it('should add one and two', function() {
		protractorDemoPage.add(1, 2);

		expect(protractorDemoPage.getLatestResultText()).
			toEqual('3');
	});

	it('should have a history', function() {
		protractorDemoPage.add(1, 2);
		protractorDemoPage.add(3, 4);

		expect(protractorDemoPage.getHistoryCount()).toEqual(2);

		protractorDemoPage.add(5, 6);

		expect(protractorDemoPage.getHistoryCount()).toEqual(3);
		expect(protractorDemoPage.getHistoryLast().getText()).toContain('1 + 2');
		expect(protractorDemoPage.getHistoryFirst().getText()).toContain('5 + 6');
	})
});