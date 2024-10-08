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
            var uK = new UnitedKingdom(){ Label = "ggg" };
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
            var uK2 = new UnitedKingdom() { Label = "ggg" };          

            var summary = "V2 Extremely Mild!" + uK2.Label;

            if (temp >= 32)
            {
                summary = "v2 Hot hot hot!" + uK2.Label;
            }
            else if (temp <= 16 && temp > 0)
            {
                summary = "V2 Freezing!" + uK2.Label;
            }
            else if (temp <= 0)
            {
                summary = "V2 Somewhat chilly!" + uK2.Label;
            }

            return summary;
        }
    }
}
