using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace AppAutomate;

[Binding]
public class Hooks
{
    public static AndroidDriver? Driver;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        try
        {
            AppiumOptions capabilities = new()
            {
                App = "AndroidSample"
            };
            capabilities.AddAdditionalAppiumOption("os_version", "9.0");
            capabilities.AddAdditionalAppiumOption("device", "Google Pixel 3");
            capabilities.AddAdditionalAppiumOption("project", "AndroidSample");
            capabilities.AddAdditionalAppiumOption("build", "1");
            capabilities.AddAdditionalAppiumOption("name", "FirstTest");
            capabilities.AddAdditionalAppiumOption("browserstack.acceptInsecureCerts", "false");

            var bsConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings").GetSection("Browserstack");
            capabilities.AddAdditionalAppiumOption("browserstack.user", bsConfig.GetSection("User").Value);
            capabilities.AddAdditionalAppiumOption("browserstack.key", bsConfig.GetSection("Key").Value);

            var remoteAddress = new Uri("https://hub.browserstack.com/wd/hub");
            Driver = new AndroidDriver(remoteAddress, capabilities, TimeSpan.FromSeconds(30));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}