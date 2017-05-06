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
        IEnumerable<FishGenerator> GetAllGenerators();
        IEnumerable<FishSurvey> GetAllSurveys();
        IEnumerable<FishSurveyLocation> GetSurveyLocations(int surveyID);
        IEnumerable<FishSurveyDetails> GetSurveyDetails(int locationID, int surveyID);

        bool UpdateSpecies(FishSpecies item);
        bool CreateSpecies(FishSpecies item);
        bool InactivateSpecies(FishSpecies item);
        FishSpecies GetSpeciesByName(string name);

        bool UpdateSpeciesGroup(FishSpeciesGroup item);
        bool CreateSpeciesGroup(FishSpeciesGroup item);
        bool InactivateSpeciesGroup(FishSpeciesGroup item);
        FishSpeciesGroup GetSpeciesGroupByName(string name);

        bool UpdateGenerator(FishGenerator item);
        bool CreateGenerator(FishGenerator item);
        bool InactivateGenerator(FishGenerator item);
        
        bool UpdateSurvey(FishSurvey item);
        int CreateSurvey(FishSurvey item);
        bool InactivateSurvey(FishSurvey item);

        bool UpdateSurveyLocation(FishSurveyLocation item);
        int CreateSurveyLocation(FishSurveyLocation item);
        bool InactivateSurveyLocation(FishSurveyLocation item);

        bool UpdateSurveyDetail(FishSurveyDetails item);
        short CreateSurveyDetail(FishSurveyDetails item);
        bool InactivateSurveyDetail(FishSurveyDetails item);

    }
}
