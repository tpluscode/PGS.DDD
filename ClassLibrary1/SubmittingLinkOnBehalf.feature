Feature: Editor submits a Link on behalf of a Submitter

Scenario: Editor sumbits a Link
Given an URL 'http://pgs-soft.com'
  And a description 'this is a company I work for'
 When Editor 'tpluskiewicz' adds a Link on behalf of Submitter 'wmalara'
 Then Link must have been submitted 
	#| SubmitterId | URL                 | Description                  |
	#| wmalara     | http://pgs-soft.com | this is a company I work for |