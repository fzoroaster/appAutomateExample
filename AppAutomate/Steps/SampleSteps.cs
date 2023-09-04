using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AppAutomate.Steps;

[Binding]
public class SampleSteps
{
    [Given(@"A mobile driver is opened")]
    public void GivenAMobileDriverIsOpened()
    {
        var driver = Hooks.Driver;
    }

    [Then(@"The test is completed")]
    public void ThenTheTestIsCompleted()
    {
        Assert.True(Hooks.Driver is not null);
    }
}