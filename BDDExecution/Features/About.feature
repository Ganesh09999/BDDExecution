Feature: About

This feature is all about getting sample for page object mobel

@POMSample
Scenario: Know more about kaara from homepage
	Given Open the browser
	And Navigate to URL
	When Know more button is visible
	And Know more button is clicked
	Then About us page is displayed