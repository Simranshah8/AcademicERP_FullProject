using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TeacherLogin.Areas.OnlineExam.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: OnlineExam/Creation
        
        public ActionResult ExamSetup()
        {
            return View();
        }

        #region"ExamSetup Crude"
        [HttpPost, ValidateInput(false)]
        public TeacherLogin.Controllers.JsonNetResult AddExamSetup()
        {
            TeacherLogin.Models.Responce dataColl = new TeacherLogin.Models.Responce();
            // List<TeacherLogin.Models.Teacher.TeacherProfile> fsds = new List<Models.Teacher.TeacherProfile>();
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = DeserializeObject<TeacherLogin.Models.Teacher.ExamSetup>(Request["jsonData"]);
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl,"Teacher/AddExamSetup", "POST");                
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(beData, keyValues);
                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Responce)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ExamSetup err = new TeacherLogin.Models.Teacher.ExamSetup();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult  GetAllExamSetup()
        {
            List<TeacherLogin.Models.Teacher.ExamSetup> dataColl = new List<TeacherLogin.Models.Teacher.ExamSetup>();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetAllExamSetup", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.ExamSetup>>(dataColl, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.ExamSetup>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ExamSetup err = new TeacherLogin.Models.Teacher.ExamSetup();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetExamSetupById(TeacherLogin.Models.Teacher.ExamSetupId ob)
        {
            TeacherLogin.Models.Teacher.ExamSetup dataColl = new TeacherLogin.Models.Teacher.ExamSetup();
            try
            {                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetExamSetupById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.ExamSetup>(ob, keyValues);


                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.ExamSetup)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.ExamSetup err = new TeacherLogin.Models.Teacher.ExamSetup();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }


        //deleteQuestionCategory
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult DelExamSetupById(TeacherLogin.Models.Teacher.ExamSetupId ob)
        {
            TeacherLogin.Models.Responce dataColl = new TeacherLogin.Models.Responce();
            try
            {
                
                string APIPwd = new TeacherLogin.Models.SessionContext().GetUserData().Password;
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/DelExamSetupById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Responce>(ob, keyValues);


                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Responce)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Responce err = new TeacherLogin.Models.Responce();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion
        public ActionResult QuestionSetup()
        {
            return View();
        }
        #region "Get An Set question setup"
      
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult AddQuestionSetup()
        {

            TeacherLogin.Models.Responce Res = new TeacherLogin.Models.Responce();

            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherLogin.Models.Teacher.QuestionSetup>(Request["jsonData"], microsoftDateFormatSettings);

                var jsonbeData = Request["jsonData"];// new JavaScriptSerializer().Serialize(beData);
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(jsonbeData), "paraDataColl");

                    if (Request.Files.Count > 0)
                        if (Request.Files.Count > 0)
                        {
                            var qImg = Request.Files["question"];
                            if (qImg != null)
                            {
                                beData.file1 = qImg;
                            }

                            int ins = 1;
                            foreach (var dc in beData.DetailsColl)
                            {
                                var ansImg = Request.Files["answer" + ins.ToString()];
                                if (ansImg != null)
                                    dc.file1 = ansImg;
                                ins++;
                            }
                        }

                    var teacherLogin = new TeacherLogin.Models.SessionContext().GetUserData();
                    string APIUser = teacherLogin.userName;
                    string APIPwd = teacherLogin.Password;
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddQuestionSetup", "POST");
                  
                    try
                    {
                        string url = BaseUrl + "Teacher/AddQuestionSetup";
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

                        int ins1 = 1;
                        foreach (var v in beData.DetailsColl)
                        {
                            if (v.file1 != null)
                            {
                                content1 = new StreamContent(v.file1.InputStream);
                                content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = v.file1.FileName,
                                    Name = "answer" + ins1.ToString()
                                };
                                form.Add(content1);
                            }
                            ins1++;
                        }

                        var response = (client.PostAsync(url, form)).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var resStr = response.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(resStr))
                            {
                                var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherLogin.Models.Responce>(resStr);
                                Res.ResponseMSG = resResult.ResponseMSG;
                                Res.IsSuccess = resResult.IsSuccess;
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
                TeacherLogin.Models.Teacher.QuestionSetup err = new TeacherLogin.Models.Teacher.QuestionSetup();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Res };

        }
      
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetQuestionByExamSetupId(TeacherLogin.Models.Teacher.QuestionSetupId W)
        {
            TeacherLogin.Models.Teacher.QuestionSetup data = new TeacherLogin.Models.Teacher.QuestionSetup();
           
            List<TeacherLogin.Models.Teacher.QuestionSetup> dataColl = new List<TeacherLogin.Models.Teacher.QuestionSetup>();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetQuestionByExamSetupId", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.QuestionSetup>>(W, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.QuestionSetup>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.QuestionSetup err = new TeacherLogin.Models.Teacher.QuestionSetup();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion




        public ActionResult QuestionList()
        {
            ViewBag.WEBURLPATH = WebUrl;
            return View();
        }
        #region "EMP GetQuestionList_OnlineExam"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetQuestionList(TeacherLogin.Models.ExamSetupDetails W)
        {
            TeacherLogin.Models.ExamType data = new TeacherLogin.Models.ExamType();
            List<TeacherLogin.Models.Teacher.QuestionSetup> dataColl = new List<TeacherLogin.Models.Teacher.QuestionSetup>();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetQuestionList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.QuestionSetup>>(W, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.QuestionSetup>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult DelQuestionList(int tranId)
        {
            TeacherLogin.Models.Responce dataColl = new TeacherLogin.Models.Responce();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/DelQuestionSetup", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var ob = new
                {
                    tranId=tranId
                };
                var responseData = request.Execute<TeacherLogin.Models.Responce>(ob, keyValues);


                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Responce)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Responce err = new TeacherLogin.Models.Responce();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion




        public ActionResult QuestionCategory()
        {
            return View();
        }
        #region"QuestionCategoryCRUD"

        //AddExamModal

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult AddExamModal()
        {
            TeacherLogin.Models.Teacher.QuestionCategory dataColl = new TeacherLogin.Models.Teacher.QuestionCategory();
            
            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };
                var beData = DeserializeObject<TeacherLogin.Models.Teacher.QuestionCategory>(Request["jsonData"]);
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/AddExamModal", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.QuestionCategory>(beData, keyValues);                
                if (responseData != null)
                {
                    if (responseData is TeacherLogin.Models.Teacher.QuestionCategory)
                        dataColl = ((TeacherLogin.Models.Teacher.QuestionCategory)responseData);
                    else
                        dataColl.ResponseMSG = responseData.ToString();
                }
            }
            catch (Exception ee)
            {
                dataColl.ResponseMSG = ee.Message;
            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        //GetAllExamModalList

       
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAllExamModalList()
        {
            List<TeacherLogin.Models.Teacher.QuestionCategory> dataColl = new List<TeacherLogin.Models.Teacher.QuestionCategory>();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetAllExamModal", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.QuestionCategory>>(dataColl, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.QuestionCategory>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.QuestionCategory err = new TeacherLogin.Models.Teacher.QuestionCategory();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        //EditQuestionCategory

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetQuestionCategoryById(TeacherLogin.Models.Teacher.CategoryId ob)
        {
            TeacherLogin.Models.Teacher.QuestionCategory dataColl = new TeacherLogin.Models.Teacher.QuestionCategory();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetExamModalById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.QuestionCategory>(ob, keyValues);


                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.QuestionCategory)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.QuestionCategory err = new TeacherLogin.Models.Teacher.QuestionCategory();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }


        //deleteQuestionCategory

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult DelQuestionCategory(TeacherLogin.Models.Teacher.CategoryId ob)
        {
            TeacherLogin.Models.Teacher.QuestionCategory dataColl = new TeacherLogin.Models.Teacher.QuestionCategory();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/DelExamModalById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<TeacherLogin.Models.Teacher.QuestionCategory>(ob, keyValues);


                if (responseData != null)
                {
                    dataColl = ((TeacherLogin.Models.Teacher.QuestionCategory)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.QuestionCategory err = new TeacherLogin.Models.Teacher.QuestionCategory();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        #endregion


        #region "All DropDown Content "

        #region"GetExamTypeList"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetExamTypeList()
        {
            TeacherLogin.Models.ExamType data = new TeacherLogin.Models.ExamType();
            List<TeacherLogin.Models.ExamType> dataColl = new List<TeacherLogin.Models.ExamType>();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetExamTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.ExamType>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.ExamType>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"GetExamTypeGroupList"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult GetExamTypeGroupList()
        {
            TeacherLogin.Models.ExamType data = new TeacherLogin.Models.ExamType();
            List<TeacherLogin.Models.ExamType> dataColl = new List<TeacherLogin.Models.ExamType>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetExamTypeGroupList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.ExamType>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.ExamType>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"GetExamTypeList"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult ExamTypeforEntity()
        {
            TeacherLogin.Models.ExamType data = new TeacherLogin.Models.ExamType();
            data.forEntity = 1;
            List<TeacherLogin.Models.ExamType> dataColl = new List<TeacherLogin.Models.ExamType>();
            try
            {
                
                
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetExamTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.ExamType>>(data, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.ExamType>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion
        #region"GetExamTypeList"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetExamSetupDetails(TeacherLogin.Models.ExamSetupDetails W)
        {
            TeacherLogin.Models.ExamType data = new TeacherLogin.Models.ExamType();
            List<TeacherLogin.Models.Teacher.QuestionModelDetails> dataColl = new List<TeacherLogin.Models.Teacher.QuestionModelDetails>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl , "Teacher/GetExamSetupDetails", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.QuestionModelDetails>>(W, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.QuestionModelDetails>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.ExamType err = new TeacherLogin.Models.ExamType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion


        #endregion







    }
}