using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ExpectedConditions = OpenQA.Selenium.Support.UI.ExpectedConditions;

namespace WeatherForcast.Selenium.Tests;

public class HomePage
{
  readonly string _testUrl = "https://testcontainers.com/";

  private readonly IWebDriver _driver;
  private readonly WebDriverWait _wait;

  public HomePage(IWebDriver driver)
  {
    _driver = driver;
    _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
  }

  // Go to the designated page
  public void GoToPage()
  {
    _driver.Navigate().GoToUrl(_testUrl);
  }

  // Returns the header string
  public String GetPageHeader()
  {
    By element = By.XPath("//*/h1");
    _wait.Until(ExpectedConditions.ElementIsVisible(element));
    return _driver.FindElement(element).Text;
  }

  // Returns selected language
  public String GetSelectedLanguage()
  {
    By element = By.XPath("//*[@id=\"content\"]/main/section[2]/div/div[2]/div[1]/button[1]");
    _wait.Until(ExpectedConditions.ElementIsVisible(element));
    return _driver.FindElement(element).Text;
  }
}
