Feature: SendMailToAll

@with_local_smtp_server
Scenario Outline: Send All Email
	Given Upcoming course number is <Upcoming course>
	And I register a contact with email <contacts>
	When I press send email
	Then Email sent number should be <Email Sent>

Examples: no upcoming course
| Upcoming course | contacts          | Email Sent |
| 0               | test@gmail.com    | 0          |

Examples: have upcoming course
| Upcoming course | contacts          | Email Sent |
| 1               | test@gmail.com    | 1          |
| 1               | st@gmail;ap@gmail | 2          |
| 1               |                   | 0          |

