Feature: MNowik

@Dropdown
Scenario: Test dropdown selected option
	Given I log on and default page is opened
	And I navigate to "dropdown" link
	And I have dropdown page
	When I get selected option
	Then Option with text "Please select an option" is selected


@Dropdown
Scenario: Test dropdown select by text
	Given I log on and default page is opened
	And  I navigate to "dropdown" link
	And I have dropdown page
	When I select option with text "Option 1"
	And I get selected option
	Then Option with text "Option 1" is selected


@Dropdown
Scenario: Test dropdown select by index
	Given I log on and default page is opened
	And  I navigate to "dropdown" link
	And I have dropdown page
	When I select option with index '1'
	And I get selected option
	Then Option with text "Option 1" is selected
