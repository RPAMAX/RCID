using AutoMapper;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public class PhytoRepository : IPhytoRepository
    {
        IMapper mapper;

        readonly byte LIMS_SOURCEID = 5;

        public PhytoRepository() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Phyto_Species, PhytoSpecies>();                
                cfg.CreateMap<Phyto_Division, PhytoDivision>();
            });

            this.mapper = config.CreateMapper();           
            
        }


        #region Surveys
        
        public IEnumerable<PhytoSurvey> GetAllSurveys()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {              
                var efList = from survey in context.Phyto_Survey                             
                             join spa in context.Lims_SamplePointArea
                             on survey.SamplePointAreaID equals spa.SamplePointAreaID                             
                             select new PhytoSurvey
                             {
                                 SurveyID = survey.SurveyID,                                 
                                 SurveyDate = survey.SurveyDate,
                                 SamplePointAreaID = spa.SamplePointAreaID,
                                 SamplePointAreaName = spa.SamplePointAreaName,
                                 SourceID = spa.SourceID,                                 
                                 SurveyActive = survey.SurveyActive,
                                 LocationDetails = survey.LocationDetails
                                 
                             };

                return efList.ToList();                
            }
        }

        public bool UpdateSurvey(PhytoSurvey item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Phyto_Survey efItem = context.Phyto_Survey.Where(b => b.SurveyID == item.SurveyID).FirstOrDefault();

                    if (efItem == null) return result;
                                        
                    efItem.SamplePointAreaID = item.SamplePointAreaID;
                    efItem.SourceID = LIMS_SOURCEID;
                    efItem.SurveyDate = item.SurveyDate;
                    efItem.LocationDetails = item.LocationDetails;
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

        public int CreateSurvey(PhytoSurvey item)
        {
            int newid = 0;           
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    newid = context.Phyto_Survey.OrderByDescending(u => u.SurveyID).FirstOrDefault().SurveyID;
                    newid++;

                    Phyto_Survey efItem = new Phyto_Survey()
                    {
                        SurveyID = newid,
                        SamplePointAreaID = item.SamplePointAreaID,
                        SourceID = LIMS_SOURCEID,
                        LocationDetails = item.LocationDetails,
                        SurveyDate = item.SurveyDate,
                        SurveyActive = item.SurveyActive
                    };

                    context.Phyto_Survey.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return newid;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return newid;
        }

        public bool InactivateSurvey(PhytoSurvey item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Phyto_Survey efItem = context.Phyto_Survey.Where(b => b.SurveyID == item.SurveyID).FirstOrDefault();

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

        public IEnumerable<PhytoSpecies> GetAllSpecies()
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = from species in context.Phyto_Species
                             join div in context.Phyto_Division
                             on species.DivisionID equals div.DivisionID
                             select new PhytoSpecies
                             {
                                 DivisionID = species.DivisionID,                                 
                                 DivisionName = div.DivisionName,
                                 SpeciesActive = species.SpeciesActive,
                                 SpeciesID = species.SpeciesID,
                                 SpeciesName = species.SpeciesName

                             };

                return efList.ToList();
            }
        }

        public bool UpdateSpecies(PhytoSpecies item) {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Phyto_Species efItem = context.Phyto_Species.Where(b => b.SpeciesID == item.SpeciesID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SpeciesName = item.SpeciesName;
                    efItem.SpeciesActive = item.SpeciesActive;
                    efItem.DivisionID = item.DivisionID;

                    if (context.SaveChanges() > 0) {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateSpecies(PhytoSpecies item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    short newid = context.Phyto_Species.OrderByDescending(u => u.SpeciesID).FirstOrDefault().SpeciesID;
                    newid++;

                    Phyto_Species efItem = new Phyto_Species()
                    {
                        SpeciesID = newid,
                        SpeciesActive = true,
                        SpeciesName = item.SpeciesName,
                        DivisionID = item.DivisionID
                    };

                    context.Phyto_Species.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;
        }

        public bool InactivateSpecies(PhytoSpecies item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Phyto_Species efItem = context.Phyto_Species.Where(b => b.SpeciesID == item.SpeciesID).FirstOrDefault();

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
                
        #endregion
              

        #region Division

        public IEnumerable<PhytoDivision> GetAllDivisions()
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Phyto_Division.OrderBy(s => s.DivisionName).ToList();

                return mapper.Map<List<Phyto_Division>, List<PhytoDivision>>(efList);
            }
        }

        public bool UpdateDivision(PhytoDivision item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Phyto_Division efItem = context.Phyto_Division.Where(b => b.DivisionID == item.DivisionID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.DivisionName = item.DivisionName;
                    efItem.DivisionCommonName = item.DivisionCommonName;
                    efItem.DivisionActive = item.DivisionActive;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public int CreateDivision(PhytoDivision item)
        {
            byte newid;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    newid = context.Phyto_Division.OrderByDescending(u => u.DivisionID).FirstOrDefault().DivisionID;
                    newid++;

                    Phyto_Division efItem = new Phyto_Division()
                    {
                        DivisionID = newid,
                        DivisionActive = item.DivisionActive,
                        DivisionName = item.DivisionName,
                        DivisionCommonName = item.DivisionCommonName
                    };

                    context.Phyto_Division.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return newid;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return newid;
        }

       
        public bool InactivateDivision(PhytoDivision item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Phyto_Division efItem = context.Phyto_Division.Where(b => b.DivisionID == item.DivisionID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.DivisionActive = false;

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
        public IEnumerable<PhytoSurveyDetails> GetSurveyDetails( int surveyID)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = from surveyDetails in context.Phyto_SurveyDetail
                             join species in context.Phyto_Species
                             on surveyDetails.SpeciesID equals species.SpeciesID
                             where surveyDetails.SurveyID == surveyID                                   
                             select new PhytoSurveyDetails
                             {
                                  SpeciesID = surveyDetails.SpeciesID,
                                  SpeciesName = species.SpeciesName,
                                  SurveyCount = surveyDetails.SurveyCount,
                                  SurveyDetailActive = surveyDetails.SurveyDetailActive,
                                  SurveyID = surveyDetails.SurveyID
                             };

                return efList.ToList();

            }
        }

        public bool CreateSurveyDetail(PhytoSurveyDetails item)
        {   
            try
            {
               using (RCID_DWHEntities context = new RCID_DWHEntities())
               {
                    var efitem = context.Phyto_SurveyDetail.Where(s => s.SurveyID == item.SurveyID && s.SpeciesID == item.SpeciesID).FirstOrDefault();
                    //item already exists
                    if (efitem != null) return false;

                    Phyto_SurveyDetail efItem = new Phyto_SurveyDetail()
                    {
                         SpeciesID = item.SpeciesID,
                         SurveyCount  = item.SurveyCount, 
                         SurveyID = item.SurveyID, 
                         SurveyDetailActive = item.SurveyDetailActive

                    };
                    
                    context.Phyto_SurveyDetail.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
           catch (Exception e) { throw e; }
           return false;

        }
        public bool UpdateSurveyDetail(PhytoSurveyDetails item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Phyto_SurveyDetail efItem = context.Phyto_SurveyDetail.Where(b => b.SurveyID == item.SurveyID 
                                                                                && b.SpeciesID == item.SpeciesID
                                                                                ).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SpeciesID = item.SpeciesID;
                    efItem.SurveyCount = item.SurveyCount;
                    efItem.SurveyDetailActive = item.SurveyDetailActive;
                                       
                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool InactivateSurveyDetail(PhytoSurveyDetails item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Phyto_SurveyDetail efItem = context.Phyto_SurveyDetail.Where(b => b.SurveyID == item.SurveyID
                                                                                && b.SpeciesID == item.SpeciesID
                                                                                ).FirstOrDefault();

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
