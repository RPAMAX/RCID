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

        public FishRepository() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Fish_Species, FishSpecies>();               
                
            });

            this.mapper = config.CreateMapper();           
            
        }


        #region Surveys
        
   /*     public IEnumerable<FishSurvey> GetAllSurveys()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {              
                var efList = from survey in context.Fish_Survey                             
                             join spa in context.Lims_SamplePointArea
                             on survey.SamplePointAreaID equals spa.SamplePointAreaID
                            
                             select new FishSurvey
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
                        SourceID = item.SourceID,
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
        */
        #endregion

        #region Species

        public IEnumerable<FishSpecies> GetAllSpecies()
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Fish_Species.OrderBy(s=>s.SpeciesName).ToList();

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
        #endregion

    }
}
