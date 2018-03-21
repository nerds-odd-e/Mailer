Feature: SendMailToAll


@SendAllEMail
Scenario Outline: Send All Email (no up coming course)
	Given Upcoming course number is <Upcoming course>
	And I register a contact with email <contacts>
	When I press send email
	Then Email sent number should be <Email Sent>

Examples: 
| Upcoming course | contacts            | Email Sent |
| 0               | "test@gmail.com"    | 0          |
| 1               | "st@gmail;ap@gmail" | 2          |
| 1               | "test@gmail.com"    | 1          |
| 1               | ""                  | 0          |

