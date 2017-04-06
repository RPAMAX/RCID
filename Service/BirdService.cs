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

        public IEnumerable<BirdSurvey> GetAllSurveys()
        {
            return _birdRepo.GetAllSurveys();
        }

        public bool UpdateSpecies(BirdSpecies item) {
            return _birdRepo.UpdateSpecies(item);
        }

        public bool CreateSpecies(BirdSpecies item)
        {
            return _birdRepo.CreateSpecies(item);
        }

        public bool UpdateSurvey(BirdSurvey item)
        {
            return _birdRepo.UpdateSurvey(item);
        }

        public bool CreateSurvey(BirdSurvey item)
        {
            return _birdRepo.CreateSurvey(item);
        }

        public bool UpdateSurveyor(BirdSurveyor item)
        {
            return _birdRepo.UpdateSurveyor(item);
        }

        public bool CreateSurveyor(BirdSurveyor item)
        {
            return _birdRepo.CreateSurveyor(item);
        }

    }
}
