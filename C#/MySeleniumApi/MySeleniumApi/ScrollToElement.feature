Feature: ScrollToElement
	In order avoid exceptions in regards to elements outside the viewport of the browser window
	As an automated test run
	I want to scroll to that element when necessary

Scenario: Element scrolled to successfully
	Given I am on the stated application screen
		And I call my scrolling function on the element name built-by used by the test
	Then I should be able to utilize the element without an exception