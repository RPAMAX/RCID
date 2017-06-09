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
        readonly byte LIMS_SOURCEID = 5;

        public GeneralRepository() {

            Mapper.Initialize(cfg => {
                cfg.CreateMap<Weather_Climate, WeatherClimate>();
                cfg.CreateMap<Lims_SamplePointArea, SamplePointArea>();
                cfg.CreateMap<Lims_SamplePoint, SamplePoint>();
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

        public IEnumerable<SamplePoint> GetAllSamplePoints()
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {
                var efList = context.Lims_SamplePoint.ToList();

                return Mapper.Map<List<Lims_SamplePoint>, List<SamplePoint>>(efList);
            }
        }

        public SamplePointArea GetSamplePointAreaByName(string name)
        {
            using (RCID_DWHEntities context = new RCID_DWHEntities())
            {                
                var efitem = context.Lims_SamplePointArea.Where(s => s.SamplePointAreaName.ToUpper().Equals(name.ToUpper())).Where(s=>s.SourceID == LIMS_SOURCEID).FirstOrDefault();
                return Mapper.Map<Lims_SamplePointArea, SamplePointArea>(efitem);
            }
        }

        #region Climate
        public bool UpdateClimate(WeatherClimate item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Weather_Climate efItem = context.Weather_Climate.Where(b => b.ClimateID == item.ClimateID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ClimateName = item.ClimateName;
                    efItem.ClimateActive = item.ClimateActive;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateClimate(WeatherClimate item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    byte newid = context.Weather_Climate.OrderByDescending(u => u.ClimateID).FirstOrDefault().ClimateID;
                    newid++;

                    Weather_Climate efItem = new Weather_Climate()
                    {
                         ClimateID = newid,
                        ClimateActive = true,
                        ClimateName = item.ClimateName
                    };

                    context.Weather_Climate.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;
        }
               
        public bool InactivateClimate(WeatherClimate item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Weather_Climate efItem = context.Weather_Climate.Where(b => b.ClimateID == item.ClimateID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.ClimateActive = false;

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

        #region Sample point area
        public bool UpdateSamplePointArea(SamplePointArea item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lims_SamplePointArea efItem = context.Lims_SamplePointArea.Where(b => b.SamplePointAreaID == item.SamplePointAreaID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SamplePointAreaName = item.SamplePointAreaName;
                    efItem.SamplePointAreaActive = item.SamplePointAreaActive;
                    efItem.SourceID = item.SourceID;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateSamplePointArea(SamplePointArea item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    short newid = context.Lims_SamplePointArea.OrderByDescending(u => u.SamplePointAreaID).FirstOrDefault().SamplePointAreaID;
                    newid++;

                    Lims_SamplePointArea efItem = new Lims_SamplePointArea()
                    {
                        SamplePointAreaID = newid,
                        SamplePointAreaActive = true,
                        SamplePointAreaName = item.SamplePointAreaName,
                        SourceID = item.SourceID
                    };

                    context.Lims_SamplePointArea.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;
        }

        public bool InactivateSamplePointArea(SamplePointArea item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lims_SamplePointArea efItem = context.Lims_SamplePointArea.Where(b => b.SamplePointAreaID == item.SamplePointAreaID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SamplePointAreaActive = false;

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

        #region Sample point 
        public bool UpdateSamplePoint(SamplePoint item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lims_SamplePoint efItem = context.Lims_SamplePoint.Where(b => b.SamplePointID == item.SamplePointID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SamplePointName = item.SamplePointName;
                    efItem.SamplePointActive = item.SamplePointActive;
                    efItem.SourceID = item.SourceID;

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception) { }
            return result;
        }

        public bool CreateSamplePoint(SamplePoint item)
        {

            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    int newid = context.Lims_SamplePoint.OrderByDescending(u => u.SamplePointID).FirstOrDefault().SamplePointID;
                    newid++;

                    Lims_SamplePoint efItem = new Lims_SamplePoint()
                    {
                        SamplePointID = newid,
                        SamplePointActive = true,
                        SamplePointName = item.SamplePointName,
                        SourceID = item.SourceID
                    };

                    context.Lims_SamplePoint.Add(efItem);

                    if (context.SaveChanges() > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e) { throw e; }
            return result;
        }

        public bool InactivateSamplePoint(SamplePoint item)
        {
            bool result = false;
            try
            {
                using (RCID_DWHEntities context = new RCID_DWHEntities())
                {
                    Lims_SamplePoint efItem = context.Lims_SamplePoint.Where(b => b.SamplePointID == item.SamplePointID).FirstOrDefault();

                    if (efItem == null) return result;

                    efItem.SamplePointActive = false;

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
