using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessmentDomain.Entites
{
    public class Weather
    {
        public string City { get; set; }
        public string Country { get; set; }
        public decimal Temperature { get; set; }
        public decimal FeelsLike { get; set; }
        public string Description { get; set; }
        public int Humidity { get; set; }
        public decimal WindSpeed { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
