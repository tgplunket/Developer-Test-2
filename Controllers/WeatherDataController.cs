using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;

namespace App.Controllers
{
    [Route("api/[controller]")]
    public class WeatherDataController : Controller
    {
         

        public WeatherDataController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private readonly IHttpClientFactory _httpClientFactory;

        [HttpGet]
        [Route("WeatherForecasts")]
        public async Task<ActionResult<string>> WeatherForecasts()
        {
            var client = _httpClientFactory.CreateClient("OpenWeatherMap");
            var maxRetryAttempts = 3;
            var pauseBetweenFailures = TimeSpan.FromSeconds(2);

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);

            var result = "";
            var retries = 0;
            await retryPolicy.ExecuteAsync(async () =>
            {
                result = await client.GetStringAsync("");
                Console.WriteLine("Weather API request attempt: " + retries + "/" + maxRetryAttempts);
            });
            return result;
        }
    }
}
