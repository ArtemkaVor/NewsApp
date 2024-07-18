using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public Weather[] weather { get; set; }
        public Wind Wind { get; set; }
        public string Name { get; set; }

        public List[] List { get; set; }
    }
}
