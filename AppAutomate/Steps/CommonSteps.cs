using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace AppAutomate.Steps;

[Binding]
public class CommonSteps
{
    private readonly ScenarioContext _scenarioContext;

    public CommonSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    private void AllowNotification()
    {
        var button =
            (AppiumElement)new WebDriverWait(_scenarioContext.Get<AppiumDriver>("driver"), TimeSpan.FromSeconds(30)).Until(
                ElementToBeClickable(By.Id("com.android.permissioncontroller:id/permission_allow_button")));
        button?.Click();
    }

    private void AllowLocationData()
    {
        var button =
            (AppiumElement)new WebDriverWait(_scenarioContext.Get<AppiumDriver>("driver"), TimeSpan.FromSeconds(30)).Until(
                ElementToBeClickable(By.Id("android:id/button1")));
        button?.Click();
    }

    private void AllowLocationWhileUsingTheApp()
    {
        var button =
            (AppiumElement)new WebDriverWait(_scenarioContext.Get<AppiumDriver>("driver"), TimeSpan.FromSeconds(30)).Until(
                ElementToBeClickable(By.XPath("//android.widget.Button[@text='While using the app']")));
        button?.Click();
    }

    private void AllowPhoneCallPermission()
    {
        var button =
            (AppiumElement)new WebDriverWait(_scenarioContext.Get<AppiumDriver>("driver"), TimeSpan.FromSeconds(30)).Until(
                ElementToBeClickable(By.Id("com.android.permissioncontroller:id/permission_allow_button")));
        button?.Click();
    }

    [When(@"Allow all system permission")]
    public void WhenAllowAllSystemPermission()
    {
        AllowNotification();
        AllowLocationData();
        AllowLocationWhileUsingTheApp();
        AllowPhoneCallPermission();
    }

    [Then(@"The application is loaded")]
    public void ThenTheApplicationIsLoaded()
    {
        var element = (AppiumElement)new WebDriverWait(_scenarioContext.Get<AppiumDriver>("driver"), TimeSpan.FromSeconds(30)).Until(
            ElementIsVisible(By.Id("com.mds.volo:id/editTextPassword")));
        Assert.IsTrue(element is not null);
    }

    [Given(@"Local driver running")]
    public void GivenLocalDriverRunning()
    {
        var driver = Drivers.CreateLocalAndroidDriver();
        _scenarioContext.Set(driver, "driver");
    }

    [Given(@"BrowserStack driver running")]
    public void GivenBrowserStackDriverRunning()
    {
        var driver = Drivers.CreateBrowserStackAndroidDriver();
        _scenarioContext.Set(driver, "driver");
    }
}