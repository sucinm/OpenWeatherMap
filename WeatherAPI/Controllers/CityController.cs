using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController
    {
        private static readonly string[] RandomCities = new[]
        {
            "London", "Jakarta", "Singapura", "Kuala Lumpur", "Seoul"
        };

        private readonly ILogger<CityController> _logger;

        public CityController(ILogger<CityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<City> Get(int? countryID)
        {
            var rng = new Random();
            List<City> cities = Enumerable.Range(1, 5).Select(index => new City
            {
                ID = index,
                countryID = index + 1,
                name = RandomCities[rng.Next(RandomCities.Length)]
            })
            .ToList();

            if(countryID != null)
            {
                return cities.Where(atr => atr.countryID == countryID).ToList();
            }

            return cities;
        }

    }
}
