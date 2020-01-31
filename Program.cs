using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeatherApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://api.openweathermap.org/data/2.5/weather?zip=";
            Console.WriteLine("Please enter your zip code");
            string zip = Console.ReadLine();
            Console.WriteLine("Please enter your country code");
            string countrycode = Console.ReadLine();
            

            

            string key = File.ReadAllText("appsettings.debug.json");
            JObject jObject = JObject.Parse(key);
            JToken token = jObject["ApiKey"];
            string apiKey = token.ToString();


            

            string apiCall = $"{url}{zip},{countrycode}&appid={apiKey}";

            var httpRequest = new HttpClient();

            Task<string> response = httpRequest.GetStringAsync(apiCall);
            string newresponse = response.Result;
            JObject jObject2 = JObject.Parse(newresponse);
            var temp = jObject2["main"]["temp"].ToString();
            var converttemp = double.Parse(temp) * 9 / 5 - 459.67;
            converttemp = Math.Round(converttemp, 1);
            Console.WriteLine($"Temperature is {converttemp}");

            
        }
    }
}