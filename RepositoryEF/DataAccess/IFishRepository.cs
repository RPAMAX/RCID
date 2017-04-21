using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public interface IFishRepository
    {
        
        IEnumerable<FishSpecies> GetAllSpecies();
        IEnumerable<FishSpeciesGroup> GetAllSpeciesGroup();
        //IEnumerable<BirdSurvey> GetAllSurveys();

        bool UpdateSpecies(FishSpecies item);
        bool CreateSpecies(FishSpecies item);
        FishSpecies GetSpeciesByName(string name);

        bool UpdateSpeciesGroup(FishSpeciesGroup item);
        bool CreateSpeciesGroup(FishSpeciesGroup item);
        FishSpeciesGroup GetSpeciesGroupByName(string name);

     //   bool UpdateSurvey(BirdSurvey item);
     //   int CreateSurvey(BirdSurvey item);

     //   bool CreateSurveyDetail(BirdSurveyDetails item);

       


    }
}
