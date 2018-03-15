using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;

namespace ExtrimTestProject.Pages
{
    public class YandexRaspPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private String from, to, date;


        public YandexRaspPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='fromName']")]
        private IWebElement DepartureTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='toName']")]
        private IWebElement DestinationTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@class='date-input_search__input']")]
        private IWebElement DataField { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@class='y-radio-group_islet-large _init']/label[4]")]
        private IWebElement ElectricTrainButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='y-button_islet-rasp-search _pin-left _init']")]
        private IWebElement FindButton { get; set; }

        public void SelectElectricTrain()
        {
            ElectricTrainButton.Click();
        }

        public void SetTripUnits(string from, string to)
        {
            this.from = from;
            this.to = to;
            DepartureTextField.Clear();
            DepartureTextField.SendKeys(from);
            DestinationTextField.Clear();
            DestinationTextField.SendKeys(to);
        }

        public void DateOfTrip(DateTime date)
        {
            // Culture info.
            CultureInfo ci = new CultureInfo("ru-RU");
            DateTimeFormatInfo dtfi = ci.DateTimeFormat;
            this.date = date.Day.ToString() + " " + dtfi.GetMonthName(date.Month);
            DataField.SendKeys(this.date);
        }

        public void FindTripAction()
        {
            FindButton.Click();
        }
           
        public String getFrom()
        {
            return this.from;
        }

        public String getTo()
        {
            return this.to;
        }

        public String getDate()
        {
            return this.date;
        }
    }
}