using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace StudentLogin.Areas.Student.Controllers
{
    public class CreationController : StudentLogin.Controllers.BaseController
    {
        // GET: Student/Creation

        public ActionResult MidasLMS()
        {

            return View();
        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetMidasURL()
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "General/GetMidasURL", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var newPara = new { };

                var responseData = request.Execute<Models.Students.Reponce>(newPara, keyValues);
                if (responseData != null)
                {
                    resVal = (Models.Students.Reponce)responseData;
                }
            }
            catch (Exception ee)
            {

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal, TotalCount = 1 };
        }


        public ActionResult LiveClass()
        {
            return View();
        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetOnlineClassesList(StudentLogin.Models.Students.PassOnlineClassesVal w)
        {            
            List<StudentLogin.Models.Students.PassOnlineClasses> dataColl = new List<StudentLogin.Models.Students.PassOnlineClasses>();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/GetPassOnlineClasses", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.PassOnlineClasses>>(w, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.PassOnlineClasses>)responseData);                   
                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.PassOnlineClasses err = new StudentLogin.Models.Students.PassOnlineClasses();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #region"GetClassSchedule"
        public StudentLogin.Controllers.JsonNetResult GetClassSchedule()
        {
            StudentLogin.Models.ClassSchedule data = new StudentLogin.Models.ClassSchedule();
            List<StudentLogin.Models.ClassSchedule> dataColl = new List<StudentLogin.Models.ClassSchedule>();
            try
            {
                
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/GetClassSchedule", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.ClassSchedule>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.ClassSchedule>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.ClassSchedule err = new StudentLogin.Models.ClassSchedule();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"GetRunningClasses"
        [HttpGet]
        public StudentLogin.Controllers.JsonNetResult GetRunningClassesList()
        {
            StudentLogin.Models.Students.RunningClasses data = new StudentLogin.Models.Students.RunningClasses();
            List<StudentLogin.Models.Students.RunningClasses> dataColl = new List<StudentLogin.Models.Students.RunningClasses>();
            try
            {
                
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/GetRunningClasses", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.RunningClasses>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.RunningClasses>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.RunningClasses err = new StudentLogin.Models.Students.RunningClasses();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"JoinOnlineClass"
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult JoinOnlineClass(StudentLogin.Models.Students.RunningClasses w)
        {
            StudentLogin.Models.Students.RunningClasses data = new StudentLogin.Models.Students.RunningClasses();

            try
            {
               
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/JoinOnlineClass", "POST");
               Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    RId = w.TranId
                };
                var responseData = request.Execute<StudentLogin.Models.Students.RunningClasses>(para, keyValues);


                if (responseData != null)
                {
                    data = ((StudentLogin.Models.Students.RunningClasses)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.RunningClasses err = new StudentLogin.Models.Students.RunningClasses();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = data };

        }
        #endregion


        public ActionResult MyProfile()
        {
            ViewBag.WebURL = WebUrl;
            return View();
        }

        #region"StudentProfile"
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetAllProfile()
        {
            StudentLogin.Models.Students.StudentProfile dataColl = new Models.Students.StudentProfile();
            try
            {
                
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/GetStudentProfile", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<StudentLogin.Models.Students.StudentProfile>(dataColl, keyValues);
                if (responseData != null)
                {
                    dataColl = ((StudentLogin.Models.Students.StudentProfile)responseData);
                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.StudentProfile err = new StudentLogin.Models.Students.StudentProfile();
            }
            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };
        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetAllCaste()
        {
            List<StudentLogin.Models.Academic.Caste> dataColl = new List<Models.Academic.Caste>();

            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Academic/GetCasteList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Academic.Caste>>(dataColl, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Academic.Caste>)responseData);
                }
            }
            catch (Exception ee)
            {

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdatePersonalInfo(StudentLogin.Models.Students.PersonalInfo beData)
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/UpdatePersonalInfo", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<Models.Students.Reponce>(beData, keyValues);
                if (responseData != null)
                {
                    resVal = ((Models.Students.Reponce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };

        }


        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdateContactInfo(StudentLogin.Models.Students.ContactInfo beData)
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/UpdateContactInfo", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<Models.Students.Reponce>(beData, keyValues);
                if (responseData != null)
                {
                    resVal = ((Models.Students.Reponce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };

        }



        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdateParentDetails(StudentLogin.Models.Students.ParentDetails beData)
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/UpdateParentDetails", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<Models.Students.Reponce>(beData, keyValues);
                if (responseData != null)
                {
                    resVal = ((Models.Students.Reponce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };

        }


        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdateTemporaryAddress(StudentLogin.Models.Students.TemporaryAddress beData)
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/UpdateTemporaryAddress", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<Models.Students.Reponce>(beData, keyValues);
                if (responseData != null)
                {
                    resVal = ((Models.Students.Reponce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };
        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdatePermanentAddress(StudentLogin.Models.Students.PermanentAddress beData)
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/UpdatePermanentAddress", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<Models.Students.Reponce>(beData, keyValues);
                if (responseData != null)
                {
                    resVal = ((Models.Students.Reponce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };
        }


        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdateGuardianDetails(StudentLogin.Models.Students.GuardianDetails beData)
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/UpdateGuardianDetails", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<Models.Students.Reponce>(beData, keyValues);
                if (responseData != null)
                {
                    resVal = ((Models.Students.Reponce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };
        }


        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdatePreviousSchool(List<StudentLogin.Models.Students.StudentPreviousAcademicDetails> beData)
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/UpdatePreviousSchool", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<Models.Students.Reponce>(beData, keyValues);
                if (responseData != null)
                {
                    resVal = ((Models.Students.Reponce)responseData);
                }
            }
            catch (Exception ee)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ee.Message;
            }
            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };
        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdatePhoto()
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                HttpPostedFileBase file1=null;
                if (Request.Files.Count > 0)
                {
                    file1= Request.Files[0];                    
                }

                string url = BaseUrl + "Student/UpdatePhoto";
                var method = new HttpMethod("POST");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("ContentType", "JSON");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                MultipartFormDataContent form = new MultipartFormDataContent();

                if (file1 != null)
                {

                    HttpContent content1 = new StreamContent(file1.InputStream);
                    content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        FileName = file1.FileName
                    }; 
                    form.Add(content1);
                    var response = (client.PostAsync(url, form)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resStr = response.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(resStr))
                        {
                            resVal = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Students.Reponce>(resStr);
                        }
                    }
                    else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "API is Not Working currently";
                    }
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Pls Select Valid Photo";
                }

            }
            catch (Exception ex1)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex1.Message;
            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };

        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult UpdateSignature()
        {
            Models.Students.Reponce resVal = new Models.Students.Reponce();
            try
            {
                HttpPostedFileBase file1 = null;
                if (Request.Files.Count > 0)
                {
                    file1 = Request.Files[0];
                }

                string url = BaseUrl + "Student/UpdateSignature";
                var method = new HttpMethod("POST");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("ContentType", "JSON");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                MultipartFormDataContent form = new MultipartFormDataContent();

                if (file1 != null)
                {

                    HttpContent content1 = new StreamContent(file1.InputStream);
                    content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        FileName = file1.FileName
                    };
                    form.Add(content1);
                    var response = (client.PostAsync(url, form)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resStr = response.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(resStr))
                        {
                            resVal = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Students.Reponce>(resStr);
                        }
                    }
                    else
                    {
                        resVal.IsSuccess = false;
                        resVal.ResponseMSG = "API is Not Working currently";
                    }
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Pls Select Valid Photo";
                }

            }
            catch (Exception ex1)
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = ex1.Message;
            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = resVal };

        }

        #endregion

        #region"Assignment Crud"

        public ActionResult Assignment()
        {
            return View();
        }
        // for Select Dropdown
        public StudentLogin.Controllers.JsonNetResult GetAssignmentTypeList()
        {
            StudentLogin.Models.AssignmentType data = new StudentLogin.Models.AssignmentType();
            List<StudentLogin.Models.AssignmentType> dataColl = new List<StudentLogin.Models.AssignmentType>();
            try
            {

                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetAssignmentTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.AssignmentType>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.AssignmentType>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.AssignmentType err = new StudentLogin.Models.AssignmentType();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        // get All Home Wosk List
        public StudentLogin.Controllers.JsonNetResult GetAllAssignmentList()
        {
            Models.Students.AssignmentVal Val = new Models.Students.AssignmentVal();
            List<StudentLogin.Models.Students.Assignment> dataColl = new List<StudentLogin.Models.Students.Assignment>();
            try
            {

                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/GetAssignmentList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.Assignment>>(Val, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.Assignment>)responseData);

                }
            }
            catch (Exception ee)
            {

                throw ee;

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult DateGetAllAssignmentList(Models.Students.DateHomeworkVal Val)
        {

            List<StudentLogin.Models.Students.Assignment> dataColl = new List<StudentLogin.Models.Students.Assignment>();
            try
            {

                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/GetAssignmentList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.Assignment>>(Val, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.Assignment>)responseData);

                }
            }
            catch (Exception ee)
            {

                throw ee;

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult AddAssignment()
        {
            StudentLogin.Models.Students.HomewoskRes Val = new StudentLogin.Models.Students.HomewoskRes();
            StudentLogin.Models.Students.SubmitAssignment dataColl = new StudentLogin.Models.Students.SubmitAssignment();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };

                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.SubmitAssignment>(Request["jsonData"], microsoftDateFormatSettings);
                if (string.IsNullOrEmpty(beData.Notes))
                {
                    Val.ResponseMSG = "Pls Enter Description";
                    Val.IsSuccess = false;
                }
                else if (Request.Files.Count == 0)
                {
                    Val.ResponseMSG = "Pls Select Document";
                    Val.IsSuccess = false;
                }
                else
                {
                    var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StringContent(jsonbeData), "paraDataColl");

                        if (Request.Files.Count > 0)
                            if (Request.Files.Count > 0)
                            {

                                beData.SelectedFiles = Request.Files;
                                for (int i = 0; i < beData.SelectedFiles.Count; i++)
                                {
                                    beData.file1 = beData.SelectedFiles[i];

                                }
                            }



                        StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/SubmitAssignment", "POST");

                        try
                        {
                            string url = BaseUrl + "Student/SubmitAssignment";
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
                                    beData.file1 = beData.SelectedFiles[i];
                                    content1 = new StreamContent(beData.file1.InputStream);
                                    content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                    {
                                        FileName = beData.file1.FileName
                                    }; //  //form-data

                                    form.Add(content1);
                                }

                            }
                            var response = (client.PostAsync(url, form)).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var resStr = response.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(resStr))
                                {
                                    var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.HomewoskRes>(resStr);
                                    Val.ResponseMSG = resResult.ResponseMSG;
                                    Val.IsSuccess = resResult.IsSuccess;

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

            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.SubmitAssignment err = new StudentLogin.Models.Students.SubmitAssignment();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = Val };

        }

        #endregion

        #region"HomeWork Crude"

        public ActionResult Homework()
        {
            return View();
        }

        // for Select Dropdown
        public StudentLogin.Controllers.JsonNetResult GetHomeworkTypeList()
        {
            StudentLogin.Models.HomeworkType data = new StudentLogin.Models.HomeworkType();
            List<StudentLogin.Models.HomeworkType> dataColl = new List<StudentLogin.Models.HomeworkType>();
            try
            {
               
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetHomeWorkTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.HomeworkType>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.HomeworkType>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.HomeworkType err = new StudentLogin.Models.HomeworkType();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        // get All Home Wosk List
        public StudentLogin.Controllers.JsonNetResult GetAllHomeworkList()
        {
            Models.Students.HomeworkVal Val = new Models.Students.HomeworkVal();
            List<StudentLogin.Models.Students.HomeWork> dataColl = new List<StudentLogin.Models.Students.HomeWork>();
            try
            {

                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/GetHomeWorkList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.HomeWork>>(Val, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.HomeWork>)responseData);

                }
            }
            catch (Exception ee)
            {

                throw ee;

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult DateGetAllHomeworkList(Models.Students.DateHomeworkVal Val)
        {
          
            List<StudentLogin.Models.Students.HomeWork> dataColl = new List<StudentLogin.Models.Students.HomeWork>();
            try
            {

                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl ,"Student/GetHomeWorkList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.HomeWork>>(Val, keyValues);
                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.HomeWork>)responseData);

                }
            }
            catch (Exception ee)
            {

                throw ee;

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult AddHomeWork()
        {
            StudentLogin.Models.Students.HomewoskRes Val = new StudentLogin.Models.Students.HomewoskRes();
            StudentLogin.Models.Students.SubmitHomework dataColl = new StudentLogin.Models.Students.SubmitHomework();
           try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };

                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.SubmitHomework>(Request["jsonData"], microsoftDateFormatSettings);
                if (string.IsNullOrEmpty(beData.Notes))
                {
                    Val.ResponseMSG = "Pls Enter Description";
                    Val.IsSuccess = false;
                }
                else if (Request.Files.Count == 0)
                {
                    Val.ResponseMSG = "Pls Select Document";
                    Val.IsSuccess = false;
                }
                else {
                    var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StringContent(jsonbeData), "paraDataColl");

                        if (Request.Files.Count > 0)
                        {

                            beData.SelectedFiles = Request.Files;
                            for (int i = 0; i < beData.SelectedFiles.Count; i++)
                            {
                                beData.file1 = beData.SelectedFiles[i];

                            }
                        }
                         
                        StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/SubmitHomeWork", "POST");
                      
                        try
                        {
                            string url = BaseUrl + "Student/SubmitHomeWork";
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
                                    beData.file1 = beData.SelectedFiles[i];
                                    content1 = new StreamContent(beData.file1.InputStream);
                                    content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                    {
                                        FileName = beData.file1.FileName
                                    }; //  //form-data

                                    form.Add(content1);
                                }

                            }
                            var response = (client.PostAsync(url, form)).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var resStr = response.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(resStr))
                                {
                                    var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.HomewoskRes>(resStr);
                                    Val.ResponseMSG = resResult.ResponseMSG;
                                    Val.IsSuccess = resResult.IsSuccess;

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
               
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.SubmitHomework err = new StudentLogin.Models.Students.SubmitHomework();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = Val };

        }

        #endregion


        public ActionResult Notification()
        {
            return View();
        }
        #region"GetNoticeList"
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetNoticeList(StudentLogin.Models.Students.NotificationVal Data)
        {

            List<StudentLogin.Models.Students.NotificationList> dataColl = new List<StudentLogin.Models.Students.NotificationList>();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/GetNoticeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);

                var responseData = request.Execute<List<StudentLogin.Models.Students.NotificationList>>(Data, keyValues);

                if (responseData != null)
                {

                    dataColl = ((List<StudentLogin.Models.Students.NotificationList>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.NotificationList err = new StudentLogin.Models.Students.NotificationList();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion




        #region"NotificationLog"
        
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetNotification()
        { 
            List<StudentLogin.Models.Students.NotificationLog> dataColl = new List<StudentLogin.Models.Students.NotificationLog>();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/GetNotificationLog", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    UserId = User.userId
                };
                var responseData = request.Execute<List<StudentLogin.Models.Students.NotificationLog>>(para,keyValues);

                if (responseData != null)
                { 
                    dataColl = ((List<StudentLogin.Models.Students.NotificationLog>)responseData); 
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult ReadNotificationLog(StudentLogin.Models.Students.NotificationVal Data)
        {

            StudentLogin.Models.Students.NotificationVal dataColl = new StudentLogin.Models.Students.NotificationVal();
            try
            {
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/ReadNotificationLog", "POST");
               Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);

                var responseData = request.Execute<StudentLogin.Models.Students.NotificationVal>(Data, keyValues);

                if (responseData != null)
                {

                    dataColl = ((StudentLogin.Models.Students.NotificationVal)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.NotificationVal err = new StudentLogin.Models.Students.NotificationVal();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
        public ActionResult LeaveRequest()
        {
            return View();
        }

        public ActionResult Attendance()
        {
            return View();
        }

        public ActionResult TimeTable()
        {
            return View();
        }
        #region"TimeTable"
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetExamSchedule(StudentLogin.Models.Students.Val DataVal)
        {
            //StudentLogin.Models.Students.Val DataVal = new StudentLogin.Models.Students.Val();
            List<StudentLogin.Models.Students.ExamShedule> dataColl = new List<StudentLogin.Models.Students.ExamShedule>();
            try
            {

                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "student/GetExamSchedule", "POST");
               Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.ExamShedule>>(DataVal, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.ExamShedule>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.ExamShedule err = new StudentLogin.Models.Students.ExamShedule();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        
        #endregion
        public ActionResult OnlineExam()
        {
            ViewBag.WEBURLPATH = WebUrl;
            return View();
        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetOnlineExamList()
        {
            StudentLogin.Models.Students.BodyValue data = new StudentLogin.Models.Students.BodyValue();
            data.forType = 0;
            List<StudentLogin.Models.Students.OnlineExamList> dataColl = new List<StudentLogin.Models.Students.OnlineExamList>();
            try
            {
                var json = new JavaScriptSerializer().Serialize(data);
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetOnlineExamList", "POST");
               Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.OnlineExamList>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.OnlineExamList>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.ClassSchedule err = new StudentLogin.Models.ClassSchedule();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetOnlineExamDetById(StudentLogin.Models.Students.BodyValue data)
        {

            StudentLogin.Models.Students.OnlineExamList dataColl = new StudentLogin.Models.Students.OnlineExamList();
            try
            {
                var json = new JavaScriptSerializer().Serialize(data);
                
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetOnlineExamDetById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<StudentLogin.Models.Students.OnlineExamList>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((StudentLogin.Models.Students.OnlineExamList)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.ClassSchedule err = new StudentLogin.Models.ClassSchedule();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult StartClass()
        {
            StudentLogin.Models.Students.Reponce res = new StudentLogin.Models.Students.Reponce();
            try
            {

                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.StartClass>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");

                    if (Request.Files.Count > 0)
                        if (Request.Files.Count > 0)
                        {

                            beData.SelectedFiles = Request.Files;
                            for (int i = 0; i < beData.SelectedFiles.Count; i++)
                            {
                                beData.file1 = beData.SelectedFiles[i];

                            }
                        }

                    
                    StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/StartOnlineExam", "POST");
                  
                    try
                    {
                        string url = BaseUrl + "Student/StartOnlineExam";
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
                                beData.file1 = beData.SelectedFiles[i];
                                content1 = new StreamContent(beData.file1.InputStream);
                                content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") //  //form-data
                                {
                                    FileName = "Test.txt"
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

                                var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.Reponce>(resStr);
                                res.IsSuccess = resResult.IsSuccess;
                                res.ResponseMSG = resResult.ResponseMSG;
                                return new StudentLogin.Controllers.JsonNetResult() { Data = resResult };
                                
                            }
                            //Response.Write("API is working  <br/>");
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
                StudentLogin.Models.Students.StartClass err = new StudentLogin.Models.Students.StartClass();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = res };

        }


        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetOnlineExamQuestion(StudentLogin.Models.Students.BodyValue data)
        {

            List<StudentLogin.Models.Students.OnlineExamQuestion> dataColl = new List<StudentLogin.Models.Students.OnlineExamQuestion>();
            try
            {
                var json = new JavaScriptSerializer().Serialize(data);
               
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/GetOnlineExamQuestion", "POST");
                 Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.OnlineExamQuestion>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.OnlineExamQuestion>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.OnlineExamQuestion err = new StudentLogin.Models.Students.OnlineExamQuestion();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult SubmitOEAnswer()
        {
            
            StudentLogin.Models.Students.SubmitOEAnswer dataColl = new StudentLogin.Models.Students.SubmitOEAnswer();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.SubmitOEAnswer>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");
                    StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/SubmitOEAnswer", "POST");
                    
                    try
                    {
                        string url = BaseUrl + "Student/SubmitOEAnswer";
                        var method = new HttpMethod("POST");
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("ContentType", "JSON");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                        MultipartFormDataContent form = new MultipartFormDataContent();

                        HttpContent content1 = new StringContent(jsonbeData);
                        form.Add(content1, "paraDataColl");

                        if (Request.Files.Count > 0)
                        {
                            int fcount = Request.Files.Count;
                            for(int f = 0; f < fcount; f++)
                            {
                                var fil1 = Request.Files[f];
                                if (fil1 != null)
                                {                                    
                                    content1 = new StreamContent(fil1.InputStream);
                                    content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") //  //form-data
                                    {
                                        FileName = fil1.FileName,
                                        Name = "file"+f.ToString()
                                    };
                                    form.Add(content1);
                                }
                            }                           
                        }                            
                        
                        var response = (client.PostAsync(url, form)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var resStr = response.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(resStr))
                            {
                                var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.Reponce>(resStr);
                            }
                            Response.Write("API is working  <br/>");
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
                StudentLogin.Models.Students.SubmitOEAnswer err = new StudentLogin.Models.Students.SubmitOEAnswer();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult EndOnlineExam()
        {

            // StudentLogin.Models.Teacher.responceClass Res = new StudentLogin.Models.Teacher.responceClass();
            StudentLogin.Models.Students.SubmitOEAnswer dataColl = new StudentLogin.Models.Students.SubmitOEAnswer();
            // List<StudentLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.SubmitOEAnswer>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");

                    if (Request.Files.Count > 0)
                        if (Request.Files.Count > 0)
                        {
                            var fil1 = Request.Files["file1"];
                            if (fil1 != null)
                            {
                                beData.file1 = fil1;
                            }


                        }

                   
                    StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/EndOnlineExam", "POST");
                   
                    try
                    {
                        string url = BaseUrl + "Student/EndOnlineExam";
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
                                Name = "file1"
                            };
                            form.Add(content1);
                        }
                        if (beData.file2 != null)
                        {
                            content1 = new StreamContent(beData.file1.InputStream);
                            content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") //  //form-data
                            {
                                FileName = beData.file1.FileName,
                                Name = "file2"
                            };
                            form.Add(content1);
                        }
                        if (beData.file2 != null)
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
                                var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.Reponce>(resStr);
                                return new StudentLogin.Controllers.JsonNetResult() { Data = resResult };
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
                StudentLogin.Models.Students.SubmitOEAnswer err = new StudentLogin.Models.Students.SubmitOEAnswer();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        public ActionResult ExamReport()
        {
            return View();
        }

        #region"GetObtainMark"
        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult GetObtainMark(StudentLogin.Models.Students.MarkSheet.Val Data)
        {
            StudentLogin.Models.Students.MarkSheet.MarkSheet dataColl = new StudentLogin.Models.Students.MarkSheet.MarkSheet();
            try
            {

                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/GetObtainMark", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<StudentLogin.Models.Students.MarkSheet.MarkSheet>(Data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((StudentLogin.Models.Students.MarkSheet.MarkSheet)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.MarkSheet.MarkSheet err = new StudentLogin.Models.Students.MarkSheet.MarkSheet();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult PrintMarkSheet(StudentLogin.Models.Students.MarkSheet.Val Data)
        {
            StudentLogin.Models.Students.MarkSheet.MarkSheetPDF dataColl = new StudentLogin.Models.Students.MarkSheet.MarkSheetPDF();
            try
            {

                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Student/PrintMarkSheet", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<StudentLogin.Models.Students.MarkSheet.MarkSheetPDF>(Data, keyValues);
                if (responseData != null)
                {
                    dataColl = ((StudentLogin.Models.Students.MarkSheet.MarkSheetPDF)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.MarkSheet.MarkSheetPDF err = new StudentLogin.Models.Students.MarkSheet.MarkSheetPDF();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"GetExamTypeList"
        [HttpGet]
        public StudentLogin.Controllers.JsonNetResult GetExamTypeList()
        {
            StudentLogin.Models.Students.ExamType data = new StudentLogin.Models.Students.ExamType();
            List<StudentLogin.Models.Students.ExamType> dataColl = new List<StudentLogin.Models.Students.ExamType>();
            try
            {

               
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetExamTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<StudentLogin.Models.Students.ExamType>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<StudentLogin.Models.Students.ExamType>)responseData);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.Students.ExamType err = new StudentLogin.Models.Students.ExamType();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
        public ActionResult Fee()
        {
            return View();
        }

        public ActionResult ComplaintAndFeedback()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        #region"UserDetail"
        [HttpGet]
        public StudentLogin.Controllers.JsonNetResult GetUserDetail()
        {
            StudentLogin.Models.UserDetail dataColl = new Models.UserDetail();
            // List<StudentLogin.Models.Teacher.UserDetail> fsds = new List<Models.Teacher.UserDetail>();
            try
            {
                
                StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "General/GetUserDetail", "GET");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<StudentLogin.Models.UserDetail>(keyValues);
                //  var responseData1 = request.Execute<List<StudentLogin.Models.Teacher.UserDetail>>(dataColl, keyValues);


                //var responseData = request.Execute<StudentLogin.Models.Teachers.UserDetail>();
                if (responseData != null)
                {
                    dataColl = ((StudentLogin.Models.UserDetail)responseData);
                    //   fsds = ((List<StudentLogin.Models.Teacher.UserDetail>)responseData1);

                }
            }
            catch (Exception ee)
            {
                StudentLogin.Models.UserDetail err = new StudentLogin.Models.UserDetail();

            }

            return new StudentLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        public ActionResult TryDocument()
        {
            return View();
        }

        public ActionResult ContinueConfirmation()
        {
            return View();
        }

        [HttpPost]
        public StudentLogin.Controllers.JsonNetResult SaveContinewConfirmation()
        {
            StudentLogin.Models.Students.Reponce res = new StudentLogin.Models.Students.Reponce();
            try
            {

                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };

                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.ContinueConfirmation>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = new JavaScriptSerializer().Serialize(beData);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");

                    //if (Request.Files.Count > 0)
                    //    if (Request.Files.Count > 0)
                    //    {

                    //        beData.SelectedFiles = Request.Files;
                    //        for (int i = 0; i < beData.SelectedFiles.Count; i++)
                    //        {
                    //            beData.file1 = beData.SelectedFiles[i];

                    //        }
                    //    }


                    StudentLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Student/ContinuousConfirmation", "POST");

                    try
                    {
                        string url = BaseUrl + "Student/ContinuousConfirmation";
                        var method = new HttpMethod("POST");
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("ContentType", "JSON");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.access_token);
                        MultipartFormDataContent form = new MultipartFormDataContent();

                        HttpContent content1 = new StringContent(jsonbeData);
                        form.Add(content1, "paraDataColl");
                        //if (beData.SelectedFiles != null)
                        //{

                        //    for (int i = 0; i < beData.SelectedFiles.Count; i++)
                        //    {
                        //        beData.file1 = beData.SelectedFiles[i];
                        //        content1 = new StreamContent(beData.file1.InputStream);
                        //        content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") //  //form-data
                        //        {
                        //            FileName = "Test.txt"
                        //        };
                        //        form.Add(content1);
                        //    }

                        //}
                        var response = (client.PostAsync(url, form)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var resStr = response.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(resStr))
                            {

                                var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentLogin.Models.Students.Reponce>(resStr);
                                res.IsSuccess = resResult.IsSuccess;
                                res.ResponseMSG = resResult.ResponseMSG;
                                return new StudentLogin.Controllers.JsonNetResult() { Data = resResult };

                            }
                            //Response.Write("API is working  <br/>");
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
                StudentLogin.Models.Students.ContinueConfirmation err = new StudentLogin.Models.Students.ContinueConfirmation();
            }
            return new StudentLogin.Controllers.JsonNetResult() { Data = res };
        }
    }
}