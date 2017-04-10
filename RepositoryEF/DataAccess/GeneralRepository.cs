using AutoMapper;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDRepository
{
    public class GeneralRepository : IGeneralRepository
    {
        public GeneralRepository() {

            Mapper.Initialize(cfg => {
                cfg.CreateMap<Weather_Climate, WeatherClimate>();
                cfg.CreateMap<Lims_SamplePointArea, SamplePointArea>();
            });

        }
        
        public IEnumerable<WeatherClimate> GetAllClimates()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {                
                var efList = context.Weather_Climate.ToList();

                return Mapper.Map<List<Weather_Climate>, List<WeatherClimate>>(efList);
            }
        }

        public WeatherClimate GetClimateByName(string name)
        {            
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efitem = context.Weather_Climate.Where(c => c.ClimateName.ToUpper().Equals(name.ToUpper())).FirstOrDefault();
                return Mapper.Map<Weather_Climate, WeatherClimate>(efitem);
            }
        }

        public IEnumerable<SamplePointArea> GetAllSamplePointAreas()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Lims_SamplePointArea.ToList();

                return Mapper.Map<List<Lims_SamplePointArea>, List<SamplePointArea>>(efList);
            }
        }

        public SamplePointArea GetSamplePointAreaByName(string name)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efitem = context.Lims_SamplePointArea.Where(s => s.SamplePointAreaName.ToUpper().Equals(name.ToUpper())).FirstOrDefault();
                return Mapper.Map<Lims_SamplePointArea, SamplePointArea>(efitem);
            }
        }


    }
}
