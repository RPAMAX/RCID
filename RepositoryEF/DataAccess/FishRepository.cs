using AutoMapper;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public class FishRepository : IFishRepository
    {
        IMapper mapper;

        readonly byte LIMS_SOURCEID = 5;

        public FishRepository() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Fish_Species, FishSpecies>()
                    .ForMember(dest=> dest.SpeciesGroupName, 
                               opts=> opts.MapFrom(src=>src.SpeciesGroup.SpeciesGroupName) );
                cfg.CreateMap<Fish_SpeciesGroup, FishSpeciesGroup>();
                cfg.CreateMap<Fish_Generator, FishGenerator>();
                cfg.CreateMap<Fish_Survey, FishSurvey>()
                   .ForMember(dest => dest.SamplePointAreaName,
                              opts => opts.MapFrom(src => src.SamplePointArea.SamplePointAreaName));
            });

            this.mapper = config.CreateMapper();           
            
        }


        #region Surveys
        
        public IEnumerable<FishSurvey> GetAllSurveys()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Fish_Survey.Include("SamplePointArea").OrderBy(s => s.SurveyYear).ToList();

                return mapper.Map<List<Fish_Survey>, List<FishSurvey>>(efList);
               
            }
        }

        public bool UpdateSurvey(FishSurvey item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_Survey efItem = context.Fish_Survey.Where(b => b.SurveyID == item.SurveyID).FirstOrDefault();

                    if (efItem == null) return result;
                                        
                    efItem.SamplePointAreaID = item.SamplePointAreaID;
                    efItem.SourceID = LIMS_SOURCEID;
                    efItem.SurveyYear = item.SurveyYear;
                    efItem.SurveyComments = item.SurveyComments;
                    efItem.SurveyActive = item.SurveyActive;
                                        
                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public int CreateSurvey(FishSurvey item)
        {
            int newid = 0;           
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    newid = context.Fish_Survey.OrderByDescending(u => u.SurveyID).FirstOrDefault().SurveyID;
                    newid++;

                    Fish_Survey efItem = new Fish_Survey()
                    {
                        SurveyID = newid,
                        SamplePointAreaID = item.SamplePointAreaID,
                        SourceID = LIMS_SOURCEID,
                        SurveyComments = item.SurveyComments,
                        SurveyYear = item.SurveyYear
                    };

                    context.Fish_Survey.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return newid;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return newid;
        }

        public bool InactivateSurvey(FishSurvey item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_Survey efItem = context.Fish_Survey.Where(b => b.SurveyID == item.SurveyID).FirstOrDefault();

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
        
        #endregion

        #region Species

        public IEnumerable<FishSpecies> GetAllSpecies()
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Fish_Species.Include("SpeciesGroup").OrderBy(s=>s.SpeciesName).ToList();

                return mapper.Map<List<Fish_Species>, List<FishSpecies>>(efList);
            }
        }

        public bool UpdateSpecies(FishSpecies item) {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_Species efItem = context.Fish_Species.Where(b => b.SpeciesID == item.SpeciesID).FirstOrDefault();

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

        public bool CreateSpecies(FishSpecies item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    short newid = context.Fish_Species.OrderByDescending(u => u.SpeciesID).FirstOrDefault().SpeciesID;
                    newid++;

                    Fish_Species efItem = new Fish_Species()
                    {
                        SpeciesID = newid,
                        SpeciesActive = true,
                        SpeciesName = item.SpeciesName
                    };

                    context.Fish_Species.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;
        }

        public bool InactivateSpecies(FishSpecies item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_Species efItem = context.Fish_Species.Where(b => b.SpeciesID == item.SpeciesID).FirstOrDefault();

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

        public FishSpecies GetSpeciesByName(string name)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efitem = context.Fish_Species.Where(s => s.SpeciesName.ToUpper().Equals(name.ToUpper())).FirstOrDefault();
                return mapper.Map<Fish_Species, FishSpecies>(efitem);
            }
        }
        #endregion

        #region Species Group

        public IEnumerable<FishSpeciesGroup> GetAllSpeciesGroup()
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Fish_SpeciesGroup.OrderBy(s => s.SpeciesGroupName).ToList();

                return mapper.Map<List<Fish_SpeciesGroup>, List<FishSpeciesGroup>>(efList);
            }
        }

        public bool UpdateSpeciesGroup(FishSpeciesGroup item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_SpeciesGroup efItem = context.Fish_SpeciesGroup.Where(b => b.SpeciesGroupID == item.SpeciesGroupID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SpeciesGroupName = item.SpeciesGroupName;
                    efItem.SpeciesGroupActive = item.SpeciesGroupActive;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateSpeciesGroup(FishSpeciesGroup item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    byte newid = context.Fish_SpeciesGroup.OrderByDescending(u => u.SpeciesGroupID).FirstOrDefault().SpeciesGroupID;
                    newid++;

                    Fish_SpeciesGroup efItem = new Fish_SpeciesGroup()
                    {
                        SpeciesGroupID = newid,
                        SpeciesGroupActive = true,
                        SpeciesGroupName = item.SpeciesGroupName
                    };

                    context.Fish_SpeciesGroup.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;
        }

        public FishSpeciesGroup GetSpeciesGroupByName(string name)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efitem = context.Fish_SpeciesGroup.Where(s => s.SpeciesGroupName.ToUpper().Equals(name.ToUpper())).FirstOrDefault();
                return mapper.Map<Fish_SpeciesGroup, FishSpeciesGroup>(efitem);
            }
        }
        public bool InactivateSpeciesGroup(FishSpeciesGroup item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_SpeciesGroup efItem = context.Fish_SpeciesGroup.Where(b => b.SpeciesGroupID == item.SpeciesGroupID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SpeciesGroupActive = false;

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

        #region Generator

        public IEnumerable<FishGenerator> GetAllGenerators()
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Fish_Generator.OrderBy(s => s.GeneratorName).ToList();

                return mapper.Map<List<Fish_Generator>, List<FishGenerator>>(efList);
            }
        }

        public bool UpdateGenerator(FishGenerator item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_Generator efItem = context.Fish_Generator.Where(b => b.GeneratorID == item.GeneratorID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.GeneratorName = item.GeneratorName;
                    efItem.GeneratorActive = item.GeneratorActive;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateGenerator(FishGenerator item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    byte newid = context.Fish_Generator.OrderByDescending(u => u.GeneratorID).FirstOrDefault().GeneratorID;
                    newid++;

                    Fish_Generator efItem = new Fish_Generator()
                    {
                        GeneratorID = newid,
                        GeneratorActive = true,
                        GeneratorName = item.GeneratorName
                    };

                    context.Fish_Generator.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;
        }

        public FishGenerator GetGeneratorByName(string name)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efitem = context.Fish_Generator.Where(s => s.GeneratorName.ToUpper().Equals(name.ToUpper())).FirstOrDefault();
                return mapper.Map<Fish_Generator, FishGenerator>(efitem);
            }
        }
        public bool InactivateGenerator(FishGenerator item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_Generator efItem = context.Fish_Generator.Where(b => b.GeneratorID == item.GeneratorID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.GeneratorActive = false;

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

        #region Survey Locations
        public IEnumerable<FishSurveyLocation> GetSurveyLocations(int surveyID)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = from surveyLocation in context.Fish_SurveyLocation
                             join generator in context.Fish_Generator
                             on surveyLocation.GeneratorID equals generator.GeneratorID
                             where surveyLocation.SurveyID == surveyID
                             select new FishSurveyLocation
                             {
                                GeneratorID = surveyLocation.GeneratorID,
                                GeneratorName = generator.GeneratorName,
                                LocationDetails = surveyLocation.LocationDetails,
                                SurveyDate = surveyLocation.SurveyDate, 
                                SurveyDurationSeconds = surveyLocation.SurveyDurationSeconds,
                                SurveyID = surveyLocation.SurveyID,
                                SurveyLocationComments = surveyLocation.SurveyLocationComments,
                                SurveyNumber = surveyLocation.SurveyNumber ,
                                SurveyLocationActive = surveyLocation.SurveyLocationActive
                             };

                return efList.ToList();

            }
        }

        public int CreateSurveyLocation(FishSurveyLocation item)
        {
            byte newid = 0;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    newid = context.Fish_SurveyLocation.Where(s=>s.SurveyID == item.SurveyID).OrderByDescending(u => u.SurveyNumber).FirstOrDefault().SurveyNumber;
                    newid++;

                    Fish_SurveyLocation efItem = new Fish_SurveyLocation()
                    {
                         GeneratorID = item.GeneratorID,
                         LocationDetails = item.LocationDetails,
                         SurveyDate = item.SurveyDate,
                         SurveyDurationSeconds = item.SurveyDurationSeconds,
                         SurveyLocationComments = item.SurveyLocationComments,
                         SurveyNumber = newid,
                         SurveyID = item.SurveyID                        
                    };

                    context.Fish_SurveyLocation.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return newid;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return newid;
        }

        public bool UpdateSurveyLocation(FishSurveyLocation item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_SurveyLocation efItem = context.Fish_SurveyLocation.Where(b => b.SurveyID == item.SurveyID && b.SurveyNumber == item.SurveyNumber).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.GeneratorID = item.GeneratorID;
                    efItem.LocationDetails = item.LocationDetails;
                    efItem.SurveyDate = item.SurveyDate;
                    efItem.SurveyDurationSeconds = item.SurveyDurationSeconds;
                    efItem.SurveyLocationComments = item.SurveyLocationComments;
                    efItem.SurveyLocationActive = item.SurveyLocationActive;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool InactivateSurveyLocation(FishSurveyLocation item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_SurveyLocation efItem = context.Fish_SurveyLocation.Where(b => b.SurveyID == item.SurveyID && b.SurveyNumber == item.SurveyNumber).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SurveyLocationActive = false;

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

        #region Survey Details
        public IEnumerable<FishSurveyDetails> GetSurveyDetails(int locationID, int surveyID)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = from surveyDetails in context.Fish_SurveyDetail
                             join species in context.Fish_Species
                             on surveyDetails.SpeciesID equals species.SpeciesID
                             where surveyDetails.SurveyID == surveyID && 
                                   surveyDetails.SurveyNumber == locationID
                             select new FishSurveyDetails
                             {
                                 SpeciesID = species.SpeciesID,
                                 SpeciesName = species.SpeciesName,
                                 SpeciesSizeInches = surveyDetails.SpeciesSizeInches,
                                 SpeciesSizeInchGroup = surveyDetails.SpeciesSizeInchGroup,
                                 SpeciesSizeMillimeters = surveyDetails.SpeciesSizeMillimeters,
                                 SpeciesWeightLbs = surveyDetails.SpeciesWeightLbs,
                                 SpeciesWeightOunces = surveyDetails.SpeciesWeightOunces,
                                 SpeciesWeightPounds = surveyDetails.SpeciesWeightPounds,
                                 SurveyNumber = surveyDetails.SurveyNumber,
                                 SurveyDetailID = surveyDetails.SurveyDetailID,
                                 SurveyID = surveyDetails.SurveyID,
                                 SurveyDetailActive = surveyDetails.SurveyDetailActive
                             };

                return efList.ToList();

            }
        }

        public short CreateSurveyDetail(FishSurveyDetails item)
        {
         
            short newid = 0;
            try
            {
               using (RCID_DWHEntities context = new RCID_DWHEntities())
               {
                    newid = context.Fish_SurveyDetail.Where(s => s.SurveyID == item.SurveyID && s.SurveyNumber == item.SurveyNumber)
                                                    .OrderByDescending(u => u.SurveyDetailID).FirstOrDefault().SurveyDetailID;
                    newid++;

                    Fish_SurveyDetail efItem = new Fish_SurveyDetail()
                    {
                        SurveyDetailID = newid,
                        SurveyID = item.SurveyID,
                        SurveyNumber = item.SurveyNumber,                       
                        SpeciesSizeMillimeters = item.SpeciesSizeMillimeters,                        
                        SpeciesWeightOunces = item.SpeciesWeightOunces,
                        SpeciesWeightPounds = item.SpeciesWeightPounds,                  
                        SpeciesID = item.SpeciesID
                       
                    };

                    // These other values are computed by the DB: 
                    // SpeciesWeightLbs, SpeciesSizeInches,SpeciesSizeInchGroup 

                    context.Fish_SurveyDetail.Add(efItem);

                   if (context.SaveChanges() > 0)
                   {
                       return newid;
                   }
               }
           }
           catch (Exception e) { throw e; }
           return newid;

        }
        public bool UpdateSurveyDetail(FishSurveyDetails item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_SurveyDetail efItem = context.Fish_SurveyDetail.Where(b => b.SurveyID == item.SurveyID 
                                                                                && b.SurveyNumber == item.SurveyNumber
                                                                                && b.SurveyDetailID == item.SurveyDetailID).FirstOrDefault();

                    if (efItem == null) return result;
                  
                    efItem.SpeciesSizeMillimeters = item.SpeciesSizeMillimeters;                   
                    efItem.SpeciesWeightOunces = item.SpeciesWeightPounds;
                    efItem.SpeciesWeightPounds = item.SpeciesWeightPounds;
                    efItem.SpeciesID = item.SpeciesID;
                    efItem.SurveyDetailActive = item.SurveyDetailActive;

                    // These other values are computed by the DB: 
                    // SpeciesWeightLbs, SpeciesSizeInches,SpeciesSizeInchGroup 
                    
                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool InactivateSurveyDetail(FishSurveyDetails item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Fish_SurveyDetail efItem = context.Fish_SurveyDetail.Where(b => b.SurveyID == item.SurveyID
                                                                                && b.SurveyNumber == item.SurveyNumber
                                                                                && b.SurveyDetailID == item.SurveyDetailID).FirstOrDefault();

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
    }
}
