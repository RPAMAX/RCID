using RCIDRepository;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDService
{
    public class LakeService : ILakeService
    {
        ILakeRepository _LakeRepo;        

        public LakeService(ILakeRepository repo)
        {
            _LakeRepo = repo;            
        }


        public IEnumerable<LakeParameter> GetAllParameters()
        {
            return _LakeRepo.GetAllParameters();
        }
        public IEnumerable<LakeProfileDetail> GetProfileDetails(int id)
        {
            return _LakeRepo.GetProfileDetails(id);
        }
        public IEnumerable<LakeProfile> GetAllProfiles()
        {
            return _LakeRepo.GetAllProfiles();
        }


        public bool UpdateParameter(LakeParameter item)
        {
            return _LakeRepo.UpdateParameter(item);
        }

        public int CreateParameter(LakeParameter item)
        {
            return _LakeRepo.CreateParameter(item);
        }

        public bool InactivateParameter(LakeParameter item)
        {
            return _LakeRepo.InactivateParameter(item);
        }

        public bool UpdateProfile(LakeProfile item)
        {
            return _LakeRepo.UpdateProfile(item);
        }

        public int CreateProfile(LakeProfile item)
        {
            return _LakeRepo.CreateProfile(item);
        }

        public bool InactivateProfile(LakeProfile item)
        {
            return _LakeRepo.InactivateProfile(item);
        }

        public bool UpdateProfileDetails(LakeProfileDetail item)
        {
            return _LakeRepo.UpdateProfileDetails(item);
        }

        public bool CreateProfileDetails(LakeProfileDetail item)
        {
            return _LakeRepo.CreateProfileDetails(item);
        }

        public bool InactivateProfileDetails(LakeProfileDetail item)
        {
            return _LakeRepo.InactivateProfileDetails(item);
        }


    }
}
