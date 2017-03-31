using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public interface IGeneralRepository
    {
        IEnumerable<WeatherClimate> GetAllClimates();
        IEnumerable<SamplePointArea> GetAllSamplePointAreas();
    }
}
