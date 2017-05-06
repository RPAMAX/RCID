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
        IGeneralRepository _generalRepo;

        public BirdService(IBirdRepository repo, IGeneralRepository genRepo)
        {
            _birdRepo = repo;
            _generalRepo = genRepo;
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

        public IEnumerable<BirdSurveyDetails> GetSurveyDetails(int surveyID)
        {
            return _birdRepo.GetSurveyDetails(surveyID);
        }

        public bool UpdateSpecies(BirdSpecies item) {
            return _birdRepo.UpdateSpecies(item);
        }

        public bool CreateSpecies(BirdSpecies item)
        {
            return _birdRepo.CreateSpecies(item);
        }

        public bool InactivateSpecies(BirdSpecies item)
        {
            return _birdRepo.InactivateSpecies(item);
        }

        public bool UpdateSurvey(BirdSurvey item)
        {
            return _birdRepo.UpdateSurvey(item);
        }

        public bool CreateSurvey(BirdSurvey item)
        {
            return (_birdRepo.CreateSurvey(item) > 0);
        }

        public bool UpdateSurveyor(BirdSurveyor item)
        {
            return _birdRepo.UpdateSurveyor(item);
        }

        public bool InactivateSurvey(BirdSurvey item)
        {
            return _birdRepo.InactivateSurvey(item);
        }

        public bool CreateSurveyor(BirdSurveyor item)
        {
            return _birdRepo.CreateSurveyor(item);
        }

        public bool InactivateSurveyor(BirdSurveyor item)
        {
            return _birdRepo.InactivateSurveyor(item);
        }

        public List<List<string>> ValidateImportList(List<BirdSurvey> toSave)
        {
            List<string> SPAErrors = new List<string>();
            List<string> SurveyorErrors = new List<string>();
            List<string> SpeciesErrors = new List<string>();
            List<string> ClimateErrors = new List<string>();
            List<List<string>> validations = new List<List<string>>();

            foreach (BirdSurvey survey in toSave)
            {
                if (_generalRepo.GetSamplePointAreaByName(survey.SamplePointAreaName) == null) SPAErrors.Add(survey.SamplePointAreaName);

                
                survey.ClimateID = this.GetWeatherIDByDate(survey.SurveyDate);

                if (_birdRepo.GetSurveyorByName(survey.SurveyorName) == null) SurveyorErrors.Add(survey.SurveyorName);
                            
                foreach (BirdSurveyDetails detail in survey.Details)
                {
                    if (_birdRepo.GetSpeciesByName(detail.SpeciesName) == null) SpeciesErrors.Add(detail.SpeciesName);                    
                }

                
            }
            validations.Add(SPAErrors.Distinct().ToList());
            validations.Add(SurveyorErrors.Distinct().ToList());
            validations.Add(ClimateErrors.Distinct().ToList());
            validations.Add(SpeciesErrors.Distinct().ToList());

            return validations;
        }
        

        public void SaveSurveys(List<BirdSurvey> toSave)
        {
            int newID;
            foreach (BirdSurvey survey in toSave)
            {

                survey.ClimateID = this.GetWeatherIDByDate(survey.SurveyDate);

                SamplePointArea spa = _generalRepo.GetSamplePointAreaByName(survey.SamplePointAreaName);
                survey.SamplePointAreaID = spa.SamplePointAreaID;
                survey.SourceID = spa.SourceID;

                survey.SurveyorID = _birdRepo.GetSurveyorByName(survey.SurveyorName).SurveyorID;

                newID = _birdRepo.CreateSurvey(survey);

                foreach (BirdSurveyDetails detail in survey.Details)
                {
                    detail.SurveyID = newID;
                    detail.SpeciesID = _birdRepo.GetSpeciesByName(detail.SpeciesName).SpeciesID;

                    _birdRepo.CreateSurveyDetail(detail);
                    
                }                
            }                       

        }


        #region private methods

        private byte GetWeatherIDByDate(DateTime surveyDate)
        {
            if (surveyDate.Month > 12 && surveyDate.Month < 2)
            {
                return (byte)WeatherEnum.Cold_DecFeb;
            }
            else {
                return (byte)WeatherEnum.Warm_MarNov;
            }
        }

        #endregion

    }
}
