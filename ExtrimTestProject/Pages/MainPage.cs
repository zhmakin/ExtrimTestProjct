using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace ExtrimTestProject.Pages
{
    internal class MainPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private readonly string url = @"https://www.yandex.ru";

        public MainPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
            PageFactory.InitElements(driver, this);
        }

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(this.url);
        }

        [FindsBy(How = How.XPath, Using = "//a[@href='https://www.yandex.ru/all']")]
        public IWebElement ExtraMenuViewer { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@href='https://rasp.yandex.ru/']")]
        public IWebElement YandexRaspLink { get; set; }

        public void GoToYandexRaspPage()
        {
            ExtraMenuViewer.Click();
            YandexRaspLink.Click();
        }
    }
}