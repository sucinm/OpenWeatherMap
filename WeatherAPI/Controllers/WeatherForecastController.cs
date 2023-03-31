//using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherAPI.Models;
//using Exception = WeatherAPI.Models.Exception;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly string APIKey = "cfc93e8c658bbba73ae13ac1b5357687";

        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string cityName)
        {

            try
            {
                Weather weatherInfo = new Weather();

                var client = _httpClientFactory.CreateClient("weathermap");
                HttpResponseMessage Res = await client.GetAsync("?q=" + cityName + "&appid=" + APIKey);

                if (Res.IsSuccessStatusCode)
                {

                    var ObjResponse = Res.Content.ReadAsStringAsync().Result;
                    weatherInfo = JsonSerializer.Deserialize<Weather>(ObjResponse);
                    return Ok(weatherInfo);
                }
                else
                {
                    Exception exception = new Exception();
                    var ObjResponse = Res.Content.ReadAsStringAsync().Result;
                    exception = JsonSerializer.Deserialize<Exception>(ObjResponse);

                    return StatusCode(System.Int32.Parse(exception.cod), exception);
                }
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        
    }
}
