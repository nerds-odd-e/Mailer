Feature: PersonalisedEmails

@ignore
@with_local_smtp_server
Scenario Outline: Personalise Email Header
	Given <FirstName> <LastName>'s email address <EmailAddress> is registered in system
	When I press send email
	Then Recepient's email starts with "Hi <FirstName> <LastName>"
Examples: 
| FirstName | LastName | EmailAddress |
| Xman      | Logo     | lx@gmail.com |

