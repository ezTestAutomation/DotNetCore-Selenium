using NUnit.Framework;
using Selenium.DotNetCore.Demo.NUnit.Environment;
using OpenQA.Selenium.Interactions;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Extensions;

namespace Selenium.DotNetCore.Demo.NUnit
{
    public class DragAndDropExtensionTest : DriverTestFixture
    {
        [SetUp]
        public void SetupMethod()
        {
            driver.Url = EnvironmentManager.Instance.UrlBuilder.WhereIs(@"Account/Users");
        }

        [TearDown]
        public void TearDownMethod()
        {
            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void DragAndDropByTest()
        {
            driver.Navigate().GoToUrl("http://demo.guru99.com/test/drag_drop.html");
            //Element(BANK) which need to drag.		
            //Element(BANK) which need to drag.		
            var From1 = By.XPath("//*[@id='credit2']/a");

            //Element(DEBIT SIDE) on which need to drop.		
            var To1 = By.XPath("//*[@id='bank']/li");

            //Element(SALES) which need to drag.		
            var From2 = By.XPath("//*[@id='credit1']/a");

            //Element(CREDIT SIDE) on which need to drop.  		
            var To2 = By.XPath("//*[@id='loan']/li");

            //Element(500) which need to drag.		
            var From3 = By.XPath("//*[@id='fourth']/a");

            //Element(DEBIT SIDE) on which need to drop.		
            var To3 =By.XPath("//*[@id='amt7']/li");

            //Element(500) which need to drag.		
            var From4 = By.XPath("//*[@id='fourth']/a");

            //Element(CREDIT SIDE) on which need to drop.		
            var To4 = By.XPath("//*[@id='amt8']/li");

            //BANK drag and drop.		
            driver.DragAndDropElement(From1, To1);

            ////SALES drag and drop.		
            driver.DragAndDropElement(From2, To2);

            ////500 drag and drop debit side.		
            driver.DragAndDropElement(From3, To3);

            ////500 drag and drop credit side. 		
            driver.DragAndDropElement(From4, To4);
        }

        [Test]
        public void DragAndDropTest()
        {
            driver.Navigate().GoToUrl("http://demo.guru99.com/test/drag_drop.html");
        
            //BANK drag and drop.		
            driver.DragAndDropElement("id:credit2", "id:bank");

            ////SALES drag and drop.		
            driver.DragAndDropElement("id:credit1", "id:loan");

            ////500 drag and drop debit side.		
            driver.DragAndDropElement("id:fourth", "id:amt7");

            ////500 drag and drop credit side. 		
            driver.DragAndDropElement("id:fourth", "id:amt8");
        }

        [Test]
        public void DragAndDropElementOffSet()
        {
            driver.Navigate().GoToUrl("http://demo.guru99.com/test/drag_drop.html");

          
            //BANK drag and drop.		
            driver.DragAndDropElementByOffSet("id:credit2", 100, 100);

            ////SALES drag and drop.		
            driver.DragAndDropElementByOffSet("id:credit1", 100, 100);

            ////500 drag and drop debit side.		
            driver.DragAndDropElementByOffSet("id:fourth", 100, 100);

            ////500 drag and drop credit side. 		
            driver.DragAndDropElementByOffSet("id:fourth", 100, 100);
        }

        [Test]
        public void DragAndDropElementByOffSet()
        {
            driver.Navigate().GoToUrl("http://demo.guru99.com/test/drag_drop.html");
            var From1 = By.XPath("//*[@id='credit2']/a");

            //Element(SALES) which need to drag.		
            var From2 = By.XPath("//*[@id='credit1']/a");

            //Element(500) which need to drag.		
            var From3 = By.XPath("//*[@id='fourth']/a");

            //Element(500) which need to drag.		
            var From4 = By.XPath("//*[@id='fourth']/a");
            //BANK drag and drop.		
            driver.DragAndDropElementByOffSet(From1, 100, 100);

            ////SALES drag and drop.		
            driver.DragAndDropElementByOffSet(From2, 100, 100);

            ////500 drag and drop debit side.		
            driver.DragAndDropElementByOffSet(From3, 100, 100);

            ////500 drag and drop credit side. 		
            driver.DragAndDropElementByOffSet(From4, 100, 100);
        }
    }
}
