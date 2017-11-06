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
            stream.RunSynchronously();
            return stream.Result;
        }

        private string Parse(XmlReader reader, string name)
        {
            string[] path = name.Split('/');

            for (int i = 0; i < path.Length; i++)
            {
                reader.ReadToDescendant(path[i]);
                if (i + 1 == path.Length)
                    return reader.Value;
            }

            return null;
        }

        public ImmediateWeather GetWeather(Stream response)
        {

            XmlReader reader = XmlReader.Create(response);
            ImmediateWeather weather = new ImmediateWeather();

            weather.temp = decimal.Parse(Parse(reader, "root/current/temp_f"));
            weather.windSpeed = decimal.Parse(Parse(reader, "root/current/wind_mph"));
            weather.windDir = Parse(reader, "root/current/wind_dir");
            weather.clouds = Parse(reader, "root/current/condition/text");
            weather.city = Parse(reader, "root/location/name");
            weather.state = Parse(reader, "root/location/region");
            weather.country = Parse(reader, "root/location/country");

            return weather;
        }
    }
}
