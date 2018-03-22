using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Mailer.Services
{
    public class Emailer
    {
        public Emailer(ISmtpClient client)
        {
            Client = client;
        }

        public ISmtpClient Client { get; set; }

        public void SendEmail(List<string> recipientList)
        {
            var emails = ConstructEmails(recipientList);

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
    }
}