Feature: Link
	
@DomainEntities
Scenario: Creating a Link
	When Link for 'http://example.org/link' is created by 'wmalara' described 'example link'
	Then Url should be 'http://example.org/link'
	 And Submitter should be 'wmalara'
	 And Description should be 'example link'
	 And Version number should be 1
	 
@DomainEntities
Scenario: Creating a Link without description
	When Link for 'http://example.org/link' is created by 'wmalara' without description
	Then Url should be 'http://example.org/link'
	 And Submitter should be 'wmalara'
	 And Description should be ''
	 And Version number should be 1
