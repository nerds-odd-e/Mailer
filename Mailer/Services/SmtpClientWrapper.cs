using System.Net;
using System.Net.Mail;

namespace Mailer.Services
{
    public class SmtpClientWrapper : ISmtpClient
    {
        public SmtpClient SmtpClient { get; set; }
        public SmtpClientWrapper()
        {
            SmtpClient = new SmtpClient();
        }
        public void Send(MailMessage mailMessage)
        {
            SmtpClient.Send(mailMessage);
        }

        public void Initialize(string host, int port, string email, string password)
        {
            SmtpClient.EnableSsl = true;
            SmtpClient.Port = port;
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.Credentials = new NetworkCredential(email, password);
            SmtpClient.Host = host;
        }
    }
}