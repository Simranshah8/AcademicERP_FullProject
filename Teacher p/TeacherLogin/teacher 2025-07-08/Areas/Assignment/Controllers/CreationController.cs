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

namespace TeacherLogin.Areas.Assignment.Controllers
{
    public class CreationController : TeacherLogin.Controllers.BaseController
    {
        // GET: Assignment/Creation
        public ActionResult Assignment()
        {
            ViewBag.WEBURLPATH = WebUrl;
            ViewBag.APIUrl = BaseUrl.Replace("/v1/", "");
            return View();
        }

        #region"GetAssignmentTypeList"
        [HttpGet]
        public TeacherLogin.Controllers.JsonNetResult AssignmentTypeList()
        {
            List<TeacherLogin.Models.Teacher.AssignmentType> dataColl = new List<TeacherLogin.Models.Teacher.AssignmentType>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetAssignmentTypeList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.AssignmentType>>(dataColl, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.AssignmentType>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.AssignmentType err = new TeacherLogin.Models.Teacher.AssignmentType();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        #region"Add Assignment"
        [AllowAnonymous]

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult AddAssignment()
        {
            TeacherLogin.Models.Responce Val = new TeacherLogin.Models.Responce();
            TeacherLogin.Models.Teacher.Assignment dataColl = new TeacherLogin.Models.Teacher.Assignment();

            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };

                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherLogin.Models.Teacher.Assignment>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = Request["jsonData"];
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
                    TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/AddAssignment", "POST");

                    try
                    {
                        string url = BaseUrl + "Teacher/AddAssignment";
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
                                //Bitmap img = GenerateThumbnails(0.5, beData.file1.InputStream);
                                //Stream stream = new MemoryStream(ToByteArray(img, ImageFormat.Jpeg));
                                //content1 = new StreamContent(stream);

                                content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    FileName = beData.file1.FileName
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
                TeacherLogin.Models.Teacher.Assignment err = new TeacherLogin.Models.Teacher.Assignment();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Val };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult UpdateDeadlineAssignment()
        {
            TeacherLogin.Models.Responce Val = new TeacherLogin.Models.Responce();

            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };

                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherLogin.Models.Teacher.Assignment>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = Request["jsonData"];

                try
                {
                    string url = BaseUrl + "Teacher/UpdateAssignmentDeadline";
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
                            var resResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherLogin.Models.Responce>(resStr);
                            Val.ResponseMSG = resResult.ResponseMSG;
                            Val.IsSuccess = resResult.IsSuccess;

                        }

                    }
                    else
                    {
                        Val.ResponseMSG = response.ToString();
                        Val.IsSuccess = false;

                        Response.Write("API is Not Working currently <br/>");
                    }
                }
                catch (Exception ex1)
                {
                    Val.ResponseMSG = ex1.Message;
                    Val.IsSuccess = false;

                }
            }
            catch (Exception ee)
            {
                Val.ResponseMSG = ee.Message;
                Val.IsSuccess = false;

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = Val };

        }
        #endregion

        #region"Get AssignmentList"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAssignmentList(TeacherLogin.Models.Teacher.Assignment w)
        {
            List<TeacherLogin.Models.Teacher.AssignmentList> dataColl = new List<TeacherLogin.Models.Teacher.AssignmentList>();
            try
            { 
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetAssignmentList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.AssignmentList>>(w, keyValues);

                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.AssignmentList>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.Assignment err = new TeacherLogin.Models.Teacher.Assignment();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAllAssignment(TeacherLogin.Models.Teacher.Assignment w)
        {
            List<TeacherLogin.Models.Teacher.Assignment> dataColl = new List<TeacherLogin.Models.Teacher.Assignment>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetAssignmentList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.Assignment>>(dataColl, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.Assignment>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.Assignment err = new TeacherLogin.Models.Teacher.Assignment();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAssignmentColl()
        {
            TeacherLogin.Models.Teacher.HomewoskVal data = new TeacherLogin.Models.Teacher.HomewoskVal();
            List<TeacherLogin.Models.Teacher.AssignmentList> dataColl = new List<TeacherLogin.Models.Teacher.AssignmentList>();
            try
            { 
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetAssignmentList", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.AssignmentList>>(data, keyValues);
                 
                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.AssignmentList>)responseData);
                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.Assignment err = new TeacherLogin.Models.Teacher.Assignment();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }


        #region "GetAssignmentById"

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAssignmentById(int hwId)
        {
            List<TeacherLogin.Models.Teacher.AssignmentById> dataColl = new List<TeacherLogin.Models.Teacher.AssignmentById>();
            try
            {
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetAssignmentById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    AssignmentId = hwId
                };

                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.AssignmentById>>(para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.AssignmentById>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.Assignment err = new TeacherLogin.Models.Teacher.Assignment();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }
        #endregion

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult GetAssignmentCorrectById(int hwId)
        {
            TeacherLogin.Models.Teacher.HomewoskVal data = new TeacherLogin.Models.Teacher.HomewoskVal();
            List<TeacherLogin.Models.Teacher.Assignment> dataColl = new List<TeacherLogin.Models.Teacher.Assignment>();
            try
            {


                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/GetAssignmentById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    AssignmentId = hwId
                };

                var responseData = request.Execute<List<TeacherLogin.Models.Teacher.Assignment>>(para, keyValues);


                if (responseData != null)
                {
                    dataColl = ((List<TeacherLogin.Models.Teacher.Assignment>)responseData);

                }
            }
            catch (Exception ee)
            {
                TeacherLogin.Models.Teacher.Assignment err = new TeacherLogin.Models.Teacher.Assignment();

            }

            return new TeacherLogin.Controllers.JsonNetResult() { Data = dataColl };

        }

        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult DelAssignmentById(int AssignmentId)
        {
            TeacherLogin.Models.Responce dataColl = new TeacherLogin.Models.Responce();
            try
            {

                string APIPwd = new TeacherLogin.Models.SessionContext().GetUserData().Password;
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/DelAssignmentById", "POST");
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("Bearer", User.access_token);
                var para = new
                {
                    AssignmentId = AssignmentId
                };
                var responseData = request.Execute<TeacherLogin.Models.Responce>(para, keyValues);


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

        #region"Check Assignment"
        [HttpPost]
        public TeacherLogin.Controllers.JsonNetResult CheckAssignment()
        {
            TeacherLogin.Models.Responce Val = new TeacherLogin.Models.Responce();
            TeacherLogin.Models.Teacher.Assignment dataColl = new TeacherLogin.Models.Teacher.Assignment();

            try
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat

                };

                var beData = Newtonsoft.Json.JsonConvert.DeserializeObject<TeacherLogin.Models.Teacher.AssignmentChecked>(Request["jsonData"], microsoftDateFormatSettings);
                var jsonbeData = Request["jsonData"];
                beData.SelectedFiles = Request.Files;
                TeacherLogin.Models.APIRequest request = new Models.APIRequest(BaseUrl, "Teacher/CheckAssignment", "POST");

                try
                {
                    string url = BaseUrl + "Teacher/CheckAssignment";
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


        public Image ResizeImage(Image sourceImage, int maxWidth, int maxHeight)
        {
            // Determine which ratio is greater, the width or height, and use
            // this to calculate the new width and height. Effectually constrains
            // the proportions of the resized image to the proportions of the original.
            double xRatio = (double)sourceImage.Width / maxWidth;
            double yRatio = (double)sourceImage.Height / maxHeight;
            double ratioToResizeImage = Math.Max(xRatio, yRatio);
            int newWidth = (int)Math.Floor(sourceImage.Width / ratioToResizeImage);
            int newHeight = (int)Math.Floor(sourceImage.Height / ratioToResizeImage);

            // Create new image canvas -- use maxWidth and maxHeight in this function call if you wish
            // to set the exact dimensions of the output image.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppArgb);

            // Render the new image, using a graphic object
            using (Graphics newGraphic = Graphics.FromImage(newImage))
            {
                // Set the background color to be transparent (can change this to any color)
                newGraphic.Clear(Color.Transparent);

                // Set the method of scaling to use -- HighQualityBicubic is said to have the best quality
                newGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Apply the transformation onto the new graphic
                Rectangle sourceDimensions = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
                Rectangle destinationDimensions = new Rectangle(0, 0, newWidth, newHeight);
                newGraphic.DrawImage(sourceImage, destinationDimensions, sourceDimensions, GraphicsUnit.Pixel);
            }

            // Image has been modified by all the references to it's related graphic above. Return changes.
            return newImage;
        }

        private byte[] ToByteArray(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
        private Bitmap GenerateThumbnails(double scaleFactor, Stream stream)
        {
            using (var image = Image.FromStream(stream))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);

                if (newWidth < 800)
                    newWidth = 800;

                if (newHeight < 800)
                    newHeight = 800;

                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                // thumbnailImg.Save("", image.RawFormat);

                return thumbnailImg;
            }

        }
        
        #endregion




    }





}

