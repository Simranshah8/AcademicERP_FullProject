using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TeacherLogin.Areas.OnlineExam.Controllers
{
    public class ReportingController : TeacherLogin.Controllers.BaseController
    {
        // GET: OnlineExam/Reporting
        public ActionResult Evaluate()
        {
            ViewBag.WEBURLPATH = WebUrl;
            return View();
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetOnlineExamListForEvaluate(DateTime DateFrom,DateTime DateTo,int ExamTypeId,int ClassId,int? SectionId,int SubjectId)
        {
            List<TeacherLogin.Models.Teacher.ExamList> dataColl = new List<Models.Teacher.ExamList>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetOnlineExamListForEvaluate", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    dateFrom = DateFrom,
                    dateTo=DateTo,
                    examTypeId=ExamTypeId,
                    classId=ClassId,
                    sectionId=SectionId,
                    subjectId=SubjectId
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ExamList>>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.ExamList>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentForEvaluate(int examSetupId,int classId,int? sectionId)
        {
            List<TeacherLogin.Models.Teacher.StudentForEvaluate> dataColl = new List<Models.Teacher.StudentForEvaluate>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetStudentForEvaluatet", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);

                if (!sectionId.HasValue)
                    sectionId = 0;

                var para = new
                {
                    examSetupId = examSetupId,
                    classId=classId,
                    sectionId=sectionId
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.StudentForEvaluate>>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.StudentForEvaluate>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SendNotificationToAbsent()
        {
            Models.Responce Res = new Models.Responce();            
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Teacher.Notice>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                try
                {
                    string url = BaseUrl + "Teacher/SendNoticeToStudent";
                    var method = new HttpMethod("POST");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("ContentType", "JSON");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    HttpContent content1 = new StringContent(jsonbeData);
                    form.Add(content1, "paraDataColl");
                    var response = (client.PostAsync(url, form)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var resStr = response.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(resStr))
                        {
                            var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Responce>(resStr);
                            Res.ResponseMSG = resResult.ResponseMSG;
                            Res.IsSuccess = resResult.IsSuccess;
                        }

                    }
                    else
                    {
                        Res.ResponseMSG = response.ToString() ;
                        Res.IsSuccess =false;
                    }
                }
                catch (Exception ex1)
                {
                    Res.ResponseMSG = ex1.Message;
                    Res.IsSuccess = false;
                }
            }
            catch (Exception ee)
            {
                Res.ResponseMSG = ee.Message;
                Res.IsSuccess = false;

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Res };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetStudentAnswerListById(int examSetupId,int studentId)
        {

            List<TeacherLogin.Models.Teacher.QuestionSetup> dataColl = new List<Models.Teacher.QuestionSetup>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetOnlineExamAnswer", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    examSetupId = examSetupId,
                    studentId=studentId
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.QuestionSetup>>(para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.QuestionSetup>)responseData);

                }
            }
            catch (Exception ee)
            {

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult ExamCopyCheck()
        {
            TeacherLogin.Models.Responce Val = new TeacherLogin.Models.Responce();
            TeacherLogin.Models.Teacher.HomeWork dataColl = new TeacherLogin.Models.Teacher.HomeWork();

            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };

                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherLogin.Models.Teacher.ExamCopyChecked>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = Request["jsonData"];
                beData.SelectedFiles = Request.Files;
                
                try
                {
                    string url = BaseUrl + "Teacher/CheckExamCopy";
                    var method = new HttpMethod("POST");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("ContentType", "JSON");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                    MultipartFormDataContent form = new MultipartFormDataContent();

                    HttpContent content1 = new StringContent(jsonbeData);
                    form.Add(content1, "paraDataColl");
                    if (beData.SelectedFiles != null)
                    {
                        for (int i = 0; i < beData.SelectedFiles.Count; i++)
                        {
                            var file1 = beData.SelectedFiles[i];
                            content1 = new StreamContent(file1.InputStream);
                            content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                FileName = beData.FilesColl[i]
                            };
                            form.Add(content1);
                        }

                    }
                    var response = (client.PostAsync(url, form)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var resStr = response.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(resStr))
                        {
                            var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherLogin.Models.Responce>(resStr);
                            Val.ResponseMSG = resResult.ResponseMSG;
                            Val.IsSuccess = resResult.IsSuccess;
                        }
                    }
                    else
                    {
                        Response.Write(response.ReasonPhrase);
                    }
                }
                catch (Exception ex1)
                {
                    Response.Write("Some error occur.  <br/>" + ex1.Message);
                }
            }
            catch (Exception ee)
            {
                Response.Write("Some error occur.  <br/>" + ee.Message);
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Val };

        }

        public ActionResult ExamStatus()
        {
            ViewBag.WEBURLPATH = WebUrl;
            return View();
        }

        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetOnlineExamList()
        {            
            List<TeacherLogin.Models.Teacher.ExamList> dataColl = new List<Models.Teacher.ExamList>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetOnlineExamList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    forType=0
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ExamList>>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.ExamList>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetOnlineExamDetById(int examSetupId)
        {

            TeacherLogin.Models.Teacher.OnlineExamList dataColl = new TeacherLogin.Models.Teacher.OnlineExamList();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetOnlineExamDetById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    examSetupId=examSetupId
                };
                var responseData = request.Execute<TeacherLogin.Models.Teacher.OnlineExamList>(para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.OnlineExamList)responseData);

                }
            }
            catch (Exception ee)
            {
                
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetQuestionListById(int examSetupId)
        {

            List<TeacherLogin.Models.Teacher.QuestionSetup> dataColl = new List<Models.Teacher.QuestionSetup>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetQuestionList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    examSetupId = examSetupId
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.QuestionSetup>>(para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.QuestionSetup>)responseData);

                }
            }
            catch (Exception ee)
            {

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
    }
}