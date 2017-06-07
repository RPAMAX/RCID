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

        readonly byte LIMS_SOURCEID = 5;

        public BirdRepository() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Bird_Species, BirdSpecies>();
                cfg.CreateMap<Bird_Surveyor, BirdSurveyor>();
                cfg.CreateMap<Bird_Survey, BirdSurvey>();
                cfg.CreateMap<Bird_SurveyDetail, BirdSurveyDetails>();
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
                                 SurveyorName = surveyor.SurveyorName,
                                 SurveyActive = survey.SurveyActive 
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
                    efItem.SourceID = LIMS_SOURCEID;
                    efItem.SurveyDate = item.SurveyDate;
                    efItem.SurveyorID = item.SurveyorID;
                    efItem.SurveyActive = item.SurveyActive;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { }
            return result;
        }

        public int CreateSurvey(BirdSurvey item)
        {
            int newid = 0;           

            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    newid = context.Bird_Survey.OrderByDescending(u => u.SurveyID).FirstOrDefault().SurveyID;
                    newid++;

                    Bird_Survey efItem = new Bird_Survey()
                    {
                        SurveyID = newid,
                        ClimateID = item.ClimateID,
                        SamplePointAreaID = item.SamplePointAreaID,
                        SourceID = item.SourceID ==0? LIMS_SOURCEID:item.SourceID,
                        SurveyDate = item.SurveyDate,
                        SurveyorID = item.SurveyorID
                    };

                    context.Bird_Survey.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return newid;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return newid;
        }

        public bool InactivateSurvey(BirdSurvey item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Survey efItem = context.Bird_Survey.Where(b => b.SurveyID == item.SurveyID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SurveyActive = false;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public IEnumerable<BirdSurveyDetails> GetSurveyDetails(int surveyID)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = from surveyDetail in context.Bird_SurveyDetail
                             join species in context.Bird_Species
                             on surveyDetail.SpeciesID equals species.SpeciesID
                             where surveyDetail.SurveyID == surveyID
                             select new BirdSurveyDetails
                             {
                                  SpeciesID = surveyDetail.SpeciesID,
                                  SurveyDetailCount = surveyDetail.SurveyDetailCount,
                                  SpeciesName = species.SpeciesName,
                                  SurveyDetailActive = surveyDetail.SurveyDetailActive
                             };

                return efList.ToList();

            }
        }

        public bool CreateSurveyDetail(BirdSurveyDetails item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {                  

                    Bird_SurveyDetail efItem = new Bird_SurveyDetail()
                    {
                        SurveyID = item.SurveyID,
                        SpeciesID = item.SpeciesID,
                        SurveyDetailCount = item.SurveyDetailCount                        
                    };

                    context.Bird_SurveyDetail.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;

        }

        public bool InactivateSurveyDetail(BirdSurveyDetails item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_SurveyDetail efItem = context.Bird_SurveyDetail.Where(b => b.SurveyID == item.SurveyID && b.SpeciesID == item.SpeciesID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SurveyDetailActive = false;

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
                var efList = context.Bird_Species.OrderBy(s=>s.SpeciesName).ToList();

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
                    efItem.SpeciesActive = item.SpeciesActive;
                    
                    if (context.SaveChanges() > 0) {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool InactivateSpecies(BirdSpecies item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Species efItem = context.Bird_Species.Where(b => b.SpeciesID == item.SpeciesID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SpeciesActive = false;

                    if (context.SaveChanges() > 0)
                    {
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
                    short newid = context.Bird_Species.OrderByDescending(u => u.SpeciesID).FirstOrDefault().SpeciesID;
                    newid++;

                    Bird_Species efItem = new Bird_Species()
                    {
                        SpeciesID = newid,
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
            catch (Exception e) { throw e; }
            return result;
        }

        public BirdSpecies GetSpeciesByName(string name)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efitem = context.Bird_Species.Where(s => s.SpeciesName.ToUpper().Equals(name.ToUpper())).FirstOrDefault();
                return mapper.Map<Bird_Species, BirdSpecies>(efitem);
            }
        }
        #endregion

        #region Surveyors
        public IEnumerable<BirdSurveyor> GetAllSurveyors()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {

                var efList = context.Bird_Surveyor.OrderBy(s=>s.SurveyorName).ToList();

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
                    byte newid = context.Bird_Surveyor.OrderByDescending(u => u.SurveyorID).FirstOrDefault().SurveyorID;
                    newid++;

                    Bird_Surveyor efItem = new Bird_Surveyor()
                    {
                        SurveyorID = newid,
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

        public bool InactivateSurveyor(BirdSurveyor item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Bird_Surveyor efItem = context.Bird_Surveyor.Where(b => b.SurveyorID == item.SurveyorID).FirstOrDefault();

                    if (efItem == null) return result;
                                        
                    efItem.SurveyorActive = false;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public BirdSurveyor GetSurveyorByName(string name)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efitem = context.Bird_Surveyor.Where(s => s.SurveyorName.ToUpper().Equals(name.ToUpper())).FirstOrDefault();
                return mapper.Map<Bird_Surveyor, BirdSurveyor>(efitem);
            }
        }


        #endregion
    }
}
