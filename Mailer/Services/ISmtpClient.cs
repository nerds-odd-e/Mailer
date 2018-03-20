using System.Net.Mail;

namespace Mailer.Services
{
    public interface ISmtpClient
    {
        void Send(MailMessage email);
        void Initialize(string host, int port, string email, string password);
    }
}