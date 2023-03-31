using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Controllers;
using WeatherAPI.Models;
using Xunit;

namespace API.test
{
    public class WeatherControllerTest
    {
        private static readonly string APIKey = "cfc93e8c658bbba73ae13ac1b5357687";
        
        [Fact]
        public void GetWeather_Return_Success()
        {
            string cityName = "London";

            var client = A.Fake<HttpClient>();
            var httpClientFactory = A.Fake<IHttpClientFactory>();
            A.CallTo(() => httpClientFactory.CreateClient("weathermap")).Returns(client);
            var controller = new WeatherForecastController(httpClientFactory);

            var actionResult = controller.Get(cityName);
            var result = actionResult.Result as OkObjectResult;
            var returnWeather = result.Value as Weather;

            Assert.Equal(cityName, returnWeather.name);
        }

        [Fact]
        public void GetWeather_Return_NotFound()
        {
            string cityName = "L";

            var client = A.Fake<HttpClient>();
            var httpClientFactory = A.Fake<IHttpClientFactory>();

            var httpResponseMessage = new HttpResponseMessage();
            A.CallTo(() => httpClientFactory.CreateClient("weathermap")).Returns(client);
            A.CallTo(() => client.GetAsync("?q=" + cityName + "&appid=" + APIKey)).Returns(httpResponseMessage);

            var controller = new WeatherForecastController(httpClientFactory);
            var actionResult = controller.Get(cityName);
            var result = actionResult.Result as ObjectResult;
            var returnWeather = result.Value as WeatherAPI.Models.Exception;

            Assert.Equal(HttpStatusCode.NotFound.ToString(), returnWeather.cod);
        }

        [Fact]
        public void GetWeather_Return_InternalServerError()
        {
            string cityName = "London";

            var client = A.Fake<HttpClient>();
            var httpClientFactory = A.Fake<IHttpClientFactory>();
            var exception = A.Fake<System.Exception>();

            var httpResponseMessage = new HttpResponseMessage();
            A.CallTo(() => httpClientFactory.CreateClient("weathermap")).Returns(client);
            A.CallTo(() => client.GetAsync("?q=" + cityName + "&appid=" + APIKey)).Throws(exception);

            var controller = new WeatherForecastController(httpClientFactory);
            var actionResult = controller.Get(cityName);
            var result = actionResult.Result as StatusCodeResult;
            
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
