using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public class FishService : IFishService
    {
        IFishRepository _fishRepo;
        IGeneralRepository _generalRepo;

        public FishService(IFishRepository repo, IGeneralRepository genRepo)
        {
            _fishRepo = repo;
            _generalRepo = genRepo;
        }

      
        public IEnumerable<FishSpecies> GetAllSpecies()
        {
            return _fishRepo.GetAllSpecies();
        }

        public IEnumerable<FishSpeciesGroup> GetAllSpeciesGroup()
        {
            return _fishRepo.GetAllSpeciesGroup();
        }

        public IEnumerable<FishGenerator> GetAllGenerators()
        {
            return _fishRepo.GetAllGenerators();
        }

        public IEnumerable<FishSurvey> GetAllSurveys()
        {
            return _fishRepo.GetAllSurveys();
        }

        public IEnumerable<FishSurveyLocation> GetSurveyLocations(int surveyID)
        {
            return _fishRepo.GetSurveyLocations(surveyID);
        }

        public bool UpdateSpecies(FishSpecies item) {
            return _fishRepo.UpdateSpecies(item);
        }

        public bool CreateSpecies(FishSpecies item)
        {
            return _fishRepo.CreateSpecies(item);
        }

        public bool InactivateSpecies(FishSpecies item)
        {
            return _fishRepo.InactivateSpecies(item);
        }

        public bool UpdateSpeciesGroup(FishSpeciesGroup item)
        {
            return _fishRepo.UpdateSpeciesGroup(item);
        }

        public bool CreateSpeciesGroup(FishSpeciesGroup item)
        {
            return _fishRepo.CreateSpeciesGroup(item);
        }

        public bool InactivateSpeciesGroup(FishSpeciesGroup item)
        {
            return _fishRepo.InactivateSpeciesGroup(item);
        }

        public bool UpdateGenerator(FishGenerator item)
        {
            return _fishRepo.UpdateGenerator(item);
        }

        public bool CreateGenerator(FishGenerator item)
        {
            return _fishRepo.CreateGenerator(item);
        }

        public bool InactivateGenerator(FishGenerator item)
        {
            return _fishRepo.InactivateGenerator(item);
        }


        public bool UpdateSurvey(FishSurvey item)
        {
            return _fishRepo.UpdateSurvey(item);
        }

        public bool CreateSurvey(FishSurvey item)
        {
            return (_fishRepo.CreateSurvey(item) > 0);
        }

        public bool UpdateSurveyLocation(FishSurveyLocation item)
        {
            return _fishRepo.UpdateSurveyLocation(item);
        }

        public bool CreateSurveyLocation(FishSurveyLocation item)
        {
            return (_fishRepo.CreateSurveyLocation(item) > 0);
        }

        
    }
}
