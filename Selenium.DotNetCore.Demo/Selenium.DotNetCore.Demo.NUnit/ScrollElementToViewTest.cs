using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Extensions;
using Selenium.DotNetCore.Demo.NUnit.Environment;
using System;
namespace Selenium.DotNetCore.Demo.NUnit
{
    public class ScrollElementToViewTest : DriverTestFixture
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
        public void ScrollElementToView()
        {
            driver.Navigate().GoToUrl("https://stackoverflow.com/questions/3401343/scroll-element-into-view-with-selenium");
            driver.ScrollElementToView("Xpath://*[@id='feed-link']/a");
        }

        [Test]
        public void ScrollElementToViewBy()
        {
            driver.Navigate().GoToUrl("https://stackoverflow.com/questions/3401343/scroll-element-into-view-with-selenium");
            var element = By.XPath("//*[@id='feed-link']/a");
            driver.ScrollElementToView(element);
        }
    }
}
