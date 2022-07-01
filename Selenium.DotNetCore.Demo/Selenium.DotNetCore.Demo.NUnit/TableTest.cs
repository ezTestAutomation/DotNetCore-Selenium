using NUnit.Framework;
using Selenium.DotNetCore.Demo.NUnit.Environment;
using OpenQA.Selenium.Extensions;
using OpenQA.Selenium;

namespace Selenium.DotNetCore.Demo.NUnit
{
    public class TableTest : DriverTestFixture
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
		public void GetDataInCell_UT()
		{
			driver.Navigate().GoToUrl("https://www.w3schools.com/html/html_tables.asp");
			var cell = driver.GetDataInCell(By.XPath("//*[@id='customers']"), 2, "Contact");
			Assert.AreEqual(cell == "Francisco Chang", true);
		}
	}
}
