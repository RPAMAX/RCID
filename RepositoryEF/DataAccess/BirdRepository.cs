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


        public IEnumerable<BirdSurveyor> GetAllSurveyors()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                
                var efList = context.Bird_Surveyor.ToList();

                return mapper.Map<List<Bird_Surveyor>, List<BirdSurveyor>>(efList);
            }
        }
        public IEnumerable<BirdSpecies> GetAllSpecies() {
            
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Bird_Species.ToList();

                return mapper.Map<List<Bird_Species>, List<BirdSpecies>>(efList);
            }
        }

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
                                 SurveyourName = surveyor.SurveyorName
                             };

                return efList.ToList();                
            }
        }
        
    }
}
