using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientInAspNetCoreWebAPI
{
    public interface IWeatherService
    {
        Task<string> Get(string cityName);
    }

    public class WeatherService : IWeatherService
    {
        private HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Get(string cityName)
        {
            var apiKey = "4098eb2761a94910840120844212706";
            string APIURL = $"?key={apiKey}&q={cityName}";
            var response = await _httpClient.GetAsync(APIURL);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
