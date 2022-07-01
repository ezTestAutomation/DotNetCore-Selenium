using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Extensions;
using Selenium.DotNetCore.Demo.NUnit.Environment;

namespace Selenium.DotNetCore.Demo.NUnit
{
    public class ElementVisibleTest : DriverTestFixture
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
        public void IsElementVisisble()
        {
            driver.Navigate().GoToUrl("https://stackoverflow.com/questions/3401343/scroll-element-into-view-with-selenium");
            var result = driver.IsElementVisisble("Xpath://*[@id='feed-link']/a");
            //Assert.IsTrue(result, "Element visible");
        }

        [Test]
        public void IsElementVisisbleBy()
        {
            driver.Navigate().GoToUrl("https://stackoverflow.com/questions/3401343/scroll-element-into-view-with-selenium");
            var element = By.XPath("//*[@id='feed-link']/a");
            //var result = driver.IsElementVisisble(element);
            //Assert.IsTrue(result, "Element visible");
        }
    }

}
