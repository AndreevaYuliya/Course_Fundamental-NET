using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace LoginForm
{
    // Singleton-класс для управления WebDriver
    public static class WebDriverSingleton
    {
        private static IWebDriver? driver;
        private static readonly object _lock = new object();    

        public static IWebDriver GetDriver(string browser)
        {
            if (driver == null)
            {
                lock (_lock)
                {
                    if (driver == null)
                    {
                        // Параметризация браузера
                        driver = browser.ToLower() switch
                        {
                            "firefox" => new FirefoxDriver(),
                            "chrome" => new ChromeDriver(),
                            _ => throw new ArgumentException("Unsupported browser")
                        };
                        driver.Manage().Window.Maximize();
                    }
                }
            }
            return driver;
        }

        public static void QuitDriver()
        {
            driver?.Quit();
            driver = null;
        }
    }

    // Page Object для страницы входа
    public class LoginPage(IWebDriver driver)
    {
        private IWebElement UsernameField => driver.FindElement(By.Id("user-name"));
        private IWebElement PasswordField => driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => driver.FindElement(By.Id("login-button"));
        private IWebElement ErrorMessage => driver.FindElement(By.CssSelector("[data-test='error']"));

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

        public string GetErrorMessage() => ErrorMessage.Text;
    }
}
