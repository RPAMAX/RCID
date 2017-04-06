using AutoMapper;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public class BirdRepository : IBirdRepository
    {
        IMapper mapper; 

        public BirdRepository() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Bird_Species, BirdSpecies>();
                cfg.CreateMap<Bird_Surveyor, BirdSurveyor>();
                cfg.CreateMap<Bird_Survey, BirdSurvey>();
            });

            this.mapper = config.CreateMapper();           

        }


        #region Surveys
        
        public IEnumerable<BirdSurvey> GetAllSurveys()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {              
                var efList = from survey in context.Bird_Survey
                             join climate in context.Weather_Climate
                             on survey.ClimateID equals climate.ClimateID
                             join spa in context.Lims_SamplePointArea
                             on survey.SamplePointAreaID equals spa.SamplePointAreaID
                             join surveyor in context.Bird_Surveyor
                             on survey.SurveyorID equals surveyor.SurveyorID
                             select new BirdSurvey
                             {
                                 ClimateID = survey.ClimateID,
                                 ClimateName = climate.ClimateName,
                                 SamplePointAreaID = survey.SamplePointAreaID,
                                 SamplePointAreaName = spa.SamplePointAreaName,
                                 SourceID = survey.SourceID,
                                 SurveyDate = survey.SurveyDate,
                                 SurveyID = survey.SurveyID,
                                 SurveyorID = survey.SurveyorID,
                                 SurveyorName = surveyor.SurveyorName
                             };

                return efList.ToList();                
            }
        }

        public bool UpdateSurvey(BirdSurvey item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Survey efItem = context.Bird_Survey.Where(b => b.SurveyID == item.SurveyID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ClimateID = item.ClimateID;
                    efItem.SamplePointAreaID = item.SamplePointAreaID;
                    efItem.SourceID = item.SourceID;
                    efItem.SurveyDate = item.SurveyDate;
                    efItem.SurveyorID = item.SurveyorID;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateSurvey(BirdSurvey item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Survey efItem = new Bird_Survey()
                    {
                        ClimateID = item.ClimateID,
                        SamplePointAreaID = item.SamplePointAreaID,
                        SourceID = item.SourceID,
                        SurveyDate = item.SurveyDate,
                        SurveyorID = item.SurveyorID
                    };

                    context.Bird_Survey.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        #endregion
        #region Species

        public IEnumerable<BirdSpecies> GetAllSpecies()
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Bird_Species.ToList();

                return mapper.Map<List<Bird_Species>, List<BirdSpecies>>(efList);
            }
        }

        public bool UpdateSpecies(BirdSpecies item) {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Species efItem = context.Bird_Species.Where(b => b.SpeciesID == item.SpeciesID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SpeciesName = item.SpeciesName;
                    if (context.SaveChanges() > 0) {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateSpecies(BirdSpecies item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Species efItem = new Bird_Species()
                    {
                        SpeciesActive = true,
                        SpeciesName = item.SpeciesName
                    };

                    context.Bird_Species.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        #endregion
        #region Surveyors
        public IEnumerable<BirdSurveyor> GetAllSurveyors()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {

                var efList = context.Bird_Surveyor.ToList();

                return mapper.Map<List<Bird_Surveyor>, List<BirdSurveyor>>(efList);
            }
        }

        public bool UpdateSurveyor(BirdSurveyor item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Surveyor efItem = context.Bird_Surveyor.Where(b => b.SurveyorID == item.SurveyorID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SurveyorName = item.SurveyorName;
                    efItem.SurveyorActive = item.SurveyorActive;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }       

        public bool CreateSurveyor(BirdSurveyor item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Surveyor efItem = new Bird_Surveyor()
                    {
                        SurveyorActive = true,
                        SurveyorName = item.SurveyorName                        
                    };

                    context.Bird_Surveyor.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        #endregion
    }
}
