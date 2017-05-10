using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public interface IPhytoRepository
    {
        
        IEnumerable<PhytoSpecies> GetAllSpecies();
        IEnumerable<PhytoDivision> GetAllDivisions();
        IEnumerable<PhytoSurvey> GetAllSurveys();        
        IEnumerable<PhytoSurveyDetails> GetSurveyDetails(short speciesID, int surveyID);

        bool UpdateSpecies(PhytoSpecies item);
        bool CreateSpecies(PhytoSpecies item);
        bool InactivateSpecies(PhytoSpecies item);            

        bool UpdateDivision(PhytoDivision item);
        int CreateDivision(PhytoDivision item);
        bool InactivateDivision(PhytoDivision item);

        bool UpdateSurvey(PhytoSurvey item);
        int CreateSurvey(PhytoSurvey item);
        bool InactivateSurvey(PhytoSurvey item);

        bool UpdateSurveyDetail(PhytoSurveyDetails item);
        short CreateSurveyDetail(PhytoSurveyDetails item);
        bool InactivateSurveyDetail(PhytoSurveyDetails item);

    }
}
