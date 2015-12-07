Feature: DBARAN

Scenario Outline: Key Presses Test
	Given I log on and default page is opened
	And I navigate to "/key_presses" link
	When I press <key>
	Then Valid <message> is displayed

	Examples: 
	| key | message |
	|"ESC"|"ESCAPE"|
	|"F2"| "F2"|
	|"1"|"1"|
	|"TAB"|"TAB"|
	|"Cabs Lock"|"CABS_LOCK"|

