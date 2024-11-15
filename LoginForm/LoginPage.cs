using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace LoginForm
{
    public static class UserCredentialsFactory
    {
        private static readonly List<string> AcceptedUsernames = new List<string>
    {
        "standard_user",
        "locked_out_user",
        "problem_user",
        "performance_glitch_user",
        "error_user",
        "visual_user"
    };

        public static IEnumerable<string> GetUsernames()
        {
            foreach (var username in AcceptedUsernames)
            {
                yield return username;
            }
        }
    }
    public class LoginPageBuilder
    {
        private IWebDriver _driver;
        private string _username;
        private readonly string _password = "secret_sauce"; // Constant password

        public LoginPageBuilder SetDriver(IWebDriver driver)
        {
            _driver = driver;
            return this;
        }

        public LoginPageBuilder SetUsername(string username)
        {
            _username = username;
            return this;
        }

        public LoginPage Build()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.EnterUsername(_username);
            loginPage.EnterPassword(_password);
            return loginPage;
        }
    }

    public class LoginPage(IWebDriver driver)
    {
        private IWebElement UsernameField => driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordField => driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => driver.FindElement(By.Id("login-button"));

        public LoginPage EnterUsername(string username)
        {
            UsernameField.Clear();
            var usernameField = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                        .Until(d => UsernameField);
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
            var loginButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                              .Until(d => LoginButton);
            loginButton.Click();
        }

        public string GetErrorMessage()
        {
            try
            {
                return driver.FindElement(By.XPath("//h3[@data-test='error']")).Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }
    }

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
                        // Параметризация браузера
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
}
