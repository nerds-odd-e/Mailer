using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Mailer.Models;

namespace Mailer.Services
{
    public class Emailer
    {
        public Emailer(ISmtpClient client)
        {
            Client = client;
        }

        public ISmtpClient Client { get; set; }

        public void SendEmail(List<Contact> recipientList)
        {
            var emails = ConstructPersonalizedEmail(recipientList);

            emails.ForEach(x => Client.Send(x));
        }

        public List<MailMessage> ConstructEmails(List<string> recipientList)
        {
            return recipientList.Select(x => new MailMessage("myodde@gmail.com", x)
            {
                Subject = "this is a test email.",
                Body = "this is my test email body"
            }).ToList();
        }

        public List<MailMessage> ConstructPersonalizedEmail(List<Contact> contacts)
        {
            return contacts.Select(x => new MailMessage("myodde@gmail.com", x.Email)
            {
                Subject = "this is a test email.",
                Body = string.Format("Hi {0} {1}\n\n{2}", x.FirstName, x.LastName, "this is a test body.")
            }).ToList();
        }
    }
}