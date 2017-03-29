﻿using RCIDService;
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
        IGeneralService _genSvc;

        public BirdsController(IBirdService service, IGeneralService genService)
        {
            _birdSvc = service;
            _genSvc = genService;
        }


        // GET: Birds
        public ActionResult Index()
        {
            return View("SpeciesView");
        }
        public ActionResult Surveyors()
        {
            return View("SurveyorsView");
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
                Results = Results.OrderByDescending(s => s.SpeciesID);
                Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Results = Results.OrderBy(s => s.SpeciesID);
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

        public JsonResult GetClimates()
        {
            var results = _genSvc.GetAllClimates().ToList();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}