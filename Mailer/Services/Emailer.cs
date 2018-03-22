using System.Collections.Generic;
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
            foreach (var recipient in recipientList)
            {
                var mail = new MailMessage("myodde@gmail.com", recipient)
                {
                    Subject = "this is a test email.",
                    Body = "this is my test email body"
                };
                Client.Send(mail);
            }
        }
    }
}