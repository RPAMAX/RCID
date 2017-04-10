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
        BirdSpecies GetSpeciesByName(string name);

        bool UpdateSurvey(BirdSurvey item);
        int CreateSurvey(BirdSurvey item);

        bool CreateSurveyDetail(BirdSurveyDetails item);

        bool UpdateSurveyor(BirdSurveyor item);
        bool CreateSurveyor(BirdSurveyor item);
        BirdSurveyor GetSurveyorByName(string name);


    }
}
