using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentLogin.Areas.Student.Controllers
{
    public class UserController : StudentLogin.Controllers.BaseController
    {
        // GET: Student/User
        public ActionResult  ChangePassword()
        {
            return View();
        }

        #region"ChangePwd"
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdatePassword()
        {
            StudentLogin.Models.User.Password dataColl = new StudentLogin.Models.User.Password();
          
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };

                var beData = DeserializeObject<StudentLogin.Models.User.Password>(Request["jsonData"]);
                if (String.IsNullOrEmpty(beData.oldPwd))
                {
                    dataColl.ResponseMSG = "Pls Enter Your Current Password";
                    dataColl.IsSuccess = false;

                }
                else if (String.IsNullOrEmpty(beData.newPwd))
                {
                    dataColl.ResponseMSG = "Pls Enter New Password";
                    dataColl.IsSuccess = false;

                }
                else if (String.IsNullOrEmpty(beData.Conform))
                {
                    dataColl.ResponseMSG = "Pls Enter Comform";
                    dataColl.IsSuccess = false;

                }
                else if (beData.newPwd != beData.Conform)
                {
                    dataColl.ResponseMSG = "New and Comform Password Does Not Match";
                    dataColl.IsSuccess = false;

                }
                else
                {

                    StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl,"General/UpdatePwd", "POST");
                   Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);
                    var responseData = request.Execute<StudentLogin.Models.User.Password>(beData, keyValues);
                    if (responseData != null)
                    {
                        dataColl = ((StudentLogin.Models.User.Password)responseData);

                    }


                }

            }
            catch (Exception ee)
            {
                StudentLogin.Models.User.Password err = new StudentLogin.Models.User.Password();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
    }
}