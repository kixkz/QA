Feature: Users


#Scenario: Get all users
#	Given I want to prepare a request
#	When I get all users from the users endpoint
#	Then Response status code should be OK
#		And the response should contains a list of users

#@Authenticate @Ivan
@Authenticate
Scenario: Create a new user
	Given I have the following user data
	| name   | Email             | Gender | Status |
	| Tofshko | tesffrgtttt@test.com | male   | active |
	When I send a request to the users endpoint
	Then Response status code should be Created
		And The user should be created successfully

@Authenticate2
Scenario: Create a new user2
	Given I have the following user data
	| name     | Email               | Gender | Status |
	| Tofsdghko | tefsrfddtgttt@test.com | male   | active |
	When I send a request to the users endpoint
	Then Response status code should be Created
		And The user should be created successfully

#Scenario: Update an exiting user
#	Given I have a created user already
#	When I send update request to the users endpoint
#	Then Response status code should be OK
#		And The user should be updated successfully


#Scenario: Delete an exiting user
#	Given I have a created user already
#	When I send a request to the users endpoint
#	Then Response status code should be No Content

