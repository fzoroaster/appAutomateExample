using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace AppAutomate;

public class Drivers
{
    public static AndroidDriver CreateLocalAndroidDriver()
    {
        var appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings");
        var bsConfig = appSettings.GetSection("Browserstack");
        var bsEnvironment = bsConfig.GetSection("Environment");

        AppiumOptions appiumOptions = new()
        {
            DeviceName = bsEnvironment.GetSection("DeviceName").Value,
            AutomationName = "UiAutomator2"
        };

        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.Udid, appSettings.GetSection("Udid").Value);
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, appSettings.GetSection("AppPackage").Value);
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, appSettings.GetSection("AppActivity").Value);
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.NoReset, false);
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, false);

        return new AndroidDriver(new Uri(appSettings.GetSection("AppiumAddress").Value ?? string.Empty), appiumOptions, TimeSpan.FromSeconds(30));
    }

    public static AndroidDriver? CreateBrowserStackAndroidDriver()
    {
        var bsConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings").GetSection("Browserstack");

        var bsEnvironment = bsConfig.GetSection("Environment");
        var bsCapabilities = bsConfig.GetSection("Capabilities");
        // https://www.browserstack.com/app-automate/capabilities#test-capabilities

        AppiumOptions driverOptions = new()
        {
            DeviceName = bsEnvironment.GetSection("DeviceName").Value,
            App = bsConfig.GetSection("App").Value
        };
        driverOptions.AddAdditionalAppiumOption("os_version", bsEnvironment.GetSection("OsVersion").Value);
        driverOptions.AddAdditionalAppiumOption("project", bsCapabilities.GetSection("Project").Value);
        driverOptions.AddAdditionalAppiumOption("build", bsCapabilities.GetSection("Build").Value);
        driverOptions.AddAdditionalAppiumOption("name", bsCapabilities.GetSection("RouteManagerMobile").Value);
        driverOptions.AddAdditionalAppiumOption("browserstack.acceptInsecureCerts", bsCapabilities.GetSection("BrowserstackAcceptInsecureCerts").Value);

        var remoteAddress = new Uri($"https://{bsConfig.GetSection("User").Value}:{bsConfig.GetSection("Key").Value}@hub.browserstack.com/wd/hub");

        return new AndroidDriver(remoteAddress, driverOptions);
    }
}