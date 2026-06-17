using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace TeacherLogin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public BaseController()
        {
            WebUrl = System.Configuration.ConfigurationManager.AppSettings["baseURL"].ToString();
            BaseUrl = System.Configuration.ConfigurationManager.AppSettings["baseURL"].ToString() + "/v1/";            
        }
        protected virtual new TeacherLogin.Models.Teacher.TeacherLogin User
        {
            get { return HttpContext.User as TeacherLogin.Models.Teacher.TeacherLogin; }
        }

        protected  TeacherLogin.Models.Teacher.TeacherLogin UserDet
        {
            get
            {
                return new TeacherLogin.Models.SessionContext().GetUserData();
            }
        }

        protected string APIUser
        {
            get
            {
                return UserDet.userName;
            }
        }
        protected string APIPwd
        {
            get
            {
                return UserDet.Password;
            }
        }

        protected string WebUrl = "";
        protected string BaseUrl = "";
        public HttpRequestBase context;
        protected JsonSerializerSettings DateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
        protected T DeserializeObject<T>(string jsonData)
        {
            T para = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData, DateFormatSettings);
            return para;
        }




    }
    public enum AttendanceType
    {
        Present,
        Absent,
        Late,
        Leave

    }

    public class JsonNetResult : JsonResult
    {
        public bool TmpResult { get; set; }
        public object Data { get; set; }
        public int TotalCount { get; set; }
        public JsonNetResult()
        {
            TmpResult = false;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/x-www-form-urlencoded";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data != null)
            {

                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());

                if (TmpResult)
                {
                    TmpData td = new TmpData();
                    td.Data = Data;
                    td.TotalCount = TotalCount;
                    serializer.Serialize(writer, td);
                }
                else
                    serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }

        class TmpData
        {
            public object Data { get; set; }
            public int TotalCount { get; set; }
        }

    }

    public class JsonNetResultWithEnum : JsonResult
    {
        public bool TmpResult { get; set; }
        public object Data { get; set; }
        public int TotalCount { get; set; }

        public JsonNetResultWithEnum()
        {

        }
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                JsonSerializer serializer = JsonSerializer.Create(settings);
                //JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());

                if (TmpResult)
                {
                    TmpData td = new TmpData();
                    td.Data = Data;
                    td.TotalCount = TotalCount;
                    serializer.Serialize(writer, td);
                }
                else
                    serializer.Serialize(writer, Data);

                writer.Flush();
            }
        }

        class TmpData
        {
            public object Data { get; set; }
            public int TotalCount { get; set; }
        }

    }
}