
#The MIT License (MIT)

#Copyright (c) 2015 Objectivity Bespoke Software Specialists

#Permission is hereby granted, free of charge, to any person obtaining a copy
#of this software and associated documentation files (the "Software"), to deal
#in the Software without restriction, including without limitation the rights
#to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
#copies of the Software, and to permit persons to whom the Software is
#furnished to do so, subject to the following conditions:

#The above copyright notice and this permission notice shall be included in all
#copies or substantial portions of the Software.

#THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
#IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
#FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
#AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
#LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
#OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
#SOFTWARE.

Feature: SmokeTests

@Dropdown
Scenario: Verify default option selected in dropdown
	Given Default page is opened
	When I click "dropdown" link
	And I see page Dropdown List
	And I check selected option
	Then Option with text "Please select an option" is selected

@Dropdown
Scenario: Verify if dropdown option can be selected by text
	Given Default page is opened
	When I click "dropdown" link
	And I see page Dropdown List
	When I select option with text "Option 1"
	And I check selected option
	Then Option with text "Option 1" is selected

@Dropdown
Scenario: Verify if dropdown option can be selected by index
	Given Default page is opened
	When I click "dropdown" link
	And I see page Dropdown List
	When I select option with index '1'
	And I check selected option
	Then Option with text "Option 1" is selected


Scenario Outline: Key Presses Test
	Given Default page is opened
	When I click "key_presses" link
	And I press <key>
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
	Given Default page is opened
	When I click "key_presses" link
	When I press <key>
	Then Results element is not displayed
	
Examples: 
	| key |
	| ""  |
