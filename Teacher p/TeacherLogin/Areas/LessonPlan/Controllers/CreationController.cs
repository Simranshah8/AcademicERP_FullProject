using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace TeacherLogin.Areas.LessonPlan.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: LessonPlan/Creation

        public ActionResult MidasLMS()
        {
            
            return View();
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetMidasURL()
        {
            TeacherLogin.Models.Responce resVal = new Models.Responce();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetMidasURL", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var newPara = new { };

                var responseData = request.Execute<TeacherLogin.Models.Responce>(newPara, keyValues);
                if (responseData != null)
                {
                    resVal = (TeacherLogin.Models.Responce)responseData;
                }
            }
            catch (Exception ee)
            {

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 1 };
        }

        public ActionResult LMS()
        {
            return View();
        }
        public ActionResult LessonPlan()
        {
            return View();
        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetLessonPlanByClassSubject(int ClassId, int SubjectId)
        {
            TeacherLogin.Models.Academic.LessonPlan retData = new Models.Academic.LessonPlan();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetLessonPlanByClassSubject", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    classId = ClassId,
                    subjectId = SubjectId
                };

                var responseData = request.Execute<TeacherLogin.Models.Academic.LessonPlan>(para, keyValues);
                if (responseData != null)
                {
                    retData = (TeacherLogin.Models.Academic.LessonPlan)responseData;
                }
            }
            catch(Exception ee)
            {
                retData.ResponseMSG = ee.Message;
                retData.IsSuccess = false;
            }
            
            return new TeacherLogin.Controllers.JsonNetResult() { Data = retData, TotalCount = 1 };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetLessonPlanByClass(int ClassId, int? SectionId)
        {
            List<TeacherLogin.Models.Academic.LessonPlan> retData = new List<Models.Academic.LessonPlan>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetLessonPlanByClass", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    classId = ClassId,
                    sectionId = SectionId
                };

                var responseData = request.Execute<List<TeacherLogin.Models.Academic.LessonPlan>>(para, keyValues);
                if (responseData != null)
                {
                    retData = (List<TeacherLogin.Models.Academic.LessonPlan>)responseData;
                }
            }
            catch (Exception ee)
            {
                //retData.ResponseMSG = ee.Message;
                //retData.IsSuccess = false;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = retData, TotalCount = 1 };
        }

        [HttpPost, ValidateInput(false)]
        public TeacherLogin.Controllers.JsonNetResult SaveLessonPlan()
        {
            TeacherLogin.Models.Responce Val = new TeacherLogin.Models.Responce(); 
            try
            {
                var jsonbeData = Request["jsonData"];
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");                   
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddLessionPlan", "POST");

                    try
                    {
                        string url = BaseUrl + "Teacher/AddLessionPlan";
                        var method = new HttpMethod("POST");
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("ContentType", "JSON");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                        MultipartFormDataContent form = new MultipartFormDataContent();

                        HttpContent content1 = new StringContent(jsonbeData);
                        form.Add(content1, "paraDataColl");
                        if (Request.Files.Count>0)
                        {
                            var file1 = Request.Files[0];
                            content1 = new StreamContent(file1.InputStream); 
                            content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                FileName = file1.FileName
                            };
                            form.Add(content1);
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
                           Val.ResponseMSG="API is Not Working currently <br/>";
                        }
                    }
                    catch (Exception ex1)
                    {
                        Val.ResponseMSG= "Some error occur.  <br/>";
                    } 
                }
            }
            catch (Exception ee)
            {
                Val.ResponseMSG = ee.Message;
                Val.IsSuccess = false;

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Val };

        }
   

        [HttpPost, ValidateInput(false)]
        public TeacherLogin.Controllers.JsonNetResult UpdateLessonPlanDate()
        {
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {
                var beData = DeserializeObject<TeacherLogin.Models.Academic.LessonPlan>(Request["jsonData"]);
                if (beData != null)
                {
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/UpdateLessionPlanDate", "POST");
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);
                  
                    var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                    if (responseData != null)
                    {
                        resVal = (TeacherLogin.Models.Responce)responseData;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetTodayLessonPlan(DateTime? forDate, int? classId, int? sectionId, int? subjectId,int reportType)
        {
            List<TeacherLogin.Models.Academic.TodayLessonPlan> retData = new List<Models.Academic.TodayLessonPlan>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetTodayLessonPlan", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    forDate = forDate,
                    classId = classId,
                    sectionId = sectionId,
                    subjectId=subjectId,
                    reportType=reportType,
                    employeeId=0
                };

                var responseData = request.Execute<List<TeacherLogin.Models.Academic.TodayLessonPlan>>(para, keyValues);
                if (responseData != null)
                {
                    retData = (List<TeacherLogin.Models.Academic.TodayLessonPlan>)responseData;
                }
            }
            catch (Exception ee)
            {

            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = retData, TotalCount = 1 };
        }

        public ActionResult UpdateLessonPlan()
        {
            return View();
        }
        public ActionResult TodaysClass()
        {
            return View();
        }
        public ActionResult SyllabusStatus()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveLessonTeacherContent()
        {
              
            TeacherLogin.Models.Responce Val = new TeacherLogin.Models.Responce();
            try
            {
                var jsonbeData = Request["jsonData"];
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddLessonTeacherContent", "POST");

                    try
                    {
                        string url = BaseUrl + "Teacher/AddLessonTeacherContent";
                        var method = new HttpMethod("POST");
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("ContentType", "JSON");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                        MultipartFormDataContent form = new MultipartFormDataContent();

                        HttpContent content1 = new StringContent(jsonbeData);
                        form.Add(content1, "paraDataColl");
                        if (Request.Files.Count > 0)
                        {
                            int fileCount = Request.Files.Count;
                            for(int f = 0; f < fileCount; f++)
                            {
                                var file1 = Request.Files[f];
                                content1 = new StreamContent(file1.InputStream);
                                content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = file1.FileName
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
                            Val.ResponseMSG = "API is Not Working currently <br/>";
                        }
                    }
                    catch (Exception ex1)
                    {
                        Val.ResponseMSG = "Some error occur.  <br/>";
                    }
                }
            }
            catch (Exception ee)
            {
                Val.ResponseMSG = ee.Message;
                Val.IsSuccess = false;

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Val };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetLessonTeacherContent(int LessonId, int LessonSNo)
        {
            List<TeacherLogin.Models.Academic.LessonTopicTeacherContent> retData = new List<Models.Academic.LessonTopicTeacherContent>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetLessonTeacherContent", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    forDate = LessonId,
                    classId = LessonSNo 
                };

                var responseData = request.Execute<List<TeacherLogin.Models.Academic.LessonTopicTeacherContent>>(para, keyValues);
                if (responseData != null)
                {
                    retData = (List<TeacherLogin.Models.Academic.LessonTopicTeacherContent>)responseData;
                }
            }
            catch (Exception ee)
            {

            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = retData, TotalCount = 1 };
        
        }


        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveLessonTopicTeacherContent()
        {

            TeacherLogin.Models.Responce Val = new TeacherLogin.Models.Responce();
            try
            {
                var jsonbeData = Request["jsonData"];
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddLessonTopicTeacherContent", "POST");

                    try
                    {
                        string url = BaseUrl + "Teacher/AddLessonTopicTeacherContent";
                        var method = new HttpMethod("POST");
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("ContentType", "JSON");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                        MultipartFormDataContent form = new MultipartFormDataContent();

                        HttpContent content1 = new StringContent(jsonbeData);
                        form.Add(content1, "paraDataColl");
                        if (Request.Files.Count > 0)
                        {
                            int fileCount = Request.Files.Count;
                            for (int f = 0; f < fileCount; f++)
                            {
                                var file1 = Request.Files[f];
                                content1 = new StreamContent(file1.InputStream);
                                content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = file1.FileName
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
                            Val.ResponseMSG = "API is Not Working currently <br/>";
                        }
                    }
                    catch (Exception ex1)
                    {
                        Val.ResponseMSG = "Some error occur.  <br/>";
                    }
                }
            }
            catch (Exception ee)
            {
                Val.ResponseMSG = ee.Message;
                Val.IsSuccess = false;

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Val };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetLessonTopicTeacherContent(int LessonId, int LessonSNo, int TopicSNo)
        {
            List<TeacherLogin.Models.Academic.LessonTopicTeacherContent> retData = new List<Models.Academic.LessonTopicTeacherContent>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetLessonTopicTeacherContent", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    lessonId = LessonId,
                    lessonSNo = LessonSNo,
                    topicSNo= TopicSNo
                };

                var responseData = request.Execute<List<TeacherLogin.Models.Academic.LessonTopicTeacherContent>>(para, keyValues);
                if (responseData != null)
                {
                    retData = (List<TeacherLogin.Models.Academic.LessonTopicTeacherContent>)responseData;
                }
            }
            catch (Exception ee)
            { 

            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = retData, TotalCount = 1 };
        }

        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveLessonTopicContent()
        {
            TeacherLogin.Models.Responce Val = new TeacherLogin.Models.Responce();
            try
            {
                var jsonbeData = Request["jsonData"];
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddLessonTopicContent", "POST");

                    try
                    {
                        string url = BaseUrl + "Teacher/AddLessonTopicContent";
                        var method = new HttpMethod("POST");
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("ContentType", "JSON");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                        MultipartFormDataContent form = new MultipartFormDataContent();

                        HttpContent content1 = new StringContent(jsonbeData);
                        form.Add(content1, "paraDataColl");
                        if (Request.Files.Count > 0)
                        {
                            int fileCount = Request.Files.Count;
                            for (int f = 0; f < fileCount; f++)
                            {
                                var file1 = Request.Files[f];
                                content1 = new StreamContent(file1.InputStream);
                                content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = file1.FileName
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
                            Val.ResponseMSG = "API is Not Working currently <br/>";
                        }
                    }
                    catch (Exception ex1)
                    {
                        Val.ResponseMSG = "Some error occur.  <br/>";
                    }
                }
            }
            catch (Exception ee)
            {
                Val.ResponseMSG = ee.Message;
                Val.IsSuccess = false;

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Val };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetLessonTopicContent(int LessonId, int LessonSNo, int TopicSNo)
        {
            List<TeacherLogin.Models.Academic.LessonTopicContent> retData = new List<Models.Academic.LessonTopicContent>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetLessonTopicContent", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    lessonId = LessonId,
                    lessonSNo = LessonSNo,
                    topicSNo = TopicSNo
                };

                var responseData = request.Execute<List<TeacherLogin.Models.Academic.LessonTopicContent>>(para, keyValues);
                if (responseData != null)
                {
                    retData = (List<TeacherLogin.Models.Academic.LessonTopicContent>)responseData;
                }
            }
            catch (Exception ee)
            {

            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = retData, TotalCount = 1 };
        }

        [HttpPost, ValidateInput(false)]
        public TeacherLogin.Controllers.JsonNetResult SaveLessonTopicVideo()
        {
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {
                var beData = DeserializeObject<List<TeacherLogin.Models.Academic.LessonTopicVideo>>(Request["jsonData"]);
                if (beData != null)
                {
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddLessonTopicVideo", "POST");
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);

                    var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                    if (responseData != null)
                    {
                        resVal = (TeacherLogin.Models.Responce)responseData;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetLessonTopicVideo(int LessonId, int LessonSNo, int TopicSNo)
        {
            List<TeacherLogin.Models.Academic.LessonTopicVideo> retData = new List<Models.Academic.LessonTopicVideo>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetLessonTopicVideo", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    lessonId = LessonId,
                    lessonSNo = LessonSNo,
                    topicSNo = TopicSNo
                };

                var responseData = request.Execute<List<TeacherLogin.Models.Academic.LessonTopicVideo>>(para, keyValues);
                if (responseData != null)
                {
                    retData = (List<TeacherLogin.Models.Academic.LessonTopicVideo>)responseData;
                }
            }
            catch (Exception ee)
            {

            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = retData, TotalCount = 1 }; 
        }

        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SaveLessonTopicQuiz()
        {
            string photoLocation = "/Attachments/lms";
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {

                var beData = DeserializeObject<TeacherLogin.Models.Academic.LessonTopicQuiz>(Request["jsonData"]);
                if (beData != null)
                {
                    //if (Request.Files.Count > 0)
                    //{
                    //    var filesColl = Request.Files;
                    //    foreach (var que in beData.QuestionColl)
                    //    {
                    //        HttpPostedFileBase file = filesColl["file-q" + que.SNo];
                    //        if (file != null)
                    //        {
                    //            var att = GetAttachmentDocuments(photoLocation, file);
                    //            que.ContentPath = att.DocPath;
                    //        }

                    //        foreach (var ans in que.AnswerColl)
                    //        {
                    //            HttpPostedFileBase file1 = filesColl["file-q" + que.SNo + "-a" + ans.SNo];
                    //            if (file1 != null)
                    //            {
                    //                var att = GetAttachmentDocuments(photoLocation, file1);
                    //                ans.ContentPath = att.DocPath;
                    //            }
                    //        }
                    //    }
                    //}
                    //resVal = new AcademicLib.BL.Academic.Transaction.LessonPlan(User.UserId, User.HostName, User.DBName).SaveLessonTopicQuiz(beData);

                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 };
        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetLessonTopicQuiz(int LessonId, int LessonSNo, int TopicSNo)
        {
            TeacherLogin.Models.Academic.LessonTopicQuiz retData = new Models.Academic.LessonTopicQuiz();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetLessonTopicQuiz", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    lessonId = LessonId,
                    lessonSNo = LessonSNo,
                    topicSNo = TopicSNo
                };

                var responseData = request.Execute<TeacherLogin.Models.Academic.LessonTopicQuiz>(para, keyValues);
                if (responseData != null)
                {
                    retData = (TeacherLogin.Models.Academic.LessonTopicQuiz)responseData;
                }
            }
            catch (Exception ee)
            {

            }
            return new TeacherLogin.Controllers.JsonNetResult() { Data = retData, TotalCount = 1 };
             
        }


        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult StartLesson()
        {
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {
                var beData = DeserializeObject<TeacherLogin.Models.Academic.LessonPlanDetails>(Request["jsonData"]);
                if (beData != null)
                {
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/StartLesson", "POST");
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);

                    var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                    if (responseData != null)
                    {
                        resVal = (TeacherLogin.Models.Responce)responseData;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 };

           
        }

        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult EndLesson()
        {
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {
                var beData = DeserializeObject<TeacherLogin.Models.Academic.LessonPlanDetails>(Request["jsonData"]);
                if (beData != null)
                {
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/EndLesson", "POST");
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);

                    var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                    if (responseData != null)
                    {
                        resVal = (TeacherLogin.Models.Responce)responseData;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 };
        }


        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult StartTopic()
        {
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {
                var beData = DeserializeObject<TeacherLogin.Models.Academic.LessonTopic>(Request["jsonData"]);
                if (beData != null)
                {
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/StartTopic", "POST");
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);

                    var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                    if (responseData != null)
                    {
                        resVal = (TeacherLogin.Models.Responce)responseData;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 };
        }

        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult EndTopic()
        {
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {
                var beData = DeserializeObject<TeacherLogin.Models.Academic.LessonTopic>(Request["jsonData"]);
                if (beData != null)
                {
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/EndTopic", "POST");
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);

                    var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                    if (responseData != null)
                    {
                        resVal = (TeacherLogin.Models.Responce)responseData;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 };
        }



        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult StartTopicContent()
        {
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {
                var beData = DeserializeObject<TeacherLogin.Models.Academic.LessonTopicTeacherContent>(Request["jsonData"]);
                if (beData != null)
                {
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/StartTopicContent", "POST");
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);

                    var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                    if (responseData != null)
                    {
                        resVal = (TeacherLogin.Models.Responce)responseData;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 }; 
       
        }

        [ValidateInput(false)]
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult EndTopicContent()
        {
            TeacherLogin.Models.Responce resVal = new TeacherLogin.Models.Responce();
            try
            {
                var beData = DeserializeObject<TeacherLogin.Models.Academic.LessonTopicTeacherContent>(Request["jsonData"]);
                if (beData != null)
                {
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/EndTopicContent", "POST");
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("Bearer", User.access_token);

                    var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                    if (responseData != null)
                    {
                        resVal = (TeacherLogin.Models.Responce)responseData;
                    }
                }
                else
                {
                    resVal.ResponseMSG = "Blank Data Can't be Accept";
                }

            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 0 };
        }

    }
}