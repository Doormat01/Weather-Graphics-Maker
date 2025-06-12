using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace WGM_Application.API_Handling
{
    internal class locationBasedWeatherAPI
    {
        public static async Task<string> GetTownFromLatLong(double lat, double lon)
        {
            string url = $"https://nominatim.openstreetmap.org/reverse?lat={lat}&lon={lon}&format=json";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "YourAppName"); // Required by Nominatim

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(jsonResponse);
                    return $"{data["address"]?["town"]}, {data["address"]?["state"]}" ?? "Unknown Location";
                }
            }
            return "Location lookup failed.";
        }

        public static async Task<string[]> GetCitySuggestions(string query)
        {

            string url = $"https://nominatim.openstreetmap.org/search?q={query}&format=json&addressdetails=1&countrycodes=us";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "YourAppName"); // Required by Nominatim

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JArray data = JArray.Parse(jsonResponse);

                    return data.Select(d => $"{d["address"]?["town"] ?? d["address"]?["city"]}, {d["address"]?["state"]}")
                               .Where(city => city != ", ") // Filter out empty results
                               .Distinct() // Remove duplicates
                               .ToArray();
                }
            }
            return new string[] { "No suggestions available." };
        }

        public void FetchWeatherForLocation(string location)
        {
            System.Diagnostics.Trace.WriteLine("Hello world!");
        }
    }
}
