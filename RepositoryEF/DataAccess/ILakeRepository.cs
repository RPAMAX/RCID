using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public interface ILakeRepository
    {
        
        IEnumerable<LakeParameter> GetAllParameters();
        IEnumerable<LakeProfileDetail> GetAllProfileDetails();
        IEnumerable<LakeProfile> GetAllProfiles();


        bool UpdateParameter(LakeParameter item);
        int CreateParameter(LakeParameter item);
        bool InactivateParameter(LakeParameter item);

        bool UpdateProfile(LakeProfile item);
        int CreateProfile(LakeProfile item);
        //bool InactivateProfile(LakeProfile item);

        bool UpdateProfileDetails(LakeProfileDetail item);
        bool CreateProfileDetails(LakeProfileDetail item);
        //bool InactivateProfileDetails(LakeProfileDetail item);

       

    }
}
