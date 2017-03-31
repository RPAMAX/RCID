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

        public GeneralController(IGeneralService genService)
        {            
            _genSvc = genService;
        }


        // GET: General
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetClimates()
        {
            var results = _genSvc.GetAllClimates().ToList();
         
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSamplePointAreas()
        {
            var results = _genSvc.GetAllSamplePointAreas().ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }


    }
}