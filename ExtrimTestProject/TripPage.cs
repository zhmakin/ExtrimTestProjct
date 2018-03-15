using ExtrimTestProject.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace ExtrimTestProject
{
    internal class TripPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string link;
        public IList<IWebElement> Stations, DepartureTimes, ArrivalTimes, DurationTimes;
        By stationsLocator = By.XPath("//div[@class='b-timetable__city']/a");
        By departureTimesLocator = By.XPath("//td[@class='b-timetable__cell b-timetable__cell_type_departure']/span/span/strong");
        By arrivalTimeLocators = By.XPath("//td[@class='b-timetable__cell b-timetable__cell_type_arrival']/span/span/strong");
        By durationTimesLocator = By.XPath("//div[@class='b-timetable__pathtime']");

        public TripPage(IWebDriver driver, WebDriverWait wait, string link)
        {
            this.driver = driver;
            this.wait = wait;
            this.link = link;
            PageFactory.InitElements(driver, this);
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(link);
        }

        public void AssertTrip(Trip trip)
        {
            Stations = driver.FindElements(stationsLocator);
            DepartureTimes = driver.FindElements(departureTimesLocator);
            ArrivalTimes = driver.FindElements(arrivalTimeLocators);
            DurationTimes = driver.FindElements(durationTimesLocator);

            Assert.IsTrue(Stations[0].Text.Contains(trip.From));
            Assert.IsTrue(Stations[Stations.Count - 1].Text.Contains(trip.To));
            Assert.AreEqual(DepartureTimes[0].Text,trip.DepartureTime);
            Assert.AreEqual(ArrivalTimes[ArrivalTimes.Count-1].Text, trip.ArrivalTime);
            Assert.AreEqual(DurationTimes[DurationTimes.Count - 1].Text, trip.DurationTime);

            Console.WriteLine(@"Test finished");
        }



    }
}