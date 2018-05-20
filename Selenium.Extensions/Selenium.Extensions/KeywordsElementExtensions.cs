namespace OpenQA.Selenium.Extensions
{
	using OpenQA.Selenium.Interactions;
	using OpenQA.Selenium.Support.UI;
	using System;
	using System.Linq;

	public static class KeywordsElementExtensions
	{
		#region Messages

		const string Msg_Default = "None";
		const string Msg_Element_Not_Exist_Or_Invisible = "The element '{0}' not exist or invisible.";
		const string Msg_ElementShouldContains = "The expected value '{0}' and actual value '{1}' does not match.";
		const string Msg_Element_Not_Enable = "The element '{0}' not enable.";
		const string Msg_Element_Not_Visible = "The element '{0}' not visiable.";

		#endregion

		#region Internal method

		/// <summary>
		/// Build message
		/// </summary>
		/// <param name="custom1"></param>
		/// <param name="custom2"></param>
		/// <returns></returns>
		private static string CustomMessage(string custom1, string custom2)
		{
			if (custom1 == Msg_Default)
				return custom2;
			return custom1;
		}

		/// <summary>
		/// Find element by locator
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="by"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		private static IWebElement Find(IWebDriver webDriver, By by, int timeout = -1, bool scroll = false)
		{
			if (timeout != -1)
				webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);

			var element = webDriver.FindElement(by);
			if (element != null)
				return element;

			if (scroll)
			{
				var jscommand = $"window.scroll(0, {element.Location.Y});";
				webDriver.ExecuteJavaScript(jscommand);

				WebDriverWait wait = new WebDriverWait(webDriver, webDriver.Manage().Timeouts().ImplicitWait);
				return wait.Until((condition) =>
				{
					element = condition.FindElement(by);
					if (element.Displayed &&
						element.Enabled &&
						element.GetAttribute("aria-disabled") == null)
					{
						return element;
					}
					return null;
				});
			}

			return null;
		}

		#endregion

		#region Javascript & action

		/// <summary>
		/// Execute javascript command
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="javaScriptCommand"></param>
		/// <returns></returns>
		public static object ExecuteJavaScript(this IWebDriver webDriver, string javaScriptCommand)
		{
			return ((IJavaScriptExecutor)webDriver).ExecuteScript(javaScriptCommand);
		}

		/// <summary>
		/// Scroll element to view
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		public static void ScrollElementToView(this IWebDriver webDriver, string locator)
		{
			var webElement = Find(webDriver, ByExtensions.ByLocator(locator));
			Actions actions = new Actions(webDriver);
			actions.MoveToElement(webElement);
			actions.Perform();
		}

		/// <summary>
		/// Scroll element to view
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="by"></param>
		/// <returns></returns>
		public static bool ScrollElementToView(this IWebDriver webDriver, By by)
		{
			var webElement = Find(webDriver, by);
			Actions actions = new Actions(webDriver);
			actions.MoveToElement(webElement);
			actions.Perform();
			return true;
		}

		/// <summary>
		/// Count element
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static int CountElement(this IWebDriver webDriver, string locator, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(locator), timeout, scroll);
				var element = webDriver.FindElements(ByExtensions.ByLocator(locator));
				return element.Count;
			}
			catch (NoSuchElementException)
			{
				return 0;
			}
		}

		/// <summary>
		/// Count element
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <returns></returns>
		public static int CountElement(this IWebDriver webDriver, By locator)
		{
			try
			{
				var element = webDriver.FindElements(locator);
				return element.Count;
			}
			catch (NoSuchElementException)
			{
				return 0;
			}
		}

		#endregion

		#region Verify

		/// <summary>
		/// Verify data
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <param name="expectedValue"></param>
		/// <param name="message"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static string ElementShouldContains(this IWebDriver webDriver, string locator, string expectedValue, string message = Msg_Default, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(locator), timeout, scroll);
				if (webElement.Text == expectedValue)
					return string.Empty;
				return CustomMessage(message, string.Format(Msg_ElementShouldContains, expectedValue, webElement.Text));
			}
			catch (NoSuchElementException)
			{
				return CustomMessage(message, string.Format(Msg_Element_Not_Exist_Or_Invisible, locator));
			}
		}

		/// <summary>
		/// Verify data
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="by"></param>
		/// <param name="expectedValue"></param>
		/// <param name="message"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static string ElementShouldContains(this IWebDriver webDriver, By by, string expectedValue, string message = Msg_Default, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, by, timeout, scroll);
				if (webElement.Text == expectedValue)
					return string.Empty;
				return CustomMessage(message, string.Format(Msg_ElementShouldContains, expectedValue, webElement.Text));
			}
			catch (NoSuchElementException)
			{
				return CustomMessage(message, string.Format(Msg_Element_Not_Exist_Or_Invisible, by));
			}
		}

		#endregion

		#region Get text
		/// <summary>
		/// Get text of element
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="by"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static string GetText(this IWebDriver webDriver, By by, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, by, timeout, scroll);
				return webElement.Text;
			}
			catch (NoSuchElementException)
			{
				return CustomMessage("", string.Format(Msg_Element_Not_Exist_Or_Invisible, by));
			}
		}

		/// <summary>
		/// Get text of element
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static string GetText(this IWebDriver webDriver, string locator, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(locator), timeout, scroll);
				return webElement.Text;
			}
			catch (NoSuchElementException)
			{
				return CustomMessage("", string.Format(Msg_Element_Not_Exist_Or_Invisible, locator));
			}
		}

		/// <summary>
		/// Get data of cell in data table
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="tableLocator"></param>
		/// <param name="rowIndex"></param>
		/// <param name="columnName"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static string GetDataInCell(this IWebDriver webDriver, By tableLocator, int rowIndex, string columnName, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, tableLocator, timeout, scroll);
				var columnIndex = webElement.FindElements(By.TagName("th")).ToList().FindIndex(f => f.Text == columnName);
				var row = webElement.FindElements(By.TagName("tr")).Skip(rowIndex).Take(1).FirstOrDefault();
				var cell = row.FindElements(By.TagName("td"))[columnIndex];
				return cell.Text;
			}
			catch (NoSuchElementException)
			{
				return CustomMessage("", string.Format(Msg_Element_Not_Exist_Or_Invisible, tableLocator));
			}
		}

		/// <summary>
		/// Get data of cell in data table
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="tableLocator"></param>
		/// <param name="rowIndex"></param>
		/// <param name="columnName"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static string GetDataInCell(this IWebDriver webDriver, string tableLocator, int rowIndex, string columnName, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(tableLocator), timeout, scroll);
				var columnIndex = webElement.FindElements(By.TagName("th")).ToList().FindIndex(f => f.Text == columnName);
				var row = webElement.FindElements(By.TagName("tr")).Skip(rowIndex).Take(1).FirstOrDefault();
				var cell = row.FindElements(By.TagName("td"))[columnIndex];
				return cell.Text;
			}
			catch (NoSuchElementException)
			{
				return CustomMessage("", string.Format(Msg_Element_Not_Exist_Or_Invisible, tableLocator));
			}
		}

		#endregion

		#region Enabled / Visisble

		/// <summary>
		/// Element has not enabled yet?
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="by"></param>
		/// <param name="message"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static string IsElementEnabled(this IWebDriver webDriver, By by, string message = Msg_Default, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, by, timeout, scroll);
				if (webElement.Enabled)
					return string.Empty;
				else
					return CustomMessage(message, string.Format(Msg_Element_Not_Enable, by));
			}
			catch (NoSuchElementException)
			{
				return CustomMessage(message, string.Format(Msg_Element_Not_Exist_Or_Invisible, by));
			}
		}

		/// <summary>
		/// Element has not enabled yet?
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <param name="message"></param>
		/// <param name="timeout"></param>
		/// <param name="scroll"></param>
		/// <returns></returns>
		public static string IsElementVisisble(this IWebDriver webDriver, string locator, string message = Msg_Default, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(locator), timeout, scroll);
				if (webElement.Enabled)
					return string.Empty;
				else
					return CustomMessage(message, string.Format(Msg_Element_Not_Enable, locator));
			}
			catch (NoSuchElementException)
			{
				return CustomMessage(message, string.Format(Msg_Element_Not_Exist_Or_Invisible, locator));
			}
		}

		#endregion

		#region Drap and drop
		/// <summary>
		/// Drap and drop element
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <param name="target"></param>
		public static void DragAndDropElement(this IWebDriver webDriver, string locator, string target)
		{
			Actions ac = new Actions(webDriver);
			var locatorElement = webDriver.FindElement(ByExtensions.ByLocator(locator));
			var targetElement = webDriver.FindElement(ByExtensions.ByLocator(target));
			ac.DragAndDrop(locatorElement, targetElement).Build().Perform();
		}

		/// <summary>
		/// Drap and drop element
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <param name="target"></param>
		public static void DragAndDropElement(this IWebDriver webDriver, By locator, By target)
		{
			Actions ac = new Actions(webDriver);
			ac.DragAndDrop(webDriver.FindElement(locator), webDriver.FindElement(target)).Build().Perform();
		}

		/// <summary>
		/// Drap and drop element by offset
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public static void DragAndDropElementByOffSet(this IWebDriver webDriver, string locator, int x, int y)
		{
			Actions ac = new Actions(webDriver);
			var locatorElement = webDriver.FindElement(ByExtensions.ByLocator(locator));
			ac.DragAndDropToOffset(locatorElement, x, y).Build().Perform();
		}

		/// <summary>
		/// Drap and drop element by offset
		/// </summary>
		/// <param name="webDriver"></param>
		/// <param name="locator"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public static void DragAndDropElementByOffSet(this IWebDriver webDriver, By locator, int x, int y)
		{
			Actions ac = new Actions(webDriver);
			ac.DragAndDropToOffset(webDriver.FindElement(locator), x, y).Build().Perform();
		}

		#endregion
	}
}
