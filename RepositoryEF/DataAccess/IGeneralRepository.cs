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
        IEnumerable<SamplePoint> GetAllSamplePoints();
        SamplePointArea GetSamplePointAreaByName(string name);

        bool InactivateClimate(WeatherClimate item);
        bool UpdateClimate(WeatherClimate item);
        bool CreateClimate(WeatherClimate item);

        bool InactivateSamplePointArea(SamplePointArea item);
        bool UpdateSamplePointArea(SamplePointArea item);
        bool CreateSamplePointArea(SamplePointArea item);

        bool InactivateSamplePoint(SamplePoint item);
        bool UpdateSamplePoint(SamplePoint item);
        bool CreateSamplePoint(SamplePoint item);

    }
}
