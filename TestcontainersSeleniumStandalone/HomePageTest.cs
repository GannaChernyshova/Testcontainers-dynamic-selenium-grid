using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Testcontainers.WebDriver;
using WeatherForcast.Selenium.Tests;

namespace TestProject1;

public class Tests
{
  internal IWebDriver driver;
  private WebDriverContainer _webDriverContainer;

  [SetUp]
  public async Task Init()
  {
    _webDriverContainer = new WebDriverBuilder()
      .WithImage("selenium/standalone-firefox:123.0")
      .Build();

    await _webDriverContainer.StartAsync();
    driver = new RemoteWebDriver(new Uri(_webDriverContainer.GetConnectionString()), new FirefoxOptions());

  }


  [Test]
  public void SelectedLanguageShouldBeJava()
  {
    HomePage homePage = new HomePage(driver);
    homePage.GoToPage();
    String currentText = homePage.GetSelectedLanguage();
    Assert.That(currentText.Equals("Java"), "Current result is: " + currentText);
  }


  [Test]
  public void MainPageHeaderShouldBeCorrect()
  {
    HomePage homePage = new HomePage(driver);
    homePage.GoToPage();
    String currentText = homePage.GetPageHeader();
    Assert.That(currentText.Equals("Unit tests with real dependencies"), "Current result is: " + currentText);
  }


  [TearDown]
  public void Cleanup()
  {
    _webDriverContainer.DisposeAsync();
    driver.Dispose();
  }

}
