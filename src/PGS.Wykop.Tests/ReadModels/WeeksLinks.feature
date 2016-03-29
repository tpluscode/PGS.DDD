Feature: Weeks Links read model
	Maintain a list of links partitioned by week

@WeeksLinks
Scenario: Add submitted link to correct week
	Given Submitter 'wmalara' has full name 'Wojciech Malara'
	When Link has been submitted
		| Url           | Description | SubmitterId | DateSubmitted |
		| http://wp.pl/ | portal wp   | wmalara     | 2015-01-02    |
	Then Following link should have been added
		| Url           | Description | SubmitterFullName | Week |
		| http://wp.pl/ | portal wp   | Wojciech Malara   | 1    |
