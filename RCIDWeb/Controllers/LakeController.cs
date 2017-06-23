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
    public class LakeController : Controller
    {

        ILakeService _LakeSvc;


        public LakeController(ILakeService service)
        {
            _LakeSvc = service;
        }


        public ActionResult Parameters()
        {
            return View("ParametersView");
        }

        public ActionResult ProfileDetails()
        {
            return View("ProfileDetailsView");
        }

        public ActionResult Profiles()
        {
            return View("ProfilesView");
        }

        #region Get Grid Data
        public JsonResult GetParametersGrid(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _LakeSvc.GetAllParameters();

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {

                switch (sidx)
                {
                    case "ParameterName":
                        results = results.OrderByDescending(s => s.ParameterName);
                        break;
                    case "ParameterFullName":
                        results = results.OrderByDescending(s => s.ParameterFullName);
                        break;
                    case "ParameterTestMethod":
                        results = results.OrderByDescending(s => s.ParameterTestMethod);
                        break;
                    case "ParameterUnit":
                        results = results.OrderByDescending(s => s.ParameterUnit);
                        break;
                    case "ParameterActive":
                        results = results.OrderByDescending(s => s.ParameterActive);
                        break;

                }
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "ParameterName":
                        results = results.OrderBy(s => s.ParameterName);
                        break;
                    case "ParameterFullName":
                        results = results.OrderBy(s => s.ParameterFullName);
                        break;
                    case "ParameterTestMethod":
                        results = results.OrderBy(s => s.ParameterTestMethod);
                        break;
                    case "ParameterUnit":
                        results = results.OrderBy(s => s.ParameterUnit);
                        break;
                    case "ParameterActive":
                        results = results.OrderBy(s => s.ParameterActive);
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

        public JsonResult GetProfilesGrid(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _LakeSvc.GetAllProfiles();

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {

                switch (sidx)
                {
                    case "ProfileDate":
                        results = results.OrderByDescending(s => s.ProfileDate);
                        break;
                    case "SamplePointName":
                        results = results.OrderByDescending(s => s.SamplePointName);
                        break;

                }
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "ProfileDate":
                        results = results.OrderBy(s => s.ProfileDate);
                        break;
                    case "SamplePointName":
                        results = results.OrderBy(s => s.SamplePointName);
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

        public JsonResult GetProfileDetailsGrid(int id, string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var results = _LakeSvc.GetProfileDetails(id);

            int totalRecords = results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {

                switch (sidx)
                {
                    case "DepthFeet":
                        results = results.OrderByDescending(s => s.DepthFeet);
                        break;
                    case "ParameterName":
                        results = results.OrderByDescending(s => s.ParameterName);
                        break;
                    case "ParameterValue":
                        results = results.OrderByDescending(s => s.ParameterValue);
                        break;
                    case "ProfileDetailNotes":
                        results = results.OrderByDescending(s => s.ProfileDetailNotes);
                        break;
                    case "ProfileDetailActive":
                        results = results.OrderByDescending(s => s.ProfileDetailActive);
                        break;
                }
                results = results.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                switch (sidx)
                {
                    case "DepthFeet":
                        results = results.OrderBy(s => s.DepthFeet);
                        break;
                    case "ParameterName":
                        results = results.OrderBy(s => s.ParameterName);
                        break;
                    case "ParameterValue":
                        results = results.OrderBy(s => s.ParameterValue);
                        break;
                    case "ProfileDetailNotes":
                        results = results.OrderBy(s => s.ProfileDetailNotes);
                        break;
                    case "ProfileDetailActive":
                        results = results.OrderBy(s => s.ProfileDetailActive);
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
        public JsonResult GetParameterList()
        {
            var results = _LakeSvc.GetAllParameters().Where(s => s.ParameterActive == true).OrderBy(c => c.ParameterName).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
       
        #endregion

        #region Parameters

        public string EditParameters(LakeParameter item)
        {
            string msg = "Edit Parameters. An error has ocurred";

            try
            {

                if (!ModelState.IsValid)
                {
                    msg = "Data validation not successfull";
                    return msg;
                }

                if (_LakeSvc.UpdateParameter(item))
                {
                    msg = "Parameters saved succesfully";
                }

            }
            catch (Exception e)
            {
                msg = "Edit Parameters. An error has ocurred";
            }

            return msg;
        }

        public string CreateParameters([Bind(Exclude = "ParametersID")] LakeParameter item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _LakeSvc.CreateParameter(item);
                    msg = "Parameters created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Parameters. An error has ocurred";
            }

            return msg;
        }

        public string DeleteParameters(LakeParameter item)
        {
            string msg = string.Empty;

            item.ParameterActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _LakeSvc.InactivateParameter(item);
                    msg = "Parameters inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Parameters. An error has ocurred";
            }

            return msg;
        }
        #endregion

        #region Profiles

        public string EditProfile(LakeProfile item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _LakeSvc.UpdateProfile(item);
                    msg = "Profile saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Profile. An error has ocurred";
            }

            return msg;
        }

        public string CreateProfile([Bind(Exclude = "ProfileID")] LakeProfile item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _LakeSvc.CreateProfile(item);
                    msg = "Profile created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Profile. An error has ocurred";
            }

            return msg;
        }

        public string DeleteProfile(LakeProfile item)
        {
            string msg = string.Empty;

            item.ProfileActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _LakeSvc.InactivateProfile(item);
                    msg = "Profile inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Profile. An error has ocurred";
            }

            return msg;
        }

        #endregion
        #region ProfileDetails
        public string EditProfileDetail(LakeProfileDetail item)
        {
            string msg = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    _LakeSvc.UpdateProfileDetails(item);
                    msg = "Profile detail saved succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Edit Profile detail. An error has ocurred";
            }

            return msg;
        }

        public string CreateProfileDetail(LakeProfileDetail item)
        {
            string msg = string.Empty;

            try
            {
                if (item.DepthFeet > 0)
                {
                    _LakeSvc.CreateProfileDetails(item);
                    msg = "Profile detail created succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Create Profile detail. An error has ocurred";
            }

            return msg;
        }

        public string DeleteProfileDetail(LakeProfileDetail item)
        {
            string msg = string.Empty;

            item.ProfileDetailActive = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _LakeSvc.InactivateProfileDetails(item);
                    msg = "Profile detail inactivated succesfully";
                }
                else
                {
                    msg = "Data validation not successfull";
                }
            }
            catch (Exception e)
            {
                msg = "Delete Profile detail. An error has ocurred";
            }

            return msg;
        }


        #endregion



    }
}