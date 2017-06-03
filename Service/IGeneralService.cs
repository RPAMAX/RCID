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
        IEnumerable<SamplePoint> GetAllSamplePoints();

        bool InactivateClimate(WeatherClimate item);
        bool UpdateClimate(WeatherClimate item);
        bool CreateClimate(WeatherClimate item);

        bool InactivateSamplePointArea(SamplePointArea item);
        bool UpdateSamplePointArea(SamplePointArea item);
        bool CreateSamplePointArea(SamplePointArea item);

     
    }
}
