using RCIDRepository.Domain;
using RCIDService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RCIDWeb.Controllers
{
    public class GeneralController : Controller
    {
        IGeneralService _genSvc;
        
        readonly byte LIMS_SOURCEID = 5;
        readonly byte LIMS_SOURCEID_PHYTO = 1;
        readonly byte LIMS_SAMPLETYPEID = 2;

        public GeneralController(IGeneralService genService)
        {
            _genSvc = genService;
        }


        // GET: General
        public ActionResult SamplePointArea()
        {
            return View("SamplePointAreaView");
        }

        public ActionResult Climate()
        {
            return View("ClimateView");
        }

        #region Gets for Dropdowns

        public JsonResult GetClimates()
        {
            var results = _genSvc.GetAllClimates().Where(c=>c.ClimateActive==true).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSamplePointAreas()
        {
            var results = _genSvc.GetAllSamplePointAreas().Where(s=>s.SamplePointAreaActive == true 
                                                                    && s.SourceID == LIMS_SOURCEID)
                                                                    .OrderBy(s=>s.SamplePointAreaName).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActiveSamplePoints()
        {
            var results = _genSvc.GetAllSamplePoints().Where(s => s.SamplePointActive == true
                                                                && s.SourceID == LIMS_SOURCEID_PHYTO
                                                                && s.SampleTypeID == LIMS_SAMPLETYPEID)
                                                                .OrderBy(s=>s.SamplePointRefID).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Gets for grids
        public JsonResult GetClimatesGrid(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = _genSvc.GetAllClimates();

            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "ClimateName":
                        Results = Results.OrderByDescending(s => s.ClimateName);
                        break;
                    case "ClimateActive":
                        Results = Results.OrderByDescending(s => s.ClimateActive);
                        break;                   
                }
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "ClimateName":
                        Results = Results.OrderBy(s => s.ClimateName);
                        break;
                    case "ClimateActive":
                        Results = Results.OrderBy(s => s.ClimateActive);
                        break;
                }
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSamplePointAreasGrid(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = _genSvc.GetAllSamplePointAreas();

            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "SamplePointAreaName":
                        Results = Results.OrderByDescending(s => s.SamplePointAreaName);
                        break;
                    case "SamplePointAreaActive":
                        Results = Results.OrderByDescending(s => s.SamplePointAreaActive);
                        break;
                }
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "SamplePointAreaName":
                        Results = Results.OrderBy(s => s.SamplePointAreaName);
                        break;
                    case "SamplePointAreaActive":
                        Results = Results.OrderBy(s => s.SamplePointAreaActive);
                        break;
                }
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSamplePointsGrid(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = _genSvc.GetAllSamplePoints();

            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                Results = Results.OrderByDescending(s => s.SamplePointName);
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Results = Results.OrderBy(s => s.SamplePointName);
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Climate functions

        public string EditClimate(WeatherClimate item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _genSvc.UpdateClimate(item);
                    msg = "Climate saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Climate. An error has ocurred";
            }

            return msg;
        }

        public string CreateClimate([Bind(Exclude = "ClimateID")] WeatherClimate item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _genSvc.CreateClimate(item);
                    msg = "Climate created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Climate. An error has ocurred";
            }

            return msg;
        }

        public string DeleteClimate(WeatherClimate item)
        {
            string msg = string.Empty;

            item.ClimateActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _genSvc.InactivateClimate(item);
                    msg = "Climate inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Climate. An error has ocurred";
            }

            return msg;
        }
        #endregion

        #region Sample Point Area functions
        public string EditSamplePointArea(SamplePointArea item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _genSvc.UpdateSamplePointArea(item);
                    msg = "Sample Point Area saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Sample Point Area. An error has ocurred";
            }

            return msg;
        }

        public string CreateSamplePointArea([Bind(Exclude = "SamplePointAreaID")] SamplePointArea item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _genSvc.CreateSamplePointArea(item);
                    msg = "Sample Point Area created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Sample Point Area. An error has ocurred";
            }

            return msg;
        }

        public string DeleteSamplePointArea(SamplePointArea item)
        {
            string msg = string.Empty;

            item.SamplePointAreaActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _genSvc.InactivateSamplePointArea(item);
                    msg = "Sample Point Area inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Sample Point Area. An error has ocurred";
            }

            return msg;
        }
        #endregion

      

    }


}

