using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TeacherLogin.Areas.StudentRecord.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: StudentRecord/Creation
        public ActionResult StudentRecord()
        {
            return View();
        }

        #region"Section ANd Class Coll"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetClassSection()
        {

            TeacherLogin.Models.Responsemsg dataColl = new TeacherLogin.Models.Responsemsg();

            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetClassSectionList", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responsemsg>(dataColl, keyValues);

                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Responsemsg)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ClassWiseAttendance err = new TeacherLogin.Models.Teacher.ClassWiseAttendance();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };


        }

        #endregion
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult AddRemarksToStudent()
        {

             Models.Responce Res = new Models.Responce();
            Models.Teacher.Remarks dataColl = new  Models.Teacher.Remarks();
            // List<TeacherLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Teacher.Remarks>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");

                    if (Request.Files.Count > 0)
                        beData.file1= Request.Files["file1"];
                     Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/AddRemarksToStudent", "POST");
                
                    try
                    {
                        string url = BaseUrl + "Teacher/AddRemarksToStudent";
                        var method = new HttpMethod("POST");
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("ContentType", "JSON");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                        MultipartFormDataContent form = new MultipartFormDataContent();

                        HttpContent content1 = new StringContent(jsonbeData);
                        form.Add(content1, "paraDataColl");

                        if (beData.file1 != null)
                        {
                            content1 = new StreamContent(beData.file1.InputStream);
                            content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") //  //form-data
                            {
                                FileName = beData.file1.FileName,
                                Name = "question"
                            };
                            form.Add(content1);
                        }
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
                            Response.Write("API is Not Working currently <br/>");
                        }
                    }
                    catch (Exception ex1)
                    {
                        Response.Write("Some error occur.  <br/>");
                    }



                }
            }
            catch (Exception ee)
            {
                Models.Teacher.Remarks err = new Models.Teacher.Remarks();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Res };

        }
          [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult SendNoticeToStudent()
        {

             Models.Responce Res = new Models.Responce();
            Models.Teacher.Notice dataColl = new  Models.Teacher.Notice();
            // List<TeacherLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Teacher.Notice>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");

                    if (Request.Files.Count > 0)
                        beData.file1= Request.Files["file1"];
                     Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/SendNoticeToStudent", "POST");
                     

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

                        if (beData.file1 != null)
                        {
                            content1 = new StreamContent(beData.file1.InputStream);
                            content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") //  //form-data
                            {
                                FileName = beData.file1.FileName
                            };
                            form.Add(content1);
                        }
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
                            Response.Write("API is Not Working currently <br/>");
                        }
                    }
                    catch (Exception ex1)
                    {
                        Response.Write("Some error occur.  <br/>");
                    }



                }
            }
            catch (Exception ee)
            {
                Models.Teacher.Notice err = new Models.Teacher.Notice();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Res };

        }


        #region"GetRemarksTypeList"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetRemarksTypeList()
        {
            List<TeacherLogin.Models.RemarkType> dataColl = new List<TeacherLogin.Models.RemarkType>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetRemarksTypeList", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.RemarkType>>(dataColl, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.RemarkType>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.RemarkType err = new TeacherLogin.Models.RemarkType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion


        #region"GetClassWiseStudentList"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetClassWiseStudentList(TeacherLogin.Models.Teacher.StudentRecord Data)
        {
            List<TeacherLogin.Models.Teacher.StudentRecord> dataColl = new List<TeacherLogin.Models.Teacher.StudentRecord>();
            try
            {
                
                if(!string.IsNullOrEmpty(Data.sectionId) && string.IsNullOrEmpty(Data.sectionIdColl))
                {
                    Data.sectionIdColl = Data.sectionId;
                }
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetClassWiseStudentList", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    classId=Data.classId,
                    sectionIdColl=Data.sectionIdColl
                };
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.StudentRecord>>(para, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.StudentRecord>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.StudentRecord err = new TeacherLogin.Models.Teacher.StudentRecord();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
    }
}