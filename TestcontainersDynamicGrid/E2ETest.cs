using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace WeatherForcast.Selenium.Tests;

[TestFixture("chrome")]
[TestFixture("firefox")]
public class Tests {

  RemoteWebDriver _driver;
  private readonly String _browser;

  public Tests(String browser)
  {
    _browser = browser;
  }

  [SetUp]
  public void Init()
  {
    if (_browser.Equals("chrome"))
    {
      var options = new ChromeOptions();
      options.AddArgument("headless");
      options.AddArgument("ignore-certificate-errors");
      _driver = new RemoteWebDriver(new Uri(GridSetup.WebDriverUrl), options);
    }
    else if (_browser.Equals("firefox"))
    {
      var options = new FirefoxOptions();
      _driver = new RemoteWebDriver(new Uri(GridSetup.WebDriverUrl), options);
    }
  }

  [Test]
  public void SelectedLanguageShouldBeJava()
  {
    HomePage homePage = new HomePage(_driver);
    homePage.GoToPage();
    String currentText = homePage.GetSelectedLanguage();
    Assert.That(currentText.Equals("Java"), "Current result is: " + currentText);
  }


  [Test]
  public void MainPageHeaderShouldBeCorrect()
  {
    HomePage homePage = new HomePage(_driver);
    homePage.GoToPage();
    String currentText = homePage.GetPageHeader();
    Assert.That(currentText.Equals("Unit tests with real dependencies"), "Current result is: " + currentText);
  }


  [TearDown]
  public void Cleanup()
  {
    _driver.Quit();
    _driver.Dispose();
  }

}
