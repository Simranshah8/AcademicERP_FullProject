using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.OnlineClass.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: OnlineClass/Creation
        public ActionResult OnlineClass()
        {
            return View();
        }

        public ActionResult RptOnlineClass()
        {
            return View();
        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetRptOnlineClass(DateTime? forDate)
        {
            TeacherLogin.Models.Teacher.OnlineClassAdmin dataColl = new Models.Teacher.OnlineClassAdmin();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/RptOnlineClasses", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var data = new
                {
                    forDate=forDate
                };
                var responseData = request.Execute<TeacherLogin.Models.Teacher.OnlineClassAdmin>(data, keyValues);
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.OnlineClassAdmin)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.RunningClasses err = new TeacherLogin.Models.Teacher.RunningClasses();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetDateWiseAttendance(DateTime? forDate,int classId,int? sectionId)
        {
            TeacherLogin.Models.Teacher.DateWiseOnlineAttendanceCollections dataColl = new Models.Teacher.DateWiseOnlineAttendanceCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetDateWiseOnlineAttendance", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var data = new
                {
                    forDate = forDate,            
                    classId=classId,
                    sectionId=sectionId
                };
                var responseData = request.Execute<TeacherLogin.Models.Teacher.DateWiseOnlineAttendanceCollections>(data, keyValues);
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.DateWiseOnlineAttendanceCollections)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.RunningClasses err = new TeacherLogin.Models.Teacher.RunningClasses();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetSubjectWiseAttendance(DateTime? fromDate,DateTime? toDate, int classId, int? sectionId,int subjectId)
        {
            TeacherLogin.Models.Teacher.DateWiseOnlineAttendanceCollections dataColl = new Models.Teacher.DateWiseOnlineAttendanceCollections();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetSubjectWiseOnlineAttendance", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var data = new
                {
                    fromDate = fromDate,
                    toDate=toDate,
                    classId = classId,
                    sectionId = sectionId,
                    subjectId=subjectId
                };
                var responseData = request.Execute<TeacherLogin.Models.Teacher.DateWiseOnlineAttendanceCollections>(data, keyValues);
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.DateWiseOnlineAttendanceCollections)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.RunningClasses err = new TeacherLogin.Models.Teacher.RunningClasses();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }

        #region"StartOnlineClass"

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult StartOnlineClass()
        {
            TeacherLogin.Models.Teacher.StartOnlineClass dataColl = new TeacherLogin.Models.Teacher.StartOnlineClass();
            // List<TeacherLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                var beData = DeserializeObject<TeacherLogin.Models.Teacher.StartOnlineClass>(Request["jsonData"]);
                if (beData.PlatformType==0)
                {
                    dataColl.IsSuccess = false;
                    dataColl.ResponseMSG = "Pls! Select PlatFrom Type";
                }
              else if (string.IsNullOrEmpty(beData.UserName))
                {
                    dataColl.IsSuccess = false;
                    dataColl.ResponseMSG = "Pls! Enter User Name";
                }

                else if (string.IsNullOrEmpty(beData.Pwd))
                {
                    dataColl.IsSuccess = false;
                    dataColl.ResponseMSG = "Pls! Enter Password";
                }
                else if (string.IsNullOrEmpty(beData.Link))
                {
                    dataColl.IsSuccess = false;
                    dataColl.ResponseMSG = "Pls! Enter Link";
                }
                else
                {

                    
                    
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/StartOnlineClass", "POST");
                    
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);
                    var responseData = request.Execute<TeacherLogin.Models.Teacher.StartOnlineClass>(beData, keyValues);
                    //  var responseData1 = request.Execute<List<TeacherLogin.Models.Teacher.TeacherProfile>>(dataColl, keyValues);


                    //var responseData = request.Execute<TeacherLogin.Models.Teachers.TeacherProfile>();
                    if (responseData != null)
                    {
                        dataColl = ((TeacherLogin.Models.Teacher.StartOnlineClass)responseData);
                        //   fsds = ((List<TeacherLogin.Models.Teacher.TeacherProfile>)responseData1);

                    }
                }


            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.StartOnlineClass err = new TeacherLogin.Models.Teacher.StartOnlineClass();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion

        #region"GetRunningClasses"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetRunningClassesList()
        {            
            List<TeacherLogin.Models.Teacher.RunningClasses> dataColl = new List<TeacherLogin.Models.Teacher.RunningClasses>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetRunningClasses", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                TeacherLogin.Models.Teacher.RunningClasses data = new TeacherLogin.Models.Teacher.RunningClasses();
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.RunningClasses>>(data, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.RunningClasses>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.RunningClasses err = new TeacherLogin.Models.Teacher.RunningClasses();                
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }

        #endregion

        #region"GetColleRunningClasses"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetColleRunningClasses()
        {
            List<TeacherLogin.Models.Teacher.RunningClasses> dataColl = new List<TeacherLogin.Models.Teacher.RunningClasses>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetColleaguesRunningClasses", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                TeacherLogin.Models.Teacher.RunningClasses data = new TeacherLogin.Models.Teacher.RunningClasses();
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.RunningClasses>>(data, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.RunningClasses>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.RunningClasses err = new TeacherLogin.Models.Teacher.RunningClasses();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }

        #endregion


        #region"EndOnlineClass"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult EndOnlineClass(TeacherLogin.Models.Teacher.RunningClasses Value)
        {
            TeacherLogin.Models.Teacher.RunningClasses data = new TeacherLogin.Models.Teacher.RunningClasses();
          
            try
            {
                                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/EndOnlineClass", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.RunningClasses>(Value, keyValues);


                if (responseData != null)
                {
                    data = ((TeacherLogin.Models.Teacher.RunningClasses)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.RunningClasses err = new TeacherLogin.Models.Teacher.RunningClasses();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = data };

        }
        #endregion


        #region"AddOnlinePlatform"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult AddOnlinePlatform()
        {
            TeacherLogin.Models.Teacher.OnlinePlatForm dataColl = new TeacherLogin.Models.Teacher.OnlinePlatForm();
            // List<TeacherLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = DeserializeObject<TeacherLogin.Models.Teacher.OnlinePlatForm>(Request["jsonData"]);
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/AddOnlinePlatform", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.OnlinePlatForm>(beData, keyValues);
                //  var responseData1 = request.Execute<List<TeacherLogin.Models.Teacher.TeacherProfile>>(dataColl, keyValues);


                //var responseData = request.Execute<TeacherLogin.Models.Teachers.TeacherProfile>();
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.OnlinePlatForm)responseData);
                    //   fsds = ((List<TeacherLogin.Models.Teacher.TeacherProfile>)responseData1);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.OnlinePlatForm err = new TeacherLogin.Models.Teacher.OnlinePlatForm();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }


        #endregion
        #region"GetOnlinePlatform"
        public TeacherLogin.Controllers.JsonNetResult GetOnlinePlatform()
        {
            List<TeacherLogin.Models.Teacher.OnlinePlatForm> dataColl = new List<TeacherLogin.Models.Teacher.OnlinePlatForm>();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl ,"Teacher/GetOnlinePlatform", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.OnlinePlatForm>>(dataColl, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.OnlinePlatForm>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.OnlinePlatForm err = new TeacherLogin.Models.Teacher.OnlinePlatForm();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion
        #region"GetSubjectList"

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetSubjectList(TeacherLogin.Models.StaticValues para)
        {
            TeacherLogin.Models.Subject data = new TeacherLogin.Models.Subject();
            List<TeacherLogin.Models.Subject> dataColl = new List<TeacherLogin.Models.Subject>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetSubjectList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Subject>>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Subject>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };
        }


        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetSubjectColl()
        {
            TeacherLogin.Models.Subject data = new TeacherLogin.Models.Subject();
            List<TeacherLogin.Models.Subject> dataColl = new List<TeacherLogin.Models.Subject>();
            try
            {

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetSubjectList", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Subject>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Subject>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Subject err = new TeacherLogin.Models.Subject();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
        #region"GetClassSchedule"
        public TeacherLogin.Controllers.JsonNetResult GetClassSchedule()
        {
            TeacherLogin.Models.Teacher.ClassSchedule data = new TeacherLogin.Models.Teacher.ClassSchedule();
            List<TeacherLogin.Models.Teacher.ClassSchedule> dataColl = new List<TeacherLogin.Models.Teacher.ClassSchedule>();
            try
            {
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetClassSchedule", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ClassSchedule>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.ClassSchedule>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ClassSchedule err = new TeacherLogin.Models.Teacher.ClassSchedule();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
      
        #region"GetClassShiftLit"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetClassShiftLit()
        {
            List<TeacherLogin.Models.Teacher.ClassSchedule> dataColl = new List<TeacherLogin.Models.Teacher.ClassSchedule>();
            try
            {
                               
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetClassShiftLit", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ClassSchedule>>(dataColl, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.ClassSchedule>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ClassSchedule err = new TeacherLogin.Models.Teacher.ClassSchedule();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
        #region"NotificationLog"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAllNotificationLogList(TeacherLogin.Models.Teacher.NotificationLog Data)
        {

            List<TeacherLogin.Models.Teacher.NotificationLog> dataColl = new List<TeacherLogin.Models.Teacher.NotificationLog>();
            try
            {
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/GetNotificationLog", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);

                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.NotificationLog>>(Data, keyValues);

                //var responseData = request.Execute<TeacherLogin.Modelss.NotificationLog>();
                if (responseData != null)
                {

                    dataColl = ((List<TeacherLogin.Models.Teacher.NotificationLog>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.NotificationLog err = new TeacherLogin.Models.Teacher.NotificationLog();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"Get OnlineClassesList"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetOnlineClassesList(TeacherLogin.Models.Teacher.PassOnlineClasses w)
        {
            List<TeacherLogin.Models.Teacher.PassOnlineClasses> dataColl = new List<TeacherLogin.Models.Teacher.PassOnlineClasses>();
            try
            {
                if (!w.dateFrom.HasValue || w.dateFrom.Value.Year < 2000)
                    w.dateFrom = DateTime.Today;

                if (!w.dateTo.HasValue || w.dateTo.Value.Year < 2000)
                    w.dateTo = DateTime.Today;

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetPassOnlineClasses", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.PassOnlineClasses>>(w, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.PassOnlineClasses>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.PassOnlineClasses err = new TeacherLogin.Models.Teacher.PassOnlineClasses();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetOnlineClassAttById(TeacherLogin.Models.Teacher.PassOnlineClasses w)
        {
            TeacherLogin.Models.Teacher.ResOnlineClassAttById dataColl = new TeacherLogin.Models.Teacher.ResOnlineClassAttById();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetOnlineClassAttById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.ResOnlineClassAttById>(w, keyValues);


                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.ResOnlineClassAttById)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.PassOnlineClasses err = new TeacherLogin.Models.Teacher.PassOnlineClasses();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
        #region"UserDetail"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetUserDetail()
        {
            TeacherLogin.Models.UserDetail dataColl = new Models.UserDetail();
            // List<TeacherLogin.Models.Teacher.UserDetail> fsds = new List<Models.Teacher.UserDetail>();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetUserDetail", "GET");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.UserDetail>(keyValues);
                //  var responseData1 = request.Execute<List<TeacherLogin.Models.Teacher.UserDetail>>(dataColl, keyValues);


                //var responseData = request.Execute<TeacherLogin.Models.Teachers.UserDetail>();
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.UserDetail)responseData);
                    //   fsds = ((List<TeacherLogin.Models.Teacher.UserDetail>)responseData1);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.UserDetail err = new TeacherLogin.Models.UserDetail();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"Missedclass"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetMissedClassList(TeacherLogin.Models.Teacher.PassOnlineClasses w)
        {
            TeacherLogin.Models.Teacher.PassOnlineClasses dataColl = new TeacherLogin.Models.Teacher.PassOnlineClasses();
            try
            {
                if (!w.dateFrom.HasValue || w.dateFrom.Value.Year < 2000)
                    w.dateFrom = DateTime.Today;

                if (!w.dateTo.HasValue || w.dateTo.Value.Year < 2000)
                    w.dateTo = DateTime.Today;

                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetMissedClassList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.PassOnlineClasses>(w, keyValues);


                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.PassOnlineClasses)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.PassOnlineClasses err = new TeacherLogin.Models.Teacher.PassOnlineClasses();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion
        public ActionResult OnlineClassTry()
        {
            return View();
        }
    }
}