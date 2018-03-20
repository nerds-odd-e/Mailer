Feature: PersonalisedEmails
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario Outline: Personalise Email Header
	Given <FirstName> <LastName>'s email address <EmailAddress> is registered in system
	When I press send email
	Then Recepient's email starts with "Hi <FirstName> <LastName>"
Examples: 
| FirstName | LastName | EmailAddress |
| Xin       | Lan      | lx@gmail.com |

