using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Extensions;
using Selenium.DotNetCore.Demo.NUnit.Environment;

namespace Selenium.DotNetCore.Demo.NUnit
{
    public class ElementContainTest : DriverTestFixture
    {
        [SetUp]
        public void SetupMethod()
        {

        }

        [TearDown]
        public void TearDownMethod()
        {
            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void ElementContain()
        {
            driver.Navigate().GoToUrl("https://www.guru99.com/drag-drop-selenium.html");
            var result = driver.ElementShouldContains("ClassName:level1" , "Home");
            //Assert.IsTrue(result, "Element visible");
        }

        [Test]
        public void ElementContainBy()
        {
            driver.Navigate().GoToUrl("https://www.guru99.com/drag-drop-selenium.html");
            var element = By.ClassName("level1");
            var result = driver.ElementShouldContains(element, "Home");
            //Assert.IsTrue(result, "Element visible");
        }
    }
}
