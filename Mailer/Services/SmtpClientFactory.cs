namespace Mailer.Services
{
    public interface ISmtpClientFactory
    {
        SmtpClientWrapper createSmtpClient();
    }

    public class SmtpClientFactory : ISmtpClientFactory
    {
        public SmtpClientWrapper createSmtpClient()
        {
            return new SmtpClientWrapper();
        }
    }
}