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
            Console.WriteLine("Enter a location: ");
            string location = Console.ReadLine();

            string apiKey = "92670dc496a0877627cb1a898b463721";
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + apiKey;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();

            JObject weatherData = JObject.Parse(json);
            Console.WriteLine("Current weather in " + location + ":");
            Console.WriteLine("Temperature: " + weatherData["main"]["temp"] + " degrees Fahrenheit");
            Console.WriteLine("Humidity: " + weatherData["main"]["humidity"] + "%");
            Console.WriteLine("Wind speed: " + weatherData["wind"]["speed"] + " mph");
            Console.WriteLine("Pressure: " + weatherData["main"]["pressure"] + " hPa");

            Console.ReadKey();
        }
    }
}
