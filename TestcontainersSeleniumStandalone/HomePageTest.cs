using DotNet.Testcontainers.Builders;
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
  private String videoFilePath = Path.Combine(CommonDirectoryPath.GetSolutionDirectory().DirectoryPath, "video.mp4");
  
  [SetUp]
  public async Task Init()
  {
    _webDriverContainer = new WebDriverBuilder()
      .WithImage("selenium/standalone-firefox:123.0")
      .WithRecording()
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


  [TearDown]
  public async Task Cleanup()
  {
    await _webDriverContainer.StopAsync()
      .ConfigureAwait(true);
    
    await _webDriverContainer.ExportVideoAsync(videoFilePath)
      .ConfigureAwait(true);
    
    _webDriverContainer.DisposeAsync();
    driver.Dispose();
    
  }

}
