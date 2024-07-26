Feature: Specflow demo for Kaara

This feature covers most basic test automation with specflow

@demoKaara
Scenario: Get the home Page
	Given Open the browser
	When Navigate to URL
	Then Verify home page is displayed

@demo
Scenario Outline: Get Page details
	Given Open the browser
	When Navigate to URL
	Then Verify '<Page>' is displayed
	Examples: 
	| Page     |
	| About Us |
	| Products |

@demoKaara
Scenario: Get Contact details
	Given Open the browser
	When Navigate to URL
	Then Verify Pages are displayed
	| Page     |
	| Products |
	| Contact  |