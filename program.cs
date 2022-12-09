using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a location (city name, zip code, or city name, country): ");
            string location = Console.ReadLine();

            string apiKey = "92670dc496a0877627cb1a898b463721";
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + apiKey;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string json = reader.ReadToEnd();

                JObject weatherData = JObject.Parse(json);
                Console.WriteLine("Current weather in " + location + ":");
                Console.WriteLine("Temperature: " + weatherData["main"]["temp"] + " degrees Celsius");
                Console.WriteLine("Humidity: " + weatherData["main"]["humidity"] + "%");
                Console.WriteLine("Wind speed: " + weatherData["wind"]["speed"] + " meters per second");
                Console.WriteLine("Pressure: " + weatherData["main"]["pressure"] + " hPa");
                Console.WriteLine("Weather condition: " + weatherData["weather"][0]["main"]);
                Console.WriteLine("Sunrise: " + UnixTimeStampToDateTime(double.Parse(weatherData["sys"]["sunrise"].ToString())));
                Console.WriteLine("Sunset: " + UnixTimeStampToDateTime(double.Parse(weatherData["sys"]["sunset"].ToString())));
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadKey();
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
