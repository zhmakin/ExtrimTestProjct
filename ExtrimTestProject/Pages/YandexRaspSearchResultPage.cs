using ExtrimTestProject.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExtrimTestProject.Pages
{
    public class YandexRaspSearchResultPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private String from, to, date;
        private By tripsNameLocator = By.XPath("//h3[@class='SegmentTitle__header']");
        private By tripsLinkLocator = By.XPath("//a[@class='Link SegmentTitle__link']");
        private By tripsTimeLocator = By.XPath("//span[@class='SearchSegment__time']");
        private By tripsDurationLocator = By.XPath("//div[@class='SearchSegment__duration']");
        private By TripsPriceLocator = By.XPath("//span[@class='Price SuburbanTariffs__buttonPrice']");
        private By currencySelectorLocator = By.XPath("//div[@class='Select CurrencySelect']/button");
        private By UsdLocator = By.XPath("//div[@data-value='USD']");
        private By RubLocator = By.XPath("//div[@data-value='RUB']");

        [FindsBy(How = How.XPath, Using = "//header[@class='SearchTitle']/span/h1/span")]
        protected IWebElement tripDeclaration { get; set; }

        [FindsBy(How = How.XPath, Using = "//header[@class='SearchTitle']/span[4]")]
        protected IWebElement tripDate { get; set; }

        [FindsBy(How = How.XPath, Using = "//article[@class='SearchSegment']")]
        public IList<IWebElement> Segments { get; set; }

        public YandexRaspSearchResultPage(IWebDriver driver, WebDriverWait wait, String from, String to, String date)
        {
            this.from = from;
            this.to = to;
            this.date = date;
            this.driver = driver;
            this.wait = wait;
            PageFactory.InitElements(driver, this);
        }

        public void AssertFindResultIsCorrect()
        {
            try
            {
                tripDeclaration.Click();
                Assert.IsTrue(tripDeclaration.Text.Contains(from) & tripDeclaration.Text.Contains(to), "!!!Trip declaration is not correct!!!");
                Assert.IsTrue(tripDate.Text.ToLower().Contains(this.date.ToLower()), "!!!Trip date is not correct!!!");
                Console.WriteLine(@"Date and declaration is OK!");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine(@"Trips that matched the search criteria were not found. Try again or change search.");
                return;
            }
        }

        public Trip FindEarliestTrip(String timeFrom, String timeTo, int priceFrom, int priceTo)
        {
            try
            {
                Trip trip = null;
                Int32 tripPrice = 0;
                IList<IWebElement> tripsName = driver.FindElements(tripsNameLocator);
                IList<IWebElement> tripsLink = driver.FindElements(tripsLinkLocator);
                IList<IWebElement> tripsTime = driver.FindElements(tripsTimeLocator);
                IList<IWebElement> tripsDuration = driver.FindElements(tripsDurationLocator);
                IList<IWebElement> tripsPrice = driver.FindElements(TripsPriceLocator);
                IWebElement currencySelector = driver.FindElement(currencySelectorLocator);
                currencySelector.Click();
                IWebElement selectorUSD = driver.FindElement(UsdLocator);
                IWebElement selectorRUB = driver.FindElement(RubLocator);
                currencySelector.Click();

                DateTime tripTimeFrom = DateTime.ParseExact(timeFrom, "HH:mm", CultureInfo.InvariantCulture);
                DateTime tripTimeTo = DateTime.ParseExact(timeTo, "HH:mm", CultureInfo.InvariantCulture);
                DateTime tripTime;

                for (int i = 0 ; i < tripsName.Count ; i++)
                {
                    tripTime = DateTime.ParseExact(tripsTime[i * 2].Text, "HH:mm", CultureInfo.InvariantCulture);
                    tripPrice = Int32.Parse(tripsPrice[i].Text.TrimEnd('Р').TrimEnd(' '));
                    if (tripTime >= tripTimeFrom && 
                        tripTime <= tripTimeTo && 
                        tripPrice <= priceTo && 
                        tripPrice >= priceFrom)
                    {
                        trip = new Trip(
                            tripsName[i].Text,                  //trip name
                            tripsTime[i * 2 + 1].Text,          //arrival time
                            tripsTime[i * 2].Text,              //derture time
                            tripsDuration[i].Text,                                 //duration time
                            tripPrice,                          //RUB price
                            "",                                 //USD price
                            tripsLink[i].GetAttribute("href")   //trip link
                            );
                        trip.From = from;           //departure point name
                        trip.To = to;               //arrival point name
                        currencySelector.Click();
                        selectorUSD.Click();
                        trip.PriceUSD = tripsPrice[i].Text.Trim('$', ' ');
                        currencySelector.Click();
                        selectorRUB.Click();
                        return trip;
                    }
                }
                return null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}