using System.Net;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using WeatherInfo;

namespace Api
{
    public class HttpTrigger
    {
        private readonly ILogger _logger;

        public HttpTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger>();
        }

        [Function("WeatherForecast")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var uK = new UnitedKingdom();
            uk.Label = "test";
            var randomNumber = new Random();
            var temp = 0;

            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = temp = randomNumber.Next(-20, 55),
                Summary = GetSummary(temp)
            }).ToArray();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(result);

            return response;
        }

        private string GetSummary(int temp)
        {
            var uK2 = new UnitedKingdom();
            uk2.Label = "test";

            var summary = "V2 Extremely Mild!" + uk2.Label;

            if (temp >= 32)
            {
                summary = "v2 Hot hot hot!";
            }
            else if (temp <= 16 && temp > 0)
            {
                summary = "V2 Freezing!";
            }
            else if (temp <= 0)
            {
                summary = "V2 Somewhat chilly!";
            }

            return summary;
        }
    }
}
