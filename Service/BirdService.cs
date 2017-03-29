using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public class BirdService : IBirdService
    {
        IBirdRepository _birdRepo;

        public BirdService(IBirdRepository repo)
        {
            _birdRepo = repo;
        }

        public IEnumerable<BirdSurveyor> GetAllSurveyors()
        {
            return _birdRepo.GetAllSurveyors();
        }
        public IEnumerable<BirdSpecies> GetAllSpecies()
        {
            return _birdRepo.GetAllSpecies();
        }

    }
}
