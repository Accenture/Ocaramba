Feature: DBARAN

Scenario Outline: Key Presses Test
	Given I log on and default page is opened
	And I navigate to "key_presses" link
	When I press <key>
	Then Valid <message> is displayed
	
	Examples: 
	| key          | message     |
	| "ESC"        | "ESCAPE"    |
	| "F2"         | "F2"        |
	| "1"          | "NUMPAD1"   |
	| "TAB"        | "TAB"       |
	| "SPACE"      | "SPACE"     |
	| "ARROW DOWN" | "DOWN"      |
	| "ARROW LEFT" | "LEFT"      |
	| "ALT"        | "ALT"       |
	| "SHIFT"      | "SHIFT"     |
	| "PAGE UP"    | "PAGE_UP"   |
	| "PAGE DOWN"  | "PAGE_DOWN" |
	| "DELETE"     | "DELETE"    |
	| "MULTIPLY"   | "MULTIPLY"  |
	| "SUBTRACT"   | "SUBTRACT"  |

Scenario Outline: Do not press any key
	Given I log on and default page is opened
	And I navigate to "key_presses" link
	When I press <key>
	Then Results element is not displayed
	
	Examples: 
	| key |
	| ""  |
