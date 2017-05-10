using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public class PhytoService : IPhytoService
    {
        IPhytoRepository _PhytoRepo;        

        public PhytoService(IPhytoRepository repo)
        {
            _PhytoRepo = repo;            
        }

      
        public IEnumerable<PhytoSpecies> GetAllSpecies()
        {
            return _PhytoRepo.GetAllSpecies();
        }
        

        public IEnumerable<PhytoDivision> GetAllDivisions()
        {
            return _PhytoRepo.GetAllDivisions();
        }

        public IEnumerable<PhytoSurvey> GetAllSurveys()
        {
            return _PhytoRepo.GetAllSurveys();
        }
        

        public IEnumerable<PhytoSurveyDetails> GetSurveyDetails(short speciesID, int surveyID)
        {
            return _PhytoRepo.GetSurveyDetails(speciesID, surveyID);
        }

        public bool UpdateSpecies(PhytoSpecies item) {
            return _PhytoRepo.UpdateSpecies(item);
        }

        public bool CreateSpecies(PhytoSpecies item)
        {
            return _PhytoRepo.CreateSpecies(item);
        }

        public bool InactivateSpecies(PhytoSpecies item)
        {
            return _PhytoRepo.InactivateSpecies(item);
        }
                

        public bool UpdateDivision(PhytoDivision item)
        {
            return _PhytoRepo.UpdateDivision(item);
        }

        public bool CreateDivision(PhytoDivision item)
        {
            return (_PhytoRepo.CreateDivision(item)>0);
        }

        public bool InactivateDivision(PhytoDivision item)
        {
            return _PhytoRepo.InactivateDivision(item);
        }


        public bool UpdateSurvey(PhytoSurvey item)
        {
            return _PhytoRepo.UpdateSurvey(item);
        }

        public bool CreateSurvey(PhytoSurvey item)
        {
            return (_PhytoRepo.CreateSurvey(item) > 0);
        }

        public bool InactivateSurvey(PhytoSurvey item)
        {
            return _PhytoRepo.InactivateSurvey(item);
        }

        

        public bool UpdateSurveyDetail(PhytoSurveyDetails item)
        {
            return _PhytoRepo.UpdateSurveyDetail(item);
        }

        public bool CreateSurveyDetail(PhytoSurveyDetails item)
        {
            return (_PhytoRepo.CreateSurveyDetail(item) > 0);
        }

        public bool InactivateSurveyDetail(PhytoSurveyDetails item)
        {
            return _PhytoRepo.InactivateSurveyDetail(item);
        }

    }
}
