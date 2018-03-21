using System;
using BoDi;
using Coypu;
using Coypu.Drivers;
using dotenv.net;
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
            var testRootPath = AppDomain.CurrentDomain.BaseDirectory;
            var envPath = testRootPath.Substring(0, testRootPath.IndexOf("bin"));
            DotEnv.Config(true, envPath + ".env");
            _browser = new BrowserSession(new SessionConfiguration { AppHost = "localhost", Browser =Browser.Chrome });
            _objectContainer.RegisterInstanceAs(_browser);
        }

        [BeforeScenario("@with_local_smtp_server")]
        public void StartLocalSmtpServer()
        {
            _smtpServer = SimpleSmtpServer.Start(25);
            _objectContainer.RegisterInstanceAs(_smtpServer);
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
