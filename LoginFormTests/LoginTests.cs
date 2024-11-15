using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using log4net;
using LoginForm;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace LoginFormTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class LoginTests
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginTests));
        private IWebDriver? driver;
        private LoginPage? loginPage;

        [OneTimeSetUp]
        public void SetupLogging()
        {
            // Initialize Log4Net
            log.Info("Logger initialized.");
        }

        private static IWebDriver GetWebDriver(string browser)
        {
            return browser.ToLower() switch
            {
                "chrome" => new ChromeDriver(),
                "firefox" => new FirefoxDriver(),
                _ => throw new ArgumentException("Unsupported browser"),
            };
        }

        [Test]
        [TestCase("chrome")]
        [TestCase("firefox")]

        [Given(@"I am on the login page")]
        public void GivenTestLoginWithValidCredentials(string browser)
        {
            log.Info($"Starting test in browser: {browser}");
            driver = GetWebDriver(browser);
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);


            log.Info("Navigated to the login page.");
        }

        [When(@"I attempt to login with empty credentials")]
        public void WhenTestLoginWithValidCredentials()
        {
            loginPage.EnterUsername("standard_user").EnterPassword("secret_sauce").ClickLogin();
            log.Info("Attempted login with valid credentials.");
        }

        [Then(@"Wait for the page to load and check the page title")]
        public void ThenTestLoginWithValidCredentials()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => d.Url.Contains("/inventory.html"));
            log.Info("Verified that login with valid credentials redirects to the home page.");

            driver.Quit(); // Cleanup driver after each test
        }

        [Test]
        [TestCase("chrome")]
        [TestCase("firefox")]

        [Given(@"I am on the login page")]
        public void GivenTestLoginWithOnlyUsername(string browser)
        {
            log.Info($"Starting test in browser: {browser}");
            driver = GetWebDriver(browser);
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);


            log.Info("Navigated to the login page.");
        }

        [When(@"I enter valid credentials and submit")]
        public void WhenTestLoginWithOnlyUsername()
        {
            loginPage.EnterUsername("standard_user").EnterPassword("").ClickLogin();
            log.Info("Attempted login with only username credentials.");
        }

        [Then(@"Wait for the page to load and check the page title")]
        public void ThenTestLoginWithOnlyUsername()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => d.Url.Contains("/inventory.html"));
            log.Info("Verified that login with only username redirects to the home page.");

            driver.Quit(); // Cleanup driver after each test
        }

        [Test]
        [TestCase("chrome")]
        [TestCase("firefox")]

        [Given(@"I am on the login page")]
        public void GivenTestLoginWithEmptyCredentials(string browser)
        {
            log.Info($"Starting test in browser: {browser}");
            driver = GetWebDriver(browser);
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);


            log.Info("Navigated to the login page.");
        }

        [When(@"I attempt to login with empty credentials")]
        public void WhenTestLoginWithEmptyCredentials()
        {
            loginPage.EnterUsername("").EnterPassword("").ClickLogin();
            log.Info("Attempted login with empty credentials.");
        }

        [Then(@"Wait for the page to load and check the page title")]
        public void ThenTestLoginWithEmptyCredentials()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => d.Url.Contains("/inventory.html"));
            log.Info("Verified that login with empty credentials redirects to the home page.");

            driver.Quit(); // Cleanup driver after each test
        }
     
        [TearDown]
        public void Teardown()
        {
            log.Info("Test finished.");
            driver?.Dispose();
        }
    }
}
