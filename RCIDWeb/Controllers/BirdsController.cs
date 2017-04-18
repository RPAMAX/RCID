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
    public class BirdsController : Controller
    {

        IBirdService _birdSvc;
        

        public BirdsController(IBirdService service)
        {
            _birdSvc = service;           
        }


        // GET: Birds
        public ActionResult Species()
        {
            return View("SpeciesView");
        }

        public ActionResult Surveyors()
        {
            return View("SurveyorsView");
        }

        public ActionResult Surveys()
        {
            return View("SurveysView");
        }

        public JsonResult GetSpecies(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Results = _birdSvc.GetAllSpecies();

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

        public JsonResult GetSurveyors(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _birdSvc.GetAllSurveyors();

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                results = results.OrderByDescending(s => s.SurveyorName);
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                results = results.OrderBy(s => s.SurveyorName);
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

        public JsonResult GetSurveyorsList()
        {
            var results = _birdSvc.GetAllSurveyors().OrderBy(c=>c.SurveyorName).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSurveys(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _birdSvc.GetAllSurveys();

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                results = results.OrderByDescending(s => s.SurveyorID);
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                results = results.OrderBy(s => s.SurveyorID);
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

        public string EditSpecies(BirdSpecies item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.UpdateSpecies(item);
                    msg = "Species saved succesfully";
                }
                else {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e) {
                msg = "Edit Species. An error has ocurred";
            }

            return msg;
        }

        public string CreateSpecies([Bind(Exclude ="SpeciesID")] BirdSpecies item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.CreateSpecies(item);
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


        public string EditSurveyor(BirdSurveyor item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.UpdateSurveyor(item);
                    msg = "Surveyor saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Surveyor. An error has ocurred";
            }

            return msg;
        }

        public string CreateSurveyor([Bind(Exclude = "SurveyorID")] BirdSurveyor item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.CreateSurveyor(item);
                    msg = "Surveyor created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Surveyor. An error has ocurred";
            }

            return msg;
        }


        public string EditSurvey(BirdSurvey item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.UpdateSurvey(item);
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

        public string CreateSurvey([Bind(Exclude = "SurveyID")] BirdSurvey item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.CreateSurvey(item);
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



        #region import excel

        public ActionResult ImportExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportExcel(FormCollection formCollection)
        {
            ValidationErrors model = new ValidationErrors();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];

                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;

                    List<BirdSurvey> parsedList =  BirdsExcelParser.ParseFile(fileName);

                    if (parsedList != null) {
                       //validate in service first
                        List<List<string>> errorList = _birdSvc.ValidateImportList(parsedList);
                                                
                        model.SPAErrors = errorList[0];
                        model.SurveyorErrors = errorList[1];
                        model.ClimateErrors = errorList[2];
                        model.SpeciesErrors = errorList[3];

                        //if there are no errors, call the service to save to DB
                        var errors = errorList.Where(s => s.Count > 0).FirstOrDefault();
                        if (errors == null)
                        {
                            _birdSvc.SaveSurveys(parsedList);
                        }
                    }
                }
            }
            return View(model);
        }
        #endregion

    }
}