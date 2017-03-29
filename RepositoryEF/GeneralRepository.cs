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
        public IEnumerable<SamplePointArea> GetAllSamplePointAreas()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {

                var efList = context.Lims_SamplePointArea.ToList();

                return Mapper.Map<List<Lims_SamplePointArea>, List<SamplePointArea>>(efList);
            }
        }


    }
}
