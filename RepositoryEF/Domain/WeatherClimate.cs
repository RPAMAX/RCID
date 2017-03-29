using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository.Domain
{
    public class WeatherClimate
    {
        public byte ClimateID { get; set; }
        public string ClimateName { get; set; }
        public bool ClimateActive { get; set; }
    }
}
