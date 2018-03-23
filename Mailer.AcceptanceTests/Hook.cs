using System;
using System.Threading;
using BoDi;
using Coypu;
using Coypu.Drivers;
using dotenv.net;
using Mailer.Models;
using netDumbster.smtp;
using TechTalk.SpecFlow;

namespace Mailer.AcceptanceTests
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private BrowserSession _browser;
        private SimpleSmtpServer _smtpServer;
        private MailerDbEntities _db;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void StartBrowserSession()
        {
            _browser = new BrowserSession(new SessionConfiguration {AppHost = "localhost", Browser = Browser.Chrome});
            _db = new MailerDbEntities();
            _objectContainer.RegisterInstanceAs(_browser);
            _objectContainer.RegisterInstanceAs(_db);
        }


        [BeforeScenario]
        public void InitEnvFile()
        {
            var testRootPath = AppDomain.CurrentDomain.BaseDirectory;
            var envPath = testRootPath.Substring(0, testRootPath.IndexOf("bin"));
            DotEnv.Config(true, envPath + ".env");
        }

        [BeforeScenario("@with_local_smtp_server")]
        public void CleanReceivedEmails()
        {
            _smtpServer = SimpleSmtpServer.Start(25);
            _objectContainer.RegisterInstanceAs(_smtpServer);

        }

        [BeforeScenario]
        public void CleanDb()
        {
            _db.Database.ExecuteSqlCommand("delete from contact");
            _db.Database.ExecuteSqlCommand("delete from course");
        }

        [AfterScenario]
        public void DisposeBrowserSession()
        {
            _browser.Dispose();
            _db.Dispose();
        }

        [AfterScenario("@with_local_smtp_server")]
        public void DisposeSmtpServer()
        {
            _smtpServer.Stop();
        }
    }
}