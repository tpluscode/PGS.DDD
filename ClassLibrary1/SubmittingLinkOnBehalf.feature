Feature: Editor submits a Link on behalf of a Submitter

Scenario Template: Submitter sumbits a Link
Given an URL '<URL>'
  And a description '<description>'
  And Submitter is '<submitterId>' 
 When Submitter submits a Link
 Then Link must have been submitted 
  And submitted Link contains data
	| SubmitterId   | Url   | Description   |
	| <submitterId> | <URL> | <description> |
Examples: 
    | URL                 | description                  | submitterId |
    | http://pgs-soft.com | this is a company I work for | wmalara     |
    | http://pgs-soft.com |                              | wmalara     |
