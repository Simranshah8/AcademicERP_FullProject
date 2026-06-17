using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentLogin.Controllers
{
    public class AccountsController : StudentLogin.Controllers.BaseController
    {
      
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("MyProfile", "Creation",new { area="Student" });

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogIn(StudentLogin.Models.Students.StudentUser model)
        {

            if (model == null || string.IsNullOrEmpty(model.userName) || string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", " pls Enter user & password");

                return View();
            }
            else
            {
                StudentLogin.Models.APIRequest sessCon = new StudentLogin.Models.APIRequest(BaseUrl);
                try
                {
                    StudentLogin.Models.TokenResponse logRes = sessCon.GetToken(model.userName, model.Password);
                    StudentLogin.Models.Students.StudentUser user =new StudentLogin.Models.Students.StudentUser();
                    user.access_token = logRes.access_token;
                    user.expires_in = logRes.expires_in;
                    //user.refresh_token = logRes.refresh_token;
                    user.userName = logRes.userName;
                    user.userId = logRes.userId;
                    user.customerCode = logRes.customerCode;
                    user.userGroup = logRes.userGroup;
                    user.Password = model.Password;

                    if (logRes != null && (logRes.userGroup == "student" || logRes.userGroup == "rstudent"))
                    {
                        // user = ((List<StudentLogin.Models.Teacher.StudentDetails>)responseData).First();
                        StudentLogin.Models.SessionContext sessContext = new StudentLogin.Models.SessionContext();
                        sessContext.SetAuthenticationToken(model.userName, false, user);
                        // return RedirectToAction("Index", "Home");
                        return RedirectToAction("MyProfile", "Creation", new { area = "Student" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid user & password. pls try again.");
                         return View();
                    }
                }
                catch(Exception ee)
                {

                }
                return View();

          
            }

        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("/Login");
        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetAboutComp()
        {
            StudentLogin.Models.AboutCompany dataColl = new Models.AboutCompany();

            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/GetAboutCompany", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<StudentLogin.Models.AboutCompany>(dataColl, keyValues);

                if (responseData != null)
                {
                    dataColl = ((StudentLogin.Models.AboutCompany)responseData);
                    dataColl.IsReg = User.isReg;
                }
            }
            catch (Exception ee)
            {
                dataColl.IsSuccess = false;
                dataColl.ResponseMSG = ee.Message;
            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetAppFeatures()
        {
            List<StudentLogin.Models.AppFeatures> dataColl = new List<Models.AppFeatures>();

            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetAppFeatures", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.AppFeatures>>(dataColl, keyValues);

                if (responseData != null)
                {
                    dataColl = (List<StudentLogin.Models.AppFeatures>)responseData;
                   
                }
            }
            catch (Exception ee)
            { 
            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
    }
}