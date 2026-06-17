using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherLogin.Areas.Timetable.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: Timetable/Creation
        public ActionResult Timetable()
        {
            return View();
        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetExamSchedule(TeacherLogin.Models.Teacher.Val DataVal)
        {
             
            List<TeacherLogin.Models.Teacher.ExamShedule> dataColl = new List<TeacherLogin.Models.Teacher.ExamShedule>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetExamSchedule", "POST");
                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ExamShedule>>(DataVal, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.ExamShedule>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ExamShedule err = new TeacherLogin.Models.Teacher.ExamShedule();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #region"GetClassSchedule"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetClassSchedule(TeacherLogin.Models.Teacher.ClassScheduleId res)
        {
            List<TeacherLogin.Models.Teacher.ClassSchedule> dataColl = new List<TeacherLogin.Models.Teacher.ClassSchedule>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetClassSchedule", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ClassSchedule>>(res, keyValues);


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

    }
}