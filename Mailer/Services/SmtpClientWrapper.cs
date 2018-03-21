using System;
using System.Net;
using System.Net.Mail;

namespace Mailer.Services
{
    public class SmtpClientWrapper : ISmtpClient
    {
        private readonly SmtpClient _smtpClient;

        public SmtpClientWrapper()
        {
            _smtpClient = new SmtpClient();
            Initialize();
        }

        public void Send(MailMessage mailMessage)
        {
            _smtpClient.Send(mailMessage);
        }

        private void Initialize()
        {
            _smtpClient.Host = Environment.GetEnvironmentVariable("smtpHost") ?? "localhost";
            _smtpClient.Port = int.Parse(Environment.GetEnvironmentVariable("smtpPort") ?? "25");
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.UseDefaultCredentials = false;
            var email = Environment.GetEnvironmentVariable("senderEmail");
            var password = Environment.GetEnvironmentVariable("senderPassword");
            _smtpClient.Credentials = new NetworkCredential(email, password);
        }

        public void EnableSsl()
        {
            _smtpClient.EnableSsl = true;
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}