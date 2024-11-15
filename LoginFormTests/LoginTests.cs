using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using LoginForm;

namespace LoginFormTests
{
    // Test class for login functionality using NUnit and SpecFlow
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class LoginFormTests
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginFormTests));
        private IWebDriver? _driver;
        private LoginPage? _loginPage;

        [OneTimeSetUp]
        public void SetupLogging()
        {
            log.Info("Logger initialized.");
        }

        // Helper method to initialize the WebDriver and navigate to the login page
        private void SetupTest(string browser)
        {
            _driver = WebDriverSingleton.GetDriver(browser);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60); // Increase to 60 seconds
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _loginPage = new LoginPage(_driver);
        }


        // SpecFlow Given step for valid credentials
        [Test]
        [TestCaseSource(typeof(LoginTestCaseData), nameof(LoginTestCaseData.GetValidLoginTestCases))]
        [Given(@"I am on the login page")]
        public void GivenTestLoginWithValidCredentials(string username, string password, string browser)
        {
            log.Info($"Starting test in browser: {browser} for user: {username}");
            SetupTest(browser);

            // ќжидание между пользовател€ми
            Thread.Sleep(5000);  // ќжидание 5 секунд между проверками пользователей

            // Additional assertion to confirm the page has loaded
            Assert.IsTrue(_driver!.Title.Contains("Swag Labs"), "Login page title is incorrect.");
        }

        [When(@"I attempt to login with valid credentials")]
        public void WhenTestLoginWithValidCredentials(string username, string password)
        {
            if (_loginPage == null)
            {
                throw new InvalidOperationException("_loginPage is not initialized.");
            }

            _loginPage.EnterUsername(username)
                      .EnterPassword(password)
                      .ClickLogin();
            log.Info($"Attempted login with username: {username}.");
        }

        [Then(@"Wait for the page to load and check the page title")]
        public void ThenTestLoginWithValidCredentials()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            try
            {
                // Handle case where login was successful (inventory page)
                wait.Until(d => d.Url.Contains("/inventory.html"));
                log.Info("Verified that login redirects to the home page.");

                // Assert that the URL is correct
                Assert.IsTrue(_driver!.Url.Contains("/inventory.html"), "Login failed or incorrect page loaded.");

                // Additional check to ensure inventory page is loaded
                var inventoryHeader = wait.Until(d => d.FindElement(By.ClassName("inventory_list")));
                Assert.IsNotNull(inventoryHeader, "Inventory list did not load correctly.");
            }
            catch (WebDriverTimeoutException)
            {
                // Handle failed login attempts (invalid credentials, empty fields, etc.)
                log.Info("Login attempt failed or page did not load.");
                Assert.IsTrue(_driver!.Url.Contains("login"), "Login page did not reload as expected.");
            }

            // Cleanup driver after each test
            Thread.Sleep(5000);  // Delay if needed
            _driver.Quit();
        }

        // SpecFlow Given step for username only
        [Test]
        [TestCaseSource(typeof(LoginTestCaseData), nameof(LoginTestCaseData.GetUsernameOnlyTestCases))]
        [Given(@"I am on the login page")]
        public void GivenTestLoginWithUsernameOnly(string username, string password, string browser)
        {
            log.Info($"Starting test in browser: {browser} for user: {username}");
            SetupTest(browser);

            // ќжидание между пользовател€ми
            Thread.Sleep(5000);  // ќжидание 5 секунд между проверками пользователей

            // Additional assertion to confirm the page has loaded
            Assert.IsTrue(_driver!.Title.Contains("Swag Labs"), "Login page title is incorrect.");
        }

        [When(@"I attempt to login with username only")]
        public void WhenTestLoginWithUsernameOnly(string username, string password)
        {
            if (_loginPage == null)
            {
                throw new InvalidOperationException("_loginPage is not initialized.");
            }

            _loginPage.EnterUsername(username)
                      .EnterPassword(password)
                      .ClickLogin();
            log.Info($"Attempted login with username: '{username}' and password: '{password}'.");
        }

        [Then(@"Wait for the page to load and check the page title")]
        public void ThenTestLoginWithUsernameOnly()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            try
            {
                // Handle case where login was successful (inventory page)
                wait.Until(d => d.Url.Contains("/inventory.html"));
                log.Info("Verified that login redirects to the home page.");

                // Assert that the URL is correct
                Assert.IsTrue(_driver!.Url.Contains("/inventory.html"), "Login failed or incorrect page loaded.");

                // Additional check to ensure inventory page is loaded
                var inventoryHeader = wait.Until(d => d.FindElement(By.ClassName("inventory_list")));
                Assert.IsNotNull(inventoryHeader, "Inventory list did not load correctly.");
            }
            catch (WebDriverTimeoutException)
            {
                // Handle failed login attempts (invalid credentials, empty fields, etc.)
                log.Info("Login attempt failed or page did not load.");
                Assert.IsTrue(_driver!.Url.Contains("login"), "Login page did not reload as expected.");
            }

            // Cleanup driver after each test
            Thread.Sleep(5000);  // Delay if needed
            _driver.Quit();
        }

        // SpecFlow Given step for empty credentials
        [Test]
        [TestCaseSource(typeof(LoginTestCaseData), nameof(LoginTestCaseData.GetEmptyCredentialsTestCases))]
        [Given(@"I am on the login page")]
        public void GivenTestLoginWithEmptyCredentials(string username, string password, string browser)
        {
            log.Info($"Starting test in browser: {browser} for user: {username}");
            SetupTest(browser);

            // ќжидание между пользовател€ми
            Thread.Sleep(5000);  // ќжидание 5 секунд между проверками пользователей

            // Additional assertion to confirm the page has loaded
            Assert.IsTrue(_driver!.Title.Contains("Swag Labs"), "Login page title is incorrect.");
        }
        [When(@"I attempt to login with empty credentials")]
        public void WhenTestLoginWithEmptyCredentials(string username, string password)
        {
            if (_loginPage == null)
            {
                throw new InvalidOperationException("_loginPage is not initialized.");
            }

            _loginPage.EnterUsername(username)
                      .EnterPassword(password)
                      .ClickLogin();
            log.Info($"Attempted login with username: '{username}' and password: '{password}'.");
        }

        [Then(@"Wait for the page to load and check the page title")]
        public void ThenTestLoginWithEmptyCredentials()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            try
            {
                // Handle case where login was successful (inventory page)
                wait.Until(d => d.Url.Contains("/inventory.html"));
                log.Info("Verified that login redirects to the home page.");

                // Assert that the URL is correct
                Assert.IsTrue(_driver!.Url.Contains("/inventory.html"), "Login failed or incorrect page loaded.");

                // Additional check to ensure inventory page is loaded
                var inventoryHeader = wait.Until(d => d.FindElement(By.ClassName("inventory_list")));
                Assert.IsNotNull(inventoryHeader, "Inventory list did not load correctly.");
            }
            catch (WebDriverTimeoutException)
            {
                // Handle failed login attempts (invalid credentials, empty fields, etc.)
                log.Info("Login attempt failed or page did not load.");
                Assert.IsTrue(_driver!.Url.Contains("login"), "Login page did not reload as expected.");
            }

            // Cleanup driver after each test
            Thread.Sleep(5000);  // Delay if needed
            _driver.Quit();
        }

        [TearDown]
        public void Teardown()
        {
            log.Info("Test finished.");

            if (_driver != null)
            {
                try
                {
                    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                    wait.Until(d => d.Url.Contains("login"));
                }
                catch (Exception e)
                {
                    log.Error("Error waiting for the page to load: " + e.Message);
                }
                finally
                {
                    _driver.Quit();
                    _driver.Dispose();
                }
            }

            WebDriverSingleton.QuitDriver();
        }
    }
}
