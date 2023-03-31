using System;
using System.Collections.Generic;

namespace WeatherAPI.Models
{
    public class Coord
    {
        public decimal lon { get; set; }
        public decimal lat { get; set; }
    }

    public class WeatherType
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public decimal temp { get; set; }
        public decimal feels_like { get; set; }
        public decimal temp_min { get; set; }
        public decimal temp_max { get; set; }
        public decimal pressure { get; set; }
        public decimal humidity { get; set; }
        public decimal sea_level { get; set; }
        public decimal grnd_level { get; set; }
    }

    public class Wind
    {
        public decimal speed { get; set; }
        public int deg { get; set; }
        public decimal gust { get; set; }
    }

    public class Rain
    {
        public decimal _1h { get; set; }
    }

    public class Weather
    {

        public Weather()
        {
            coordinate = new Coord();
            weather = new List<WeatherType>();
            main = new Main();
            wind = new Wind();
            rain = new Rain();
        }

        public Coord coordinate { get; set; }
        public List<WeatherType> weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        //"rain": {
        //                  "1h": 3.16
        //},
        //"clouds": {
        //                  "all": 100
        //},
        //"dt": 1661870592,
        //"sys": {
        //                  "type": 2,
        //  "id": 2075663,
        //  "country": "IT",
        //  "sunrise": 1661834187,
        //  "sunset": 1661882248
        //},
        //"timezone": 7200,
        //"id": 3163858,
        //"name": "Zocca",
        //"cod": 200
        //          }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }



    }
}
