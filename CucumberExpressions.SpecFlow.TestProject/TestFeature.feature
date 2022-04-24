﻿Feature: Test Feature

Scenario: Test scenario
	Given I registered as Marvin
	And I logged in as Marvin
	When I add 1 copy of the book "Discovery: Explore behaviour using examples" into my basket
	And I add 5 copies of the book "Formulation: Express examples using Given/When/Then" into my basket
	Then my basket should contain 6 books
	And they cost $10 in total
	And the weight of them is 12.5 kg
	Then the user should be user Marvin
	Then the coordinates should be 12, 45
