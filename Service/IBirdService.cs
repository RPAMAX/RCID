using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public interface IBirdService
    {
        IEnumerable<BirdSurveyor> GetAllSurveyors();
        IEnumerable<BirdSpecies> GetAllSpecies();
        IEnumerable<BirdSurvey> GetAllSurveys();

        bool UpdateSpecies(BirdSpecies item);
        bool CreateSpecies(BirdSpecies item);
               
        bool UpdateSurvey(BirdSurvey item);
        bool CreateSurvey(BirdSurvey item);

        bool UpdateSurveyor(BirdSurveyor item);
        bool CreateSurveyor(BirdSurveyor item);
    }
}
