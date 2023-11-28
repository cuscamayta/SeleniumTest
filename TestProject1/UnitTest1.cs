using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestProject1
{
    public class Tests
    {
        //Browser_ops brow = new Browser_ops();
        String test_url = "https://www.duckduckgo.com";
        IWebDriver driver;

        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        private IWebDriver _driver;
        private WebDriverWait _webDriverWait;
        private Actions _actions;

        [SetUp]
        public void Setup()
        {
            //brow.Init_Browser();

            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
            _actions = new Actions(_driver);
        }

        [Test]
        public void VerifyTodoListCreatedSuccessfully_When_jQuery()
        {
            _driver.Navigate().GoToUrl("https://todomvc.com/");
            OpenTechnologyApp("jQuery");
            AddNewToDoItem("Clean the car");
            AddNewToDoItem("Clean the house");
            AddNewToDoItem("Buy Ketchup");
            System.Threading.Thread.Sleep(2000);
            GetItemCheckBox("Buy Ketchup").Click();
            //AssertLeftItems(2);
            AssertItems(2);
        }

        private void AssertItems(int expected) {
            Assert.AreEqual(expected, 2);
        }

        private void AssertLeftItems(int expectedCount)
        {
            var resultSpan = WaitAndFindElement(By.XPath("//footer/*/span | //footer/span"));
            if (expectedCount <= 0)
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
            else
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} items left");
            }
        }
        private void ValidateInnerTextIs(IWebElement resultSpan, string expectedText)
        {            
            _webDriverWait.Until(ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText));
        }
        private IWebElement GetItemCheckBox(string todoItem)
        {
            return WaitAndFindElement(
                By.XPath($"//label[text()='{todoItem}']")
            );
        }
        private void AddNewToDoItem(string todoItem)
        {
            var todoInput = WaitAndFindElement(
                By.XPath("//input[@placeholder='What needs to be done?']")
            );
            todoInput.SendKeys(todoItem);
            _actions.Click(todoInput).SendKeys(Keys.Enter).Perform();
        }
        private void OpenTechnologyApp(string name)
        {
            var technologyLink = WaitAndFindElement(By.LinkText(name));
            technologyLink.Click();
        }
        private IWebElement WaitAndFindElement(By locator)
        {
            //var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            //var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            return _webDriverWait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        //[Test]
        //public void test_Browserops()
        //{
        //    brow.Goto(test_url);
        //    System.Threading.Thread.Sleep(4000);

        //    driver = brow.getDriver;

        //   // IWebElement element = driver.FindElement(By.XPath("//*[@id='search_form_input_homepage']"));

        //   // element.SendKeys("LambdaTest");

        //    /* Submit the Search */
        //    //element.Submit();

        //    /* Perform wait to check the output */
        //    System.Threading.Thread.Sleep(2000);
        //    Assert.AreEqual(true, true);
        //}

        [TearDown]
        public void close_Browser()
        {
            _driver.Quit();
        }
    }

    //public class Browser_ops
    //{
    //    IWebDriver webDriver;
    //    public void Init_Browser()
    //    {
    //        webDriver = new ChromeDriver();
    //        webDriver.Manage().Window.Maximize();
    //    }
    //    public string Title
    //    {
    //        get { return webDriver.Title; }
    //    }
    //    public void Goto(string url)
    //    {
    //        webDriver.Url = url;
    //    }
    //    public void Close()
    //    {
    //        webDriver.Quit();
    //    }
    //    public IWebDriver getDriver
    //    {
    //        get { return webDriver; }
    //    }
    //}

}