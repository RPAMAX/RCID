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
        public BirdRepository() {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Bird_Species, BirdSpecies>();
                cfg.CreateMap<Bird_Surveyor, BirdSurveyor>();
                cfg.CreateMap<Bird_Survey, BirdSurvey>();
            });

        }


        public IEnumerable<BirdSurveyor> GetAllSurveyors()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                
                var efList = context.Bird_Surveyor.ToList();

                return Mapper.Map<List<Bird_Surveyor>, List<BirdSurveyor>>(efList);
            }
        }
        public IEnumerable<BirdSpecies> GetAllSpecies() {

            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Bird_Species.ToList();

                return Mapper.Map<List<Bird_Species>, List<BirdSpecies>>(efList);
            }
        }

        public IEnumerable<BirdSurvey> GetAllSurveys()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {

                var efList = context.Bird_Survey.ToList();

                return Mapper.Map<List<Bird_Survey>, List<BirdSurvey>>(efList);
            }
        }
        
    }
}
