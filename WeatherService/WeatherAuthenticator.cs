using System;

namespace WeatherService
{
    sealed class WeatherAuthenticator
    {
        private static readonly string URL_START = "https://api.apixu.com/v1/current.xml";
        private static readonly string URL_KEY = "ff436f1b91a7474aab4223122170710";
        private static readonly string URL = URL_START + "?q={0}&key=" + URL_KEY;

        private WeatherAuthenticator()
        {

        }

        public static string Url
        {
            get
            {
                return URL;
            }
        }

    }
}
