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
    public class CountryController
    {
        private static readonly string[] RandomCountries = new[]
        {
            "UK", "Indonesia", "Singapura", "Malaysia", "South Korea"
        };

        private readonly ILogger<CountryController> _logger;

        public CountryController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Country> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Country
            {
                ID = index,
                name = RandomCountries[rng.Next(RandomCountries.Length)]
            })
            .ToArray();
        }
    }
}
