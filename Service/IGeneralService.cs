using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public interface IGeneralService
    {
        IEnumerable<WeatherClimate> GetAllClimates();
        IEnumerable<SamplePointArea> GetAllSamplePointAreas();
    }
}
