using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.Notice.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: Notice/Creation
        public ActionResult Notice()
        {
            return View();
        }

        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetAllNoticeList()
        {
            List<TeacherLogin.Models.Teacher.NotificationLog> dataColl = new List<TeacherLogin.Models.Teacher.NotificationLog>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetNotificationLog", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    dateFrom = DateTime.Today
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.NotificationLog>>(para, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.NotificationLog>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.RemarkType err = new TeacherLogin.Models.RemarkType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetTopNoticeList()
        {
            List<TeacherLogin.Models.Teacher.NotificationLog> dataColl = new List<TeacherLogin.Models.Teacher.NotificationLog>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetTopNotificationLog", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para =new
                {
                    ForDate=DateTime.Today
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.NotificationLog>>(para,keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.NotificationLog>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.RemarkType err = new TeacherLogin.Models.RemarkType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }


        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GeNoticeCount()
        {
            TeacherLogin.Models.Teacher.NotificationCount dataColl = new Models.Teacher.NotificationCount();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/NotificationCount", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    ForDate = DateTime.Today
                };
                var responseData = request.Execute<TeacherLogin.Models.Teacher.NotificationCount>(para, keyValues);
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.NotificationCount)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.RemarkType err = new TeacherLogin.Models.RemarkType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }


        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetUpcomingEventHoliday()
        {
            List<TeacherLogin.Models.Teacher.EventHoliday> dataColl = new List<TeacherLogin.Models.Teacher.EventHoliday>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetEventHoliday", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    ForDate = DateTime.Today
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.EventHoliday>>(para, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.EventHoliday>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.RemarkType err = new TeacherLogin.Models.RemarkType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
    }
}