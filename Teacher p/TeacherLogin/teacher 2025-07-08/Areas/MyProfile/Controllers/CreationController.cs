using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.MyProfile.Controllers
{
    public class CreationController :TeacherLogin.Controllers.BaseController
    {
        // GET: MyProfile/Creation
        public ActionResult MyProfile()
        {
            ViewBag.WebURL = WebUrl;
            return View();

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAllCaste()
        {
            List<TeacherLogin.Models.Academic.Caste> dataColl = new List<Models.Academic.Caste>();

            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Academic/GetCasteList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Academic.Caste>>(dataColl, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Academic.Caste>)responseData);
                }
            }
            catch (Exception ee)
            {
                
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #region"TeacherProfile"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAllProfile()
        {
            TeacherLogin.Models.Teacher.TeacherProfile dataColl = new Models.Teacher.TeacherProfile();
           
            try
            {                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetEmployeeProfile", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.TeacherProfile>(dataColl, keyValues);
        
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.TeacherProfile)responseData);        
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.TeacherProfile err = new TeacherLogin.Models.Teacher.TeacherProfile();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult UpdatePersonalInfo(TeacherLogin.Models.Profile.PersonalInformation beData)
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/UpdatePersonalInfo", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);

                if (responseData != null)
                {
                    resVal = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult UpdatePermanentAddress(TeacherLogin.Models.Profile.PermananetAddress beData)
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/UpdatePermanentAddress", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);

                if (responseData != null)
                {
                    resVal = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult UpdateTemporaryAddress(TeacherLogin.Models.Profile.TemporaryAddress beData)
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/UpdateTemporaryAddress", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);

                if (responseData != null)
                {
                    resVal = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult UpdateCitizenship(TeacherLogin.Models.Profile.CitizenshipDetails beData)
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/UpdateCitizenship", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);

                if (responseData != null)
                {
                    resVal = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult UpdateCITDetails(TeacherLogin.Models.Profile.CITDetails beData)
        {

            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/UpdateCITDetails", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);

                if (responseData != null)
                {
                    resVal = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal };

        }
    }
}