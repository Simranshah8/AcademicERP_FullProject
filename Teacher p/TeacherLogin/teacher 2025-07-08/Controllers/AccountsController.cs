using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TeacherLogin.Controllers
{
    public class AccountsController : TeacherLogin.Controllers.BaseController
    {

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
           
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogIn(TeacherLogin.Models.Teacher.TeacherLogin model)
        {

            if (model == null || string.IsNullOrEmpty(model.userName) || string.IsNullOrEmpty(model.Password))
            {
                ViewBag.ErrorMessage = "Pls Enter UserName And Password";
                return View();
            }
            else
            {
                TeacherLogin.Models.APIRequest sessCon = new TeacherLogin.Models.APIRequest(BaseUrl);
                try
                {
                    TeacherLogin.Models.TokenResponse logRes = sessCon.GetToken( model.userName, model.Password);

                    if (logRes!=null) {

                        TeacherLogin.Models.Teacher.TeacherLogin user = new TeacherLogin.Models.Teacher.TeacherLogin();
                        user.access_token = logRes.access_token;
                        user.expires_in = logRes.expires_in;
                        user.refresh_token = logRes.refresh_token;
                        user.userName = logRes.userName;
                        user.userId = logRes.userId;
                        user.customerCode = logRes.customerCode;
                        user.userGroup = logRes.userGroup;
                        user.Password = model.Password;
                        user.photoPath = logRes.photoPath;
                        user.name = logRes.name;
                        if (logRes != null && logRes.userGroup == "employee")
                        {
                            // user = ((List<TeacherLogin.Models.Teacher.StudentDetails>)responseData).First();
                            TeacherLogin.Models.SessionContext sessContext = new TeacherLogin.Models.SessionContext();
                            sessContext.SetAuthenticationToken(model.userName, false, user);
                            return RedirectToAction("Index", "Home");
                        }

                    }

                    else
                    {
                        ModelState.AddModelError("", "Invalid user & password. pls try again.");
                        return View();
                    }
                }
                catch (Exception e)
                {
                    TeacherLogin.Models.APIRequest ee = new TeacherLogin.Models.APIRequest();
                }
                ViewBag.ErrorMessage= "Pls Check Your Username and Password";
                return View();




            }

        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Accounts");
        }
        public ActionResult ChangePassword()
        {
            
            return View();
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAboutComp()
        {
            TeacherLogin.Models.AboutCompany dataColl = new Models.AboutCompany();

            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/GetAboutCompany", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.AboutCompany>(dataColl, keyValues);

                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.AboutCompany)responseData);
                }
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAppFeatures()
        {
            List<TeacherLogin.Models.AppFeatures> dataColl = new List<Models.AppFeatures>();

            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetAppFeatures", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.AppFeatures>>(dataColl, keyValues);

                if (responseData != null)
                {
                    dataColl = (List<TeacherLogin.Models.AppFeatures>)responseData;

                }
            }
            catch (Exception ee)
            {
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #region"ChangePwd"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult UpdatePassword()
        {
            TeacherLogin.Models.Teacher.Password dataColl = new TeacherLogin.Models.Teacher.Password();
            // List<TeacherLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = DeserializeObject<TeacherLogin.Models.Teacher.Password>(Request["jsonData"]);
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/UpdatePwd", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.Password>(beData, keyValues);              
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.Password)responseData);                    

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.Password err = new TeacherLogin.Models.Teacher.Password();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

    }
}