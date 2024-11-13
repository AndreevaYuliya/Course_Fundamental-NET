using FluentAssertions;
using LoginForm;
using OpenQA.Selenium;
using Serilog;

namespace LoginFormTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class LoginTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [OneTimeSetUp]
        public void SetupLogging()
        {
            // Настройка Serilog для логирования
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Логгер инициализирован.");
        }

        [SetUp]
        public void Setup()
        {
            string browser = TestContext.Parameters.Get("browser", "chrome");
            Log.Information($"Запуск теста в браузере {browser}");
            driver = WebDriverSingleton.GetDriver(browser);
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);

            // Enter login credentials (replace with actual login data)
            loginPage.EnterUsername("standard_user"); // Enter the username
            loginPage.EnterPassword("secret_sauce"); // Enter the password

            // Submit the login form by clicking the login button
            loginPage.ClickLogin();
        }

        [TearDown]
        public void Teardown()
        {
            Log.Information("Завершение теста.");
            driver?.Dispose();
            WebDriverSingleton.QuitDriver();
        }

        [Test]
        public void TestLoginWithEmptyCredentials()
        {
            Log.Information("Тест входа с пустыми данными запущен.");
            loginPage.EnterUsername("").EnterPassword("").ClickLogin();
            var errorMessage = loginPage.GetErrorMessage();
            _ = errorMessage.Should().Be("Epic sadface: Username and password do not match any user in this service");
            Log.Information("Тест входа с пустыми данными завершен успешно.");
        }

        [Test]
        public void TestLoginWithOnlyUsername()
        {
            Log.Information("Тест входа только с именем пользователя запущен.");
            loginPage.EnterUsername("standard_user").EnterPassword("").ClickLogin();
            var errorMessage = loginPage.GetErrorMessage();
            errorMessage.Should().Be("Epic sadface: Username and password do not match any user in this service");
            Log.Information("Тест входа только с именем пользователя завершен успешно.");
        }

        [Test]
        public void TestLoginWithValidCredentials()
        {
            Log.Information("Тест входа с корректными данными запущен.");
            loginPage.EnterUsername("standard_user").EnterPassword("secret_sauce").ClickLogin();

            try
            {
                var pageTitle = driver.FindElement(By.ClassName("login_logo")).Text;
                pageTitle.Should().Be("Swag Labs");
                Log.Information("Тест входа с корректными данными завершен успешно.");
            }
            catch (NoSuchElementException ex)
            {
                Log.Error("Элемент не найден: " + ex.Message);
                Assert.Fail("Элемент не найден");
            }
        }

        [OneTimeTearDown]
        public void CleanupLogging()
        {
            Log.Information("Закрытие логгера.");
            Log.CloseAndFlush();
        }
    }
}