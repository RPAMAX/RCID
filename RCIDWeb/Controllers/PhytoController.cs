using RCIDRepository.Domain;
using RCIDService;
using RCIDWeb.Models;
using RCIDWeb.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RCIDWeb.Controllers
{
    [Authorize]
    public class PhytoController : Controller
    {

        IPhytoService _PhytoSvc;
        

        public PhytoController(IPhytoService service)
        {
            _PhytoSvc = service;           
        }


        // GET: Phyto
        public ActionResult Species()
        {
            return View("SpeciesView");
        }               

        public ActionResult Surveys()
        {
            return View("SurveysView");
        }

        public ActionResult Divisions()
        {
            return View("DivisionsView");
        }

        #region Get Grid Data
        public JsonResult GetSpecies(string sidx, string sord, int page, int rows,
            bool _search, string searchField, string searchOper, string searchString)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var Results = _PhytoSvc.GetAllSpecies();

            if (_search)
            {
                switch (searchOper)
                {
                    case "cn":
                        switch(searchField){
                            case "SpeciesName":
                                Results = Results.Where(s => s.SpeciesName.Contains(searchString));
                                break;
                            case "DivisionName":
                                Results = Results.Where(s => s.DivisionName.Contains(searchString));
                                break;
                        }                       
                        break;
                    case "bw":
                        switch (searchField)
                        {
                            case "SpeciesName":
                                Results = Results.Where(s => s.SpeciesName.StartsWith(searchString));
                                break;
                            case "DivisionName":
                                Results = Results.Where(s => s.DivisionName.StartsWith(searchString));
                                break;
                        }
                        break;
                }
            }
            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                Results = Results.OrderByDescending(s => s.SpeciesName);
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Results = Results.OrderBy(s => s.SpeciesName);
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

        

        public JsonResult GetDivisionsGrid(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = _PhytoSvc.GetAllDivisions();

            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "DivisionName":
                        Results = Results.OrderByDescending(s => s.DivisionName);
                        break;
                    case "DivisionCommonName":
                        Results = Results.OrderByDescending(s => s.DivisionCommonName);
                        break;
                    case "DivisionActive":
                        Results = Results.OrderByDescending(s => s.DivisionActive);
                        break;
                }
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "DivisionName":
                        Results = Results.OrderBy(s => s.DivisionName);
                        break;
                    case "DivisionCommonName":
                        Results = Results.OrderBy(s => s.DivisionCommonName);
                        break;
                    case "DivisionActive":
                        Results = Results.OrderBy(s => s.DivisionActive);
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

        public JsonResult GetSurveys(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _PhytoSvc.GetAllSurveys();

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "SurveyDate":
                        results = results.OrderByDescending(s => s.SurveyDate);
                        break;
                    case "LocationDetails":
                        results = results.OrderByDescending(s => s.LocationDetails);
                        break;                    
                    case "SamplePointAreaName":
                        results = results.OrderByDescending(s => s.SamplePointAreaName);
                        break;
                    case "SurveyActive":
                        results = results.OrderByDescending(s => s.SurveyActive);
                        break;

                }
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "SurveyDate":
                        results = results.OrderBy(s => s.SurveyDate);
                        break;
                    case "LocationDetails":
                        results = results.OrderBy(s => s.LocationDetails);
                        break;
                    case "SamplePointAreaName":
                        results = results.OrderBy(s => s.SamplePointAreaName);
                        break;
                    case "SurveyActive":
                        results = results.OrderBy(s => s.SurveyActive);
                        break;

                }
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

       

        public JsonResult GetSurveyDetails(int id, string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _PhytoSvc.GetSurveyDetails(id);

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "SpeciesName":
                        results = results.OrderByDescending(s => s.SpeciesName);
                        break;
                    case "SurveyCount":
                        results = results.OrderByDescending(s => s.SurveyCount);
                        break;
                    case "SurveyDetailActive":
                        results = results.OrderByDescending(s => s.SurveyDetailActive);
                        break;     
                }
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "SpeciesName":
                        results = results.OrderBy(s => s.SpeciesName);
                        break;
                    case "SurveyCount":
                        results = results.OrderBy(s => s.SurveyCount);
                        break;
                    case "SurveyDetailActive":
                        results = results.OrderBy(s => s.SurveyDetailActive);
                        break;
                }
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region GetListData
        public JsonResult GetSpeciesList()
        {
            var results = _PhytoSvc.GetAllSpecies().Where(s=>s.SpeciesActive == true).OrderBy(c => c.SpeciesName).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDivisionsList()
        {
            var results = _PhytoSvc.GetAllDivisions().Where(g => g.DivisionActive == true).OrderBy(g => g.DivisionName).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Species

        public string EditSpecies(PhytoSpecies item)
        {
            string msg = "Edit Species. An error has ocurred";

            try
            {

                if (!ModelState.IsValid)
                {
                    msg = "Data validation not successfull";
                    return msg;
                }

                if (_PhytoSvc.UpdateSpecies(item))
                {
                    msg = "Species saved succesfully";
                }
                
            }
            catch (Exception e) {
                msg = "Edit Species. An error has ocurred";
            }

            return msg;
        }

        public string CreateSpecies([Bind(Exclude ="SpeciesID")] PhytoSpecies item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.CreateSpecies(item);
                    msg = "Species created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Species. An error has ocurred";
            }

            return msg;
        }

        public string DeleteSpecies(PhytoSpecies item)
        {
            string msg = string.Empty;

            item.SpeciesActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.InactivateSpecies(item);
                    msg = "Species inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Species. An error has ocurred";
            }

            return msg;
        }
        #endregion
    
        #region Divisions

        public string EditDivision(PhytoDivision item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.UpdateDivision(item);
                    msg = "Division saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Division. An error has ocurred";
            }

            return msg;
        }

        public string CreateDivision([Bind(Exclude = "DivisionID")] PhytoDivision item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.CreateDivision(item);
                    msg = "Division created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Division. An error has ocurred";
            }

            return msg;
        }

        public string DeleteDivision(PhytoDivision item)
        {
            string msg = string.Empty;

            item.DivisionActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.InactivateDivision(item);
                    msg = "Division inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Division. An error has ocurred";
            }

            return msg;
        }

        #endregion
        #region Surveys
        public string EditSurvey(PhytoSurvey item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.UpdateSurvey(item);
                    msg = "Survey saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Survey. An error has ocurred";
            }

            return msg;
        }

        public string CreateSurvey([Bind(Exclude = "SurveyID")] PhytoSurvey item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.CreateSurvey(item);
                    msg = "Survey created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Survey. An error has ocurred";
            }

            return msg;
        }

        public string DeleteSurvey(PhytoSurvey item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.InactivateSurvey(item);
                    msg = "Survey inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Survey. An error has ocurred";
            }

            return msg;
        }
        #endregion
 
        #region Survey Details
        public string CreateSurveyDetail(PhytoSurveyDetails item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.CreateSurveyDetail(item);
                    msg = "Survey detail created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Survey detail. An error has ocurred";
            }

            return msg;
        }

        public string EditSurveyDetail(PhytoSurveyDetails item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.UpdateSurveyDetail(item);
                    msg = "Survey Detail saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Survey Detail. An error has ocurred";
            }

            return msg;
        }

        public string DeleteSurveyDetail(PhytoSurveyDetails item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _PhytoSvc.InactivateSurveyDetail(item);
                    msg = "Survey Detail inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Survey Detail. An error has ocurred";
            }

            return msg;
        }
        #endregion

       

    }
}