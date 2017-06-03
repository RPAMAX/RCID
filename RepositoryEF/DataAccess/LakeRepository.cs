using AutoMapper;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public class LakeRepository : ILakeRepository
    {
        IMapper mapper;

        //TO DO: change sourceid to 5
        readonly byte LIMS_SOURCEID = 1;

        public LakeRepository() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Lake_Parameter, LakeParameter>();                
                cfg.CreateMap<Phyto_Division, PhytoDivision>();
            });

            this.mapper = config.CreateMapper();           
            
        }


        #region Parameters
        
        public IEnumerable<LakeParameter> GetAllParameters()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Lake_Parameter.OrderBy(s => s.ParameterName).ToList();

                return mapper.Map<List<Lake_Parameter>, List<LakeParameter>>(efList);
            }
        }

        public bool UpdateParameter(LakeParameter item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lake_Parameter efItem = context.Lake_Parameter.Where(b => b.ParameterID == item.ParameterID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ParameterActive = item.ParameterActive;
                    efItem.ParameterFullName = item.ParameterFullName;
                    efItem.ParameterName = item.ParameterName;
                    efItem.ParameterTestMethod = item.ParameterTestMethod;
                    efItem.ParameterUnit = item.ParameterUnit;
                                        
                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public int CreateParameter(LakeParameter item)
        {
            byte newid = 0;           
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    newid = context.Lake_Parameter.OrderByDescending(u => u.ParameterID).FirstOrDefault().ParameterID;
                    newid++;

                    Lake_Parameter efItem = new Lake_Parameter()
                    {
                        ParameterActive = item.ParameterActive,
                        ParameterFullName = item.ParameterFullName,
                        ParameterID = newid,
                        ParameterName = item.ParameterName, 
                        ParameterTestMethod = item.ParameterTestMethod,
                        ParameterUnit = item.ParameterUnit
                              
                    };

                    context.Lake_Parameter.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return newid;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return newid;
        }

        public bool InactivateParameter(LakeParameter item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lake_Parameter efItem = context.Lake_Parameter.Where(b => b.ParameterID == item.ParameterID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ParameterActive = false;

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

        #region ProfileDetail

        public IEnumerable<LakeProfileDetail> GetProfileDetails(int profileID)
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = from profile in context.Lake_ProfileDetail
                             join param in context.Lake_Parameter
                             on profile.ParameterID equals param.ParameterID
                             where profile.ProfileID == profileID
                             select new LakeProfileDetail
                             {
                                 DepthFeet = profile.DepthFeet,
                                 ParameterID = profile.ParameterID,
                                 ParameterName = param.ParameterFullName, 
                                 ParameterValue = profile.ParameterValue, 
                                 ProfileDetailNotes = profile.ProfileDetailNotes,
                                 ProfileDetailActive = profile.ProfileDetailActive
                             };

                return efList.ToList();
            }
        }

        public bool UpdateProfileDetails(LakeProfileDetail item) {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lake_ProfileDetail efItem = context.Lake_ProfileDetail.Where(u => u.ProfileID == item.ProfileID
                                                                                    && u.DepthFeet == item.DepthFeet
                                                                                    && u.ParameterID == item.ParameterID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ParameterValue = item.ParameterValue;
                    efItem.ProfileDetailNotes = item.ProfileDetailNotes;
                    efItem.ProfileDetailActive = item.ProfileDetailActive;

                    if (context.SaveChanges() > 0) {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateProfileDetails(LakeProfileDetail item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {                   

                    Lake_ProfileDetail efItem = new Lake_ProfileDetail()
                    {
                         DepthFeet = item.DepthFeet,
                         ParameterID = item.ParameterID,
                         ProfileID = item.ProfileID,
                         ParameterValue = item.ParameterValue,
                         ProfileDetailNotes = item.ProfileDetailNotes,
                         ProfileDetailActive = item.ProfileDetailActive                             
                    };

                    context.Lake_ProfileDetail.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;
        }

        public bool InactivateProfileDetails(LakeProfileDetail item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lake_ProfileDetail efItem = context.Lake_ProfileDetail.Where(u => u.ProfileID == item.ProfileID
                                                                                    && u.DepthFeet == item.DepthFeet
                                                                                    && u.ParameterID == item.ParameterID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ProfileDetailActive = false;

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


        #region Profile

        public IEnumerable<LakeProfile> GetAllProfiles()
        {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = from profile in context.Lake_Profile
                             join sp in context.Lims_SamplePoint
                             on profile.SamplePointID equals sp.SamplePointID
                             select new LakeProfile
                             {
                                  ProfileDate = profile.ProfileDate,
                                  ProfileID = profile.ProfileID,
                                  SamplePointID = profile.SamplePointID,
                                  SamplePointName = sp.SamplePointName,
                                  SourceID = profile.SourceID,
                                  ProfileActive = profile.ProfileActive,
                                  SamplePointRefID = sp.SamplePointRefID
                             };

                return efList.ToList();
            }
        }

        public bool UpdateProfile(LakeProfile item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lake_Profile efItem = context.Lake_Profile.Where(b => b.ProfileID == item.ProfileID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ProfileDate = item.ProfileDate;
                    efItem.SamplePointID = item.SamplePointID;
                    efItem.ProfileActive = item.ProfileActive;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public int CreateProfile(LakeProfile item)
        {
            int newid;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    newid = context.Lake_Profile.OrderByDescending(u => u.ProfileID).FirstOrDefault().ProfileID;
                    newid++;

                    Lake_Profile efItem = new Lake_Profile()
                    {
                        ProfileDate = item.ProfileDate,
                        ProfileID = newid,
                        ProfileActive = item.ProfileActive,
                        SourceID = LIMS_SOURCEID,
                        SamplePointID = item.SamplePointID
                    };

                    context.Lake_Profile.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        return newid;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return newid;
        }


        public bool InactivateProfile(LakeProfile item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lake_Profile efItem = context.Lake_Profile.Where(b => b.ProfileID == item.ProfileID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ProfileActive = false;

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
