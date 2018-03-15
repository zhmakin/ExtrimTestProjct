using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtrimTestProject.Pages
{
    public class Trip
    {
        private string arrivalTime;
        private string departureTime;
        private string durationTime;
        private Int32 priceRUB;
        private string priceUSD;
        private string name;
        private string link;
        private string from;
        private string to;

        public Trip(string name, string arrivalTime, string departureTime, string durationTime, Int32 priceRUB, string priceUSD, string link)
        {
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            DurationTime = durationTime;
            PriceRUB = priceRUB;
            PriceUSD = priceUSD;
            Name = name;
            Link = link;
        }

        public string DurationTime { get => durationTime; set => durationTime = value; }
        public int PriceRUB { get => priceRUB; set => priceRUB = value; }
        public string PriceUSD { get => priceUSD; set => priceUSD = value; }
        public string Name { get => name; set => name = value; }
        public string Link { get => link; set => link = value; }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string DepartureTime { get => departureTime; set => departureTime = value; }
        public string ArrivalTime { get => arrivalTime; set => arrivalTime = value; }
    }
}
