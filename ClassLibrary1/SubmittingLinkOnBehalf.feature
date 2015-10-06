Feature: Editor submits a Link on behalf of a Submitter

Scenario Template: Submitter submits a Link
Given an URL '<URL>'
  And a description '<description>'
  And Submitter is '<submitterId>' 
 When Submitter submits a Link
 Then Link must have been submitted 
  And submitted Link contains data
	| SubmitterId   | Url   | Description   |
	| <submitterId> | <URL> | <description> |
Examples: 
    | URL                  | description                  | submitterId |
    | http://pgs-soft.com  | this is a company I work for | wmalara     |
    | http://pgs-soft.com  |                              | wmalara     |
    | https://pgs-soft.com |                              | wmalara     |

@DomainErrorOccurs
Scenario Template: Submitter submits a Link with invalid URL
Given an URL '<URL>'
  And Submitter is 'wmalara' 
 When Submitter submits a Link
Examples: 
    | URL                        |
    | localhost                  |
    | pgs-soft.com               |
    | http://localhost           |
    | http://tpluskiewicz        |
    | ssh://www.pgs-soft.com/git |
    | ftp://wcss.wroc.pl         |
    | urn:isbn:123456789         |
    | gg:123456789               |
    | arbitrary text             |
    | https://pgs soft.com       |
    | http://pgs_soft.com        |