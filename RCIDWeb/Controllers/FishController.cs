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
    public class FishController : Controller
    {

        IFishService _fishSvc;
        

        public FishController(IFishService service)
        {
            _fishSvc = service;           
        }


        // GET: Fish
        public ActionResult Species()
        {
            return View("SpeciesView");
        }

        public ActionResult SpeciesGroup()
        {
            return View("SpeciesGroupView");
        }

        public ActionResult Surveys()
        {
            return View("SurveysView");
        }

        public ActionResult Generators()
        {
            return View("GeneratorsView");
        }

        #region Get Grid Data
        public JsonResult GetSpecies(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = _fishSvc.GetAllSpecies();

            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "SpeciesName":
                        Results = Results.OrderByDescending(s => s.SpeciesName);
                        break;
                    case "SpeciesActive":
                        Results = Results.OrderByDescending(s => s.SpeciesActive);
                        break;
                    case "SpeciesGroupName":
                        Results = Results.OrderByDescending(s => s.SpeciesGroupName);
                        break;
                }
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "SpeciesName":
                        Results = Results.OrderBy(s => s.SpeciesName);
                        break;
                    case "SpeciesActive":
                        Results = Results.OrderBy(s => s.SpeciesActive);
                        break;
                    case "SpeciesGroupName":
                        Results = Results.OrderBy(s => s.SpeciesGroupName);
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

        public JsonResult GetSpeciesGroup(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = _fishSvc.GetAllSpeciesGroup();

            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "SpeciesGroupName":
                        Results = Results.OrderByDescending(s => s.SpeciesGroupName);
                        break;
                    case "SpeciesGroupActive":
                        Results = Results.OrderByDescending(s => s.SpeciesGroupActive);
                        break;                   
                }
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "SpeciesGroupName":
                        Results = Results.OrderBy(s => s.SpeciesGroupName);
                        break;
                    case "SpeciesGroupActive":
                        Results = Results.OrderBy(s => s.SpeciesGroupActive);
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

        public JsonResult GetGeneratorsGrid(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = _fishSvc.GetAllGenerators();

            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "GeneratorName":
                        Results = Results.OrderByDescending(s => s.GeneratorName);
                        break;
                    case "GeneratorActive":
                        Results = Results.OrderByDescending(s => s.GeneratorActive);
                        break;
                }
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "GeneratorName":
                        Results = Results.OrderBy(s => s.GeneratorName);
                        break;
                    case "GeneratorActive":
                        Results = Results.OrderBy(s => s.GeneratorActive);
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
            var results = _fishSvc.GetAllSurveys();

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "SurveyYear":
                        results = results.OrderByDescending(s => s.SurveyYear);
                        break;
                    case "SurveyComments":
                        results = results.OrderByDescending(s => s.SurveyComments);
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
                    case "SurveyYear":
                        results = results.OrderBy(s => s.SurveyYear);
                        break;
                    case "SurveyComments":
                        results = results.OrderBy(s => s.SurveyComments);
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

        public JsonResult GetSurveyLocations(int id, string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _fishSvc.GetSurveyLocations(id);

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "SurveyNumber":
                        results = results.OrderByDescending(s => s.SurveyNumber);
                        break;
                    case "LocationDetails":
                        results = results.OrderByDescending(s => s.LocationDetails);
                        break;
                    case "SurveyDate":
                        results = results.OrderByDescending(s => s.SurveyDate);
                        break;
                    case "SurveyDurationSeconds":
                        results = results.OrderByDescending(s => s.SurveyDurationSeconds);
                        break;
                    case "GeneratorName":
                        results = results.OrderByDescending(s => s.GeneratorName);
                        break;
                    case "SurveyLocationComments":
                        results = results.OrderByDescending(s => s.SurveyLocationComments);
                        break;
                    case "SurveyLocationActive":
                        results = results.OrderByDescending(s => s.SurveyLocationActive);
                        break;

                }
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "SurveyNumber":
                        results = results.OrderBy(s => s.SurveyNumber);
                        break;
                    case "LocationDetails":
                        results = results.OrderBy(s => s.LocationDetails);
                        break;
                    case "SurveyDate":
                        results = results.OrderBy(s => s.SurveyDate);
                        break;
                    case "SurveyDurationSeconds":
                        results = results.OrderBy(s => s.SurveyDurationSeconds);
                        break;
                    case "GeneratorName":
                        results = results.OrderBy(s => s.GeneratorName);
                        break;
                    case "SurveyLocationComments":
                        results = results.OrderBy(s => s.SurveyLocationComments);
                        break;
                    case "SurveyLocationActive":
                        results = results.OrderBy(s => s.SurveyLocationActive);
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

        public JsonResult GetSurveyDetails(int id, int surveyID, string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _fishSvc.GetSurveyDetails(id,surveyID);

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                switch (sidx)
                {
                    case "SurveyNumber":
                        results = results.OrderByDescending(s => s.SurveyNumber);
                        break;
                    case "SpeciesName":
                        results = results.OrderByDescending(s => s.SpeciesName);
                        break;
                    case "SpeciesSizeMillimeters":
                        results = results.OrderByDescending(s => s.SpeciesSizeMillimeters);
                        break;
                    case "SpeciesSizeInches":
                        results = results.OrderByDescending(s => s.SpeciesSizeInches);
                        break;
                    case "SpeciesSizeInchGroup":
                        results = results.OrderByDescending(s => s.SpeciesSizeInchGroup);
                        break;
                    case "SpeciesWeightPounds":
                        results = results.OrderByDescending(s => s.SpeciesWeightPounds);
                        break;
                    case "SpeciesWeightOunces":
                        results = results.OrderByDescending(s => s.SpeciesWeightOunces);
                        break;
                    case "SpeciesWeightLbs":
                        results = results.OrderByDescending(s => s.SpeciesWeightLbs);
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
                    case "SurveyNumber":
                        results = results.OrderBy(s => s.SurveyNumber);
                        break;
                    case "SpeciesName":
                        results = results.OrderBy(s => s.SpeciesName);
                        break;
                    case "SpeciesSizeMillimeters":
                        results = results.OrderBy(s => s.SpeciesSizeMillimeters);
                        break;
                    case "SpeciesSizeInches":
                        results = results.OrderBy(s => s.SpeciesSizeInches);
                        break;
                    case "SpeciesSizeInchGroup":
                        results = results.OrderBy(s => s.SpeciesSizeInchGroup);
                        break;
                    case "SpeciesWeightPounds":
                        results = results.OrderBy(s => s.SpeciesWeightPounds);
                        break;
                    case "SpeciesWeightOunces":
                        results = results.OrderBy(s => s.SpeciesWeightOunces);
                        break;
                    case "SpeciesWeightLbs":
                        results = results.OrderBy(s => s.SpeciesWeightLbs);
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
            var results = _fishSvc.GetAllSpecies().Where(s=>s.SpeciesActive==true).OrderBy(c => c.SpeciesName).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSpeciesGroupList()
        {
            var results = _fishSvc.GetAllSpeciesGroup().Where(s => s.SpeciesGroupActive == true).OrderBy(c => c.SpeciesGroupName).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Species

        public string EditSpecies(FishSpecies item)
        {
            string msg = "Edit Species. An error has ocurred";

            try
            {

                if (!ModelState.IsValid)
                {
                    msg = "Data validation not successfull";
                    return msg;
                }

                if (_fishSvc.UpdateSpecies(item))
                {
                    msg = "Species saved succesfully";
                }
                
            }
            catch (Exception e) {
                msg = "Edit Species. An error has ocurred";
            }

            return msg;
        }

        public string CreateSpecies([Bind(Exclude ="SpeciesID")] FishSpecies item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.CreateSpecies(item);
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

        public string DeleteSpecies(FishSpecies item)
        {
            string msg = string.Empty;

            item.SpeciesActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.InactivateSpecies(item);
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
        #region Species Groups
        public string EditSpeciesGroup(FishSpeciesGroup item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.UpdateSpeciesGroup(item);
                    msg = "Species Group saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Species Group. An error has ocurred";
            }

            return msg;
        }

        public string CreateSpeciesGroup([Bind(Exclude = "SpeciesGroupID")] FishSpeciesGroup item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.CreateSpeciesGroup(item);
                    msg = "Species Group created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Species Group. An error has ocurred";
            }

            return msg;
        }

        public string DeleteSpeciesGroup(FishSpeciesGroup item)
        {
            string msg = string.Empty;

            item.SpeciesGroupActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.InactivateSpeciesGroup(item);
                    msg = "SpeciesGroup inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Species Group. An error has ocurred";
            }

            return msg;
        }
        #endregion
        #region Generators

        public string EditGenerator(FishGenerator item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.UpdateGenerator(item);
                    msg = "Generator saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Generator. An error has ocurred";
            }

            return msg;
        }

        public string CreateGenerator([Bind(Exclude = "GeneratorID")] FishGenerator item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.CreateGenerator(item);
                    msg = "Generator created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Generator. An error has ocurred";
            }

            return msg;
        }

        public string DeleteGenerator(FishGenerator item)
        {
            string msg = string.Empty;

            item.GeneratorActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.InactivateGenerator(item);
                    msg = "Generator inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Generator. An error has ocurred";
            }

            return msg;
        }

        #endregion
        #region Surveys
        public string EditSurvey(FishSurvey item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.UpdateSurvey(item);
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

        public string CreateSurvey([Bind(Exclude = "SurveyID")] FishSurvey item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.CreateSurvey(item);
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

        public string DeleteSurvey(FishSurvey item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.InactivateSurvey(item);
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
        #region Survey Locations
        public string CreateSurveyLocation(FishSurveyLocation item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.CreateSurveyLocation(item);
                    msg = "Survey location created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Survey location. An error has ocurred";
            }

            return msg;
        }

        public string EditSurveyLocation(FishSurveyLocation item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.UpdateSurveyLocation(item);
                    msg = "Survey location saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Survey location. An error has ocurred";
            }

            return msg;
        }

        public string DeleteSurveyLocation(FishSurveyLocation item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.InactivateSurveyLocation(item);
                    msg = "Survey location inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Survey location. An error has ocurred";
            }

            return msg;
        }

        #endregion

        #region Survey Details
        public string CreateSurveyDetail(FishSurveyDetails item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.CreateSurveyDetail(item);
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

        public string EditSurveyDetail(FishSurveyDetails item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.UpdateSurveyDetail(item);
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

        public string DeleteSurveyDetail(FishSurveyDetails item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _fishSvc.InactivateSurveyDetail(item);
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

        #region Get Dropdown lists
        public JsonResult GetGenerators()
        {
            var results = _fishSvc.GetAllGenerators().Where(g=>g.GeneratorActive == true).OrderBy(g=>g.GeneratorName).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}