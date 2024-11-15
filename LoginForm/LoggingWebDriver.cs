using log4net;
using OpenQA.Selenium;
using System.Collections.ObjectModel;


namespace LoginForm
{
    public class LoggingWebDriver : IWebDriver
    {
        private readonly IWebDriver _driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(LoggingWebDriver));

        public LoggingWebDriver(IWebDriver driver)
        {
            _driver = driver;
            log4net.Config.XmlConfigurator.Configure(); // Initialize Log4Net
        }

        public string Title => _driver.Title;

        public string Url
        {
            get => _driver.Url;
            set => _driver.Url = value;
        }

        public string PageSource => _driver.PageSource;

        public string CurrentWindowHandle => _driver.CurrentWindowHandle;

        public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;

        public void Dispose()
        {
            Log("Dispose called");
            _driver.Dispose();
        }

        public void Quit()
        {
            Log("Quit called");
            _driver.Quit();
        }

        public IWebElement FindElement(By by)
        {
            Log($"Finding element: {by.ToString()}");
            return _driver.FindElement(by);
        }

        private static void Log(string message)
        {
            // Log messages with Log4Net
            log.Info(message);
        }

        public void Close()
        {
            _driver.Close();
        }

        public IOptions Manage()
        {
            return _driver.Manage();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _driver.FindElements(by);
        }
    }
}
