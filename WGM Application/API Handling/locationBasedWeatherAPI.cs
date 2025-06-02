using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

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


        public void FetchWeatherForLocation(string location)
        {
            System.Diagnostics.Trace.WriteLine("Hello world!");
        }
    }
}
