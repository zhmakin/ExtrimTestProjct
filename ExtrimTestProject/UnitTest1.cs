using System;
using System.Collections.Generic;
using ExtrimTestProject.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ExtrimTestProject
{
    [TestClass]
    public class UnitTest1
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            this.Driver = new FirefoxDriver();
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(5));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }

        [TestMethod]
        public void TestAction()
        {
            //1) Открываем Yandex
            MainPage mp = new MainPage(this.Driver, this.Wait);
            mp.Navigate();

            //2) Переходим в расписания
            mp.GoToYandexRaspPage();

            //3) Найти электрички из Екатеринбурга в Каменск-Уральский на ближайшую субботу.
            YandexRaspPage rp = new YandexRaspPage(this.Driver, this.Wait);
            rp.SelectElectricTrain();
            rp.SetTripUnits("Екатеринбург","Каменск-Уральский");

            var nextSaturday = DateTime.Today.AddDays(7 - (int)(DateTime.Today.DayOfWeek + 1) % 7);
            rp.DateOfTrip(nextSaturday);

            rp.FindTripAction();

            //4) Проверить, что произведен поиск и название таблицы результатов соответствует параметрам поиска.
            YandexRaspSearchResultPage frp = new YandexRaspSearchResultPage(this.Driver, this.Wait, rp.getFrom(), rp.getTo(), rp.getDate());
            frp.AssertFindResultIsCorrect();

            //5) Сохранить данные о рейсе
            Trip earliestTrip = frp.FindEarliestTrip("12:00","23:59", 0, 200);

            //6) Вывести на консоль данные о рейсе
            if (earliestTrip != null){
                Console.WriteLine(
                            earliestTrip.Name + " - " + 
                            earliestTrip.DepartureTime + " по цене: " +
                            earliestTrip.PriceRUB.ToString() + "руб. / $" +
                            earliestTrip.PriceUSD + '.');
            }
            else {
                Console.WriteLine(@"Trips that matched the search criteria were not found. Try again or change search.");
                return;
            }
            //7) Открыть страницу информации о рейсе.
            TripPage tp = new TripPage(this.Driver, this.Wait, earliestTrip.Link);
            tp.Navigate();

            //8) Проверить, что данные о рейсе на странице информации соответствуют
            tp.AssertTrip(earliestTrip);
            
        }
    }
}
