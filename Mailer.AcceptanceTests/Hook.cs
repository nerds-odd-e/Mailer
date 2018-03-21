using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using Coypu;
using Coypu.Drivers;
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
