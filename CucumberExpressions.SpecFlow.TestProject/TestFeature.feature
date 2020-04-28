Feature: Test Feature

Scenario: Test scenario
	Given I logged in as Marvin
	When I add 1 copy of the book "Discovery: Explore behaviour using examples" into my basket
	And I add 5 copies of the book "Formulation: Express examples using Given/When/Then" into my basket
	Then my basket should contain 6 books
