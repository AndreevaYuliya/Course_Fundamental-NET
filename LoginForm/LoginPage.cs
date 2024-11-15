using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace LoginForm
{
    // LoginPage class to interact with the page elements
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private IWebElement UsernameField => _driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;         
        }

        public LoginPage EnterUsername(string username)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var usernameField = wait.Until(d => UsernameField);
            usernameField.Clear();
            usernameField.SendKeys(username);
            return this;
        }


        public LoginPage EnterPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            return this;
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }

        public string GetErrorMessage()
        {
            try
            {
                return _driver.FindElement(By.XPath("//h3[@data-test='error']")).Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }
    }

    // WebDriver Singleton class to ensure only one instance of WebDriver is used across tests
    public static class WebDriverSingleton
    {
        private static IWebDriver? _driver;
        private static readonly object _lock = new object();

        public static IWebDriver GetDriver(string browser)
        {
            if (_driver == null)
            {
                lock (_lock)
                {
                    if (_driver == null)
                    {
                        _driver = browser.ToLower() switch
                        {
                            "firefox" => new FirefoxDriver(),
                            "chrome" => new ChromeDriver(),
                            _ => throw new ArgumentException("Unsupported browser")
                        };
                        _driver.Manage().Window.Maximize();
                    }
                }
            }
            return _driver;
        }

        public static void QuitDriver()
        {
            _driver?.Quit();
            _driver = null;
        }
    }

    // Test case data for login scenarios
    public static class LoginTestCaseData
    {
        // Returns test cases for valid credentials
        public static IEnumerable<object[]> GetValidLoginTestCases()
        {
            yield return new object[] { "standard_user", "secret_sauce", "chrome" };
            yield return new object[] { "locked_out_user", "secret_sauce", "chrome" };
            yield return new object[] { "problem_user", "secret_sauce", "chrome" };
            yield return new object[] { "performance_glitch_user", "secret_sauce", "chrome" };
            yield return new object[] { "error_user", "secret_sauce", "chrome" };
            yield return new object[] { "visual_user", "secret_sauce", "chrome" };
            yield return new object[] { "standard_user", "secret_sauce", "firefox" };
            yield return new object[] { "locked_out_user", "secret_sauce", "firefox" };
            yield return new object[] { "problem_user", "secret_sauce", "firefox" };
            yield return new object[] { "performance_glitch_user", "secret_sauce", "firefox" };
            yield return new object[] { "error_user", "secret_sauce", "firefox" };
            yield return new object[] { "visual_user", "secret_sauce", "firefox" };
        }

        // Returns test cases for empty credentials
        public static IEnumerable<object[]> GetEmptyCredentialsTestCases()
        {
            yield return new object[] { "", "", "chrome" };
            yield return new object[] { "", "", "firefox" };
        }

        // Returns test cases for username only (no password)
        public static IEnumerable<object[]> GetUsernameOnlyTestCases()
        {
            yield return new object[] { "standard_user", "", "chrome" };
            yield return new object[] { "locked_out_user", "", "chrome" };
            yield return new object[] { "problem_user", "", "chrome" };
            yield return new object[] { "performance_glitch_user", "", "chrome" };
            yield return new object[] { "error_user", "", "chrome" };
            yield return new object[] { "visual_user", "", "chrome" };
            yield return new object[] { "standard_user", "", "firefox" };
            yield return new object[] { "locked_out_user", "", "firefox" };
            yield return new object[] { "problem_user", "", "firefox" };
            yield return new object[] { "performance_glitch_user", "", "firefox" };
            yield return new object[] { "error_user", "", "firefox" };
            yield return new object[] { "visual_user", "", "firefox" };
        }
    }
}
