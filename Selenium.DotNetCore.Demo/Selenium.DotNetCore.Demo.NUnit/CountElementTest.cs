using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Extensions;
using Selenium.DotNetCore.Demo.NUnit.Environment;

namespace Selenium.DotNetCore.Demo.NUnit
{
    public class CountElementTest : DriverTestFixture
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
        public void CountElement()
        {
            driver.Navigate().GoToUrl("https://www.guru99.com/drag-drop-selenium.html");
            var result = driver.CountElement("ClassName:level1");
            Assert.IsTrue(result == 17, "Element visible");
        }

        [Test]
        public void CountElementBy()
        {
            driver.Navigate().GoToUrl("https://www.guru99.com/drag-drop-selenium.html");
            var element = By.ClassName("level1");
            var result = driver.CountElement(element);
            Assert.IsTrue(result == 17, "Element visible");
        }
    }
}
