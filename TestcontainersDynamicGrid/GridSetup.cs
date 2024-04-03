using DotNet.Testcontainers.Builders;

namespace WeatherForcast.Selenium.Tests;

[SetUpFixture]
public class GridSetup
{
  public static String WebDriverUrl;

  [OneTimeSetUp]
  public async Task Setup()
  {
    var gridNetwork = new NetworkBuilder()
      .WithName("seleniumgridnetwork")
      .Build();

    var hubEnvironment = new Dictionary<string, string>()
    {
      { "VNC_NO_PASSWORD", "1" },
      { "SE_NODE_MAX_INSTANCES", "4" },
      { "SE_NODE_MAX_SESSIONS", "4" }
    };

    var nodeEnvironment = new Dictionary<string, string>()
    {
      { "SE_EVENT_BUS_HOST", "selenium-hub" },
      { "SE_EVENT_BUS_PUBLISH_PORT", "4442" },
      { "SE_EVENT_BUS_SUBSCRIBE_PORT", "4443" },
      { "SE_NODE_MAX_SESSIONS", "4" },
      { "SE_NODE_MAX_INSTANCES", "4" },
      { "SE_NODE_SESSION_TIMEOUT", "180" }
    };

    var hubContainer = new ContainerBuilder()
      .WithImage("selenium/hub:4.18.1")
      .WithName("selenium-hub")
      .WithNetwork(gridNetwork)
      .WithEnvironment(hubEnvironment)
      .WithPortBinding("4442", "4442")
      .WithPortBinding("4443", "4443")
      .WithPortBinding("4444", "4444")
      .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(4444))
      .Build();

    var fireFoxContainer = new ContainerBuilder()
      .WithImage("selenium/node-firefox:123.0")
      .WithName("ffNode")
      .WithNetwork(gridNetwork)
      .WithEnvironment(nodeEnvironment)
      .Build();

    var chromeContainer = new ContainerBuilder()
      .WithImage("selenium/node-chrome:123.0")
      .WithName("chromeNode")
      .WithNetwork(gridNetwork)
      .WithEnvironment(nodeEnvironment)
      .Build();

    await hubContainer.StartAsync();
    await fireFoxContainer.StartAsync();
    await chromeContainer.StartAsync();

    var connectionsString = "http://localhost:" + hubContainer.GetMappedPublicPort(4444);

    Console.WriteLine("connectionsString = " + connectionsString);
    WebDriverUrl = connectionsString;
  }

}
