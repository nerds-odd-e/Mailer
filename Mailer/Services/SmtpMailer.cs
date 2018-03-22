using System;
using System.Net.Mail;

namespace Mailer.Services
{
    public class SmtpMailer
    {
        private SmtpClient _smtpClient;

        public SmtpMailer()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Host = Environment.GetEnvironmentVariable("Host");//"localhost";
            _smtpClient.Port = int.Parse(Environment.GetEnvironmentVariable("Port"));
        }

        public void SendOneEmail(string body, string recipient)
        {
            var mail = new MailMessage("from@gmail.com", recipient);
            mail.Subject = "test";
            mail.Body = body;
            _smtpClient.Send(mail);
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}