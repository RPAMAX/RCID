using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public interface IPhytoService
    {
        IEnumerable<PhytoSpecies> GetAllSpecies();
        IEnumerable<PhytoDivision> GetAllDivisions();
        IEnumerable<PhytoSurvey> GetAllSurveys();
        IEnumerable<PhytoSurveyDetails> GetSurveyDetails(int surveyID);

        bool UpdateSpecies(PhytoSpecies item);
        bool CreateSpecies(PhytoSpecies item);
        bool InactivateSpecies(PhytoSpecies item);

        bool UpdateDivision(PhytoDivision item);
        bool CreateDivision(PhytoDivision item);
        bool InactivateDivision(PhytoDivision item);

        bool UpdateSurvey(PhytoSurvey item);
        bool CreateSurvey(PhytoSurvey item);
        bool InactivateSurvey(PhytoSurvey item);

        bool UpdateSurveyDetail(PhytoSurveyDetails item);
        bool CreateSurveyDetail(PhytoSurveyDetails item);
        bool InactivateSurveyDetail(PhytoSurveyDetails item);
    }
}
