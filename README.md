### TestcontainersDynamicGrid 
project contains the example of Dynamic Selenium Grid setup with Testcontainers, using programmatic-compose approach.

You'll need ```Testcontainers``` NuGet package to get started.

[E2ETest.cs](TestcontainersDynamicGrid/E2ETest.cs) contains test that will run with "chrome" and "firefox" Fixtures.
[GridSetup.cs](TestcontainersDynamicGrid/GridSetup.cs) contains programmatic setup of Selenium Hub and Nodes with Testcontainers and example of how to get a dynamic ```connectionsString```.
By default Testcontainers start containers on random ports, to reduce ports-conflict probability.

### TestcontainersSeleniumStandalone
project contains the example of Testcontainers WebDriver module, that starts Selenium Standalone

You'll need ```Testcontainers``` and ```Testcontainers.WebDriver``` NuGet packages to get started.

[HomePageTest.cs](TestcontainersSeleniumStandalone/HomePageTest.cs) contains tests and WebDriver setup using ```WebDriverBuilder```.
