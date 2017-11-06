using System;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace WeatherServices
{
    public class WeatherService
    {
        private HttpClient request;

        public WeatherService()
        {
            request = new HttpClient();
        }

        public Stream GetStream(string location)
        {
            Task<Stream> stream = request.GetStreamAsync(string.Format(WeatherAuthenticator.Url, location));
            stream.Wait();
            return stream.Result;
        }

        private string Parse(string s, string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(s);
            XmlNode node = doc.SelectSingleNode(name);
            return node.InnerText;
        }

        public ImmediateWeather GetWeather(Stream s)
        {
            ImmediateWeather weather = new ImmediateWeather();
            StreamReader sr = new StreamReader(s);
            string response = sr.ReadToEnd();

            weather.temp = decimal.Parse(Parse(response, "/root/current/temp_f"));
            weather.windSpeed = decimal.Parse(Parse(response, "/root/current/wind_mph"));
            weather.windDir = Parse(response, "/root/current/wind_dir");
            weather.clouds = Parse(response, "/root/current/condition/text");
            weather.city = Parse(response, "/root/location/name");
            weather.state = Parse(response, "/root/location/region");
            weather.country = Parse(response, "/root/location/country");

            return weather;
        }
    }
}
