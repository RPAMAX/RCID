using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public class GeneralService : IGeneralService
    {
        IGeneralRepository _genRepo;

        public GeneralService(IGeneralRepository repo)
        {
            _genRepo = repo;
        }

        public IEnumerable<WeatherClimate> GetAllClimates()
        {
            return _genRepo.GetAllClimates();
        }

        public IEnumerable<SamplePointArea> GetAllSamplePointAreas()
        {
            return _genRepo.GetAllSamplePointAreas();
        }

        public bool CreateSamplePointArea(SamplePointArea item)
        {
            return _genRepo.CreateSamplePointArea(item);
        }

        public bool InactivateSamplePointArea(SamplePointArea item)
        {
            return _genRepo.InactivateSamplePointArea(item);
        }

        public bool UpdateSamplePointArea(SamplePointArea item)
        {
            return _genRepo.UpdateSamplePointArea(item);
        }

        public bool CreateClimate(WeatherClimate item)
        {
            return _genRepo.CreateClimate(item);
        }

        public bool InactivateClimate(WeatherClimate item)
        {
            return _genRepo.InactivateClimate(item);
        }

        public bool UpdateClimate(WeatherClimate item)
        {
            return _genRepo.UpdateClimate(item);
        }



    }
}
