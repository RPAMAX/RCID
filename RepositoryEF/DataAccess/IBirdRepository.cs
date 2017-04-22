using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public interface IBirdRepository
    {
        IEnumerable<BirdSurveyor> GetAllSurveyors();
        IEnumerable<BirdSpecies> GetAllSpecies();
        IEnumerable<BirdSurvey> GetAllSurveys();

        bool UpdateSpecies(BirdSpecies item);
        bool CreateSpecies(BirdSpecies item);
        bool InactivateSpecies(BirdSpecies item);
        BirdSpecies GetSpeciesByName(string name);

        bool UpdateSurvey(BirdSurvey item);
        int CreateSurvey(BirdSurvey item);

        IEnumerable<BirdSurveyDetails> GetSurveyDetails(int surveyID);
        bool CreateSurveyDetail(BirdSurveyDetails item);

        bool UpdateSurveyor(BirdSurveyor item);
        bool CreateSurveyor(BirdSurveyor item);
        bool InactivateSurveyor(BirdSurveyor item);
        BirdSurveyor GetSurveyorByName(string name);


    }
}
