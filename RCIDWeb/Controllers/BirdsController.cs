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

        public JsonResult GetSurveyDetails(int id, string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _birdSvc.GetSurveyDetails(id);

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                results = results.OrderByDescending(s => s.SpeciesName);
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                results = results.OrderBy(s => s.SpeciesName);
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

        public string DeleteSpecies(BirdSpecies item)
        {
            string msg = string.Empty;

            item.SpeciesActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.InactivateSpecies(item);
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

        public string InactivateSurveyor(BirdSurveyor item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.InactivateSurveyor(item);
                    msg = "Surveyor inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Inactivate Surveyor. An error has ocurred";
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

        public string DeleteSurvey(BirdSurvey item)
        {
            string msg = string.Empty;                     

            try
            {
                if (ModelState.IsValid)
                {
                    _birdSvc.InactivateSurvey(item);
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

        #region import excel

        public ActionResult ImportExcel()
        {
            ValidationErrors model = new ValidationErrors();
            return View(model);
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
                    try
                    {
                        List<BirdSurvey> parsedList = BirdsExcelParser.ParseFile(fileName);
                        model.ProcessedRows = parsedList.Count();

                        if (parsedList != null)
                        {
                            //validate in service first
                            List<List<string>> errorList = _birdSvc.ValidateImportList(parsedList);

                            model.SPAErrors = errorList[0];
                            model.SurveyorErrors = errorList[1];
                            model.ClimateErrors = errorList[2];
                            model.SpeciesErrors = errorList[3];

                            model.ErrorCount = model.SPAErrors.Count() +
                                               model.SurveyorErrors.Count() +
                                               model.SpeciesErrors.Count() +
                                               model.ClimateErrors.Count();

                            //if there are no errors, call the service to save to DB
                            var errors = errorList.Where(s => s.Count > 0).FirstOrDefault();
                            if (errors == null)
                            {
                                _birdSvc.SaveSurveys(parsedList);
                            }
                        }
                    }
                    catch (FormatException fe)
                    {
                        model.FormatError = true;
                        model.ErrorCount = 1;
                    }
                    catch (Exception e)
                    {
                        model.ErrorCount = 1;
                        model.GeneralError = e.Message;
                    }
                }
            }
            return View(model);
        }
        #endregion

    }
}