Feature: Editor submits a Link on behalf of a Submitter

Scenario Template: Submitter submits a Link
 Given The date is '2015-10-10'
 When Submitter '<submitterId>' submits a Link '<URL>' described '<description>'
 Then Link should have been submitted 
	| SubmitterId   | Url   | Description   | DateSubmitted |
	| <submitterId> | <URL> | <description> | 2015-10-10    |
Examples: 
    | URL                  | description                  | submitterId |
    | http://pgs-soft.com  | this is a company I work for | wmalara     |
    | http://pgs-soft.com  |                              | wmalara     |
    | https://pgs-soft.com |                              | wmalara     |

Scenario Template: Submitter submits a non-HTTP Link
 When Submitter 'wmalara' submits a Link '<URL>'
 Then Link should not have been submitted
Examples: 
    | URL                        |
    | http://localhost           |
    | http://tpluskiewicz        |
    | ssh://www.pgs-soft.com/git |
    | ftp://wcss.wroc.pl         |
    | /a/relative/uri            |
