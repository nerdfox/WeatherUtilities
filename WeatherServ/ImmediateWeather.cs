using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherServ
{
    public struct ImmediateWeather
    {
        public decimal temp;
        public decimal windSpeed;
        public string windDir;
        public string clouds;
        public string city;
        public string state;
        public string country;
    }
}
