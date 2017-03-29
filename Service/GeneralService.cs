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

    }
}
