using System;
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

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void StartBrowserSession()
        {
            _browser = new BrowserSession(new SessionConfiguration { AppHost = "localhost", Browser =Browser.Chrome });
            _objectContainer.RegisterInstanceAs(_browser);
        }

        [BeforeScenario]
        public void InitEnvFile()
        {
            var testRootPath = AppDomain.CurrentDomain.BaseDirectory;
            var envPath = testRootPath.Substring(0, testRootPath.IndexOf("bin"));
            DotEnv.Config(true, envPath + ".env");
        }

        [BeforeScenario("@with_local_smtp_server")]
        public void StartLocalSmtpServer()
        {
            _smtpServer = SimpleSmtpServer.Start(25);
            _objectContainer.RegisterInstanceAs(_smtpServer);
        }

        [BeforeScenario]
        public void CleanDb()
        {
            using (var db = new MailerDbEntities())
            {
                db.Database.ExecuteSqlCommand("delete from contact");
            }
        }

        [AfterScenario]
        public void DisposeBrowserSession()
        {
            _browser.Dispose();
        }

        [AfterScenario("@with_local_smtp_server")]
        public void DisposeSmtpServer()
        {
            _smtpServer.Stop();
        }
    }
}
