Feature: LabelMaker
	In order customize my labels
	As a Online POS user
	I want to be create label templates

Scenario: Add Field Receipt Elements
	Given I am at the setup label page
	When I click on "Invoice No" button
	Then the receipt template should contain "Invoice No"

Scenario: Add Free Text Receipt Elements
	Given I am at the setup label page
	And I have filled out the Free Text Field with "Some Free Text"
	When I click on "Free Text" button
	Then the receipt template should contain "Some Free Text"

Scenario: Add Repeater Receipt Elements
	Given I am at the setup label page
	When I click on "Add Repeater" button
	Then the receipt template should contain "Some Free Text"

Scenario: Receipt Elements are added below exiting items
	Given I am at the setup label page
	When I click on "Invoice No" button
	And I click on "User" button
	Then the "User" button fields are below the "Invoice No" fields
