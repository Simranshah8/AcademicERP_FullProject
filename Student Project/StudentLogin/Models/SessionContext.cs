using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;


using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace StudentLogin.Models
{


    public class Credentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Httpresponse
    {
        public int Statuscode { get; set; }
        public string Location { get; set; }
    }

    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string userName { get; set; }
        public int userId { get; set; }
        public string customerCode { get; set; }
        public string userGroup { get; set; }
    //    "": "****",
    //"": "student",
    }

    public class APIRequest
    {

        public string APIUser { get; set; }
        public string APIPwd { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        /// <summary>
        /// Url of http server wich request will be created to.
        /// </summary>
        public String URL { get; set; }

        public string BaseURL { get; set; }

        /// <summary>
        /// HTTP Verb wich will be used. Eg. GET, POST, PUT, DELETE.
        /// </summary>
        public String Verb { get; set; }

        /// <summary>
        /// Request content, Json by default.
        /// </summary>
        /// 
        private string _Content = "text/json";
        public String Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        /// <summary>
        /// User and Password for Basic Authentication
        /// </summary>
        public Credentials Credentials { get; set; }

        public HttpWebRequest HttpRequest { get; internal set; }
        public HttpWebResponse HttpResponse { get; internal set; }
        public CookieContainer CookieContainer = new CookieContainer();

        /// <summary>
        /// Constructor Overload that allows passing URL and the VERB to be used.
        /// </summary>
        /// <param name="url">URL which request will be created</param>
        /// <param name="verb">Http Verb that will be userd in this request</param>
        public APIRequest(string baseURL, string url, string verb, string content = "")
        {
            BaseURL = baseURL;
            URL = baseURL+url;
            Verb = verb;

            if (!string.IsNullOrEmpty(content))
                this._Content = content;
        }

        /// <summary>
        /// Default constructor overload without any paramter
        /// </summary>
        public APIRequest()
        {
            Verb = "GET";
        }

        public APIRequest(string baseURL)
        {
            Verb = "GET";
            BaseURL = baseURL;
        }
        public object Execute<TT>(string url, object obj, string verb)
        {
            if (url != null)
                URL = url;

            if (verb != null)
                Verb = verb;

            HttpRequest = CreateRequest();

            WriteStream(obj);

            try
            {
                HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
            }
            catch (WebException error)
            {
                HttpResponse = (HttpWebResponse)error.Response;
                return ReadResponseFromError(error);
            }

            return JsonConvert.DeserializeObject<TT>(ReadResponse());
        }

        public object Execute<TT>(object obj, Dictionary<string, string> headersKey = null)
        {

            HttpRequest = CreateRequest();

            if (headersKey != null)
            {
                foreach (var v in headersKey)
                {
                    HttpRequest.Headers.Add("Authorization", v.Key + " " + v.Value);
                }
            }

            WriteStream(obj);

            try
            {
                HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
            }
            catch (WebException error)
            {
                HttpResponse = (HttpWebResponse)error.Response;
                return ReadResponseFromError(error);
            }

            return JsonConvert.DeserializeObject<TT>(ReadResponse());
        }
        public object Execute<TT>(string url, Dictionary<string, string> headersKey = null)
        {
            if (url != null)
                URL = url;

            HttpRequest = CreateRequest();

            try
            {
                if (headersKey != null)
                {
                    foreach (var v in headersKey)
                    {
                        HttpRequest.Headers.Add("Authorization", v.Key + " " + v.Value);
                    }
                }


                HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
            }
            catch (WebException error)
            {
                HttpResponse = (HttpWebResponse)error.Response;
                return ReadResponseFromError(error);
            }

            return JsonConvert.DeserializeObject<TT>(ReadResponse());
        }

        public object Execute<TT>(Dictionary<string, string> headersKey = null)
        {
            if (URL == null)
                throw new ArgumentException("URL cannot be null.");

            HttpRequest = CreateRequest();

            try
            {
                if (headersKey != null)
                {
                    foreach (var v in headersKey)
                    {
                        HttpRequest.Headers.Add("Authorization", v.Key + " " + v.Value);
                    }
                }

                HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
            }
            catch (WebException error)
            {
                HttpResponse = (HttpWebResponse)error.Response;
                return ReadResponseFromError(error);
            }

            return JsonConvert.DeserializeObject<TT>(ReadResponse());
        }

        public object Execute(string url, object obj, string verb, Dictionary<string, string> headersKey = null)
        {
            if (url != null)
                URL = url;

            if (verb != null)
                Verb = verb;

            HttpRequest = CreateRequest();


            if (headersKey != null)
            {
                foreach (var v in headersKey)
                {
                    HttpRequest.Headers.Add("Authorization", v.Key + " " + v.Value);
                }
            }


            WriteStream(obj);

            try
            {
                HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
            }
            catch (WebException error)
            {
                HttpResponse = (HttpWebResponse)error.Response;
                return ReadResponseFromError(error);
            }

            return ReadResponse();
        }
        public object Execute(object obj, Dictionary<string, string> headersKey = null)
        {

            HttpRequest = CreateRequest();

            if (headersKey != null)
            {
                foreach (var v in headersKey)
                {
                    HttpRequest.Headers.Add("Authorization", v.Key + " " + v.Value);
                }
            }

            WriteStream(obj);

            try
            {
                HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
            }
            catch (WebException error)
            {
                HttpResponse = (HttpWebResponse)error.Response;
                return ReadResponseFromError(error);
            }

            return ReadResponse();
        }
        public object Execute(string url, Dictionary<string, string> headersKey = null)
        {
            if (url != null)
                URL = url;

            HttpRequest = CreateRequest();

            if (headersKey != null)
            {
                foreach (var v in headersKey)
                {
                    HttpRequest.Headers.Add("Authorization", v.Key + " " + v.Value);
                }
            }

            try
            {
                HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
            }
            catch (WebException error)
            {
                HttpResponse = (HttpWebResponse)error.Response;
                return ReadResponseFromError(error);
            }

            return ReadResponse();
        }

        public object Execute(Dictionary<string, string> headersKey = null)
        {
            if (URL == null)
                throw new ArgumentException("URL cannot be null.");

            HttpRequest = CreateRequest();

            try
            {
                if (headersKey != null)
                {
                    foreach (var v in headersKey)
                    {
                        HttpRequest.Headers.Add("Authorization", v.Key + " " + v.Value);
                    }
                }


                HttpResponse = (HttpWebResponse)HttpRequest.GetResponse();
            }
            catch (WebException error)
            {
                HttpResponse = (HttpWebResponse)error.Response;
                return ReadResponseFromError(error);
            }

            return ReadResponse();
        }

        internal HttpWebRequest CreateRequest()
        {
            var basicRequest = (HttpWebRequest)WebRequest.Create(URL);
            basicRequest.ContentType = Content;
            basicRequest.Method = Verb;
            basicRequest.CookieContainer = CookieContainer;

            if (Credentials != null)
                basicRequest.Headers.Add("Authorization", "Basic" + " " + EncodeCredentials(Credentials));


            return basicRequest;
        }

        internal void WriteStream(object obj)
        {
            if (obj != null)
            {
                using (var streamWriter = new StreamWriter(HttpRequest.GetRequestStream()))
                {
                    if (obj is string)
                        streamWriter.Write(obj);
                    else
                        streamWriter.Write(JsonConvert.SerializeObject(obj));
                }
            }
        }

        internal String ReadResponse()
        {
            if (HttpResponse != null)
                using (var streamReader = new StreamReader(HttpResponse.GetResponseStream()))
                    return streamReader.ReadToEnd();

            return string.Empty;
        }

        internal String ReadResponseFromError(WebException error)
        {
            using (var streamReader = new StreamReader(error.Response.GetResponseStream()))
                return streamReader.ReadToEnd();
        }

        internal static string EncodeCredentials(Credentials credentials)
        {
            var strCredentials = string.Format("{0}:{1}", credentials.UserName, credentials.Password);
            var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(strCredentials));

            return encodedCredentials;
        }

        public TokenResponse GetToken( string userName, string pwd)
        {
         
            TokenResponse logonCookie = new TokenResponse();

            Dictionary<string, object> logonBody = new Dictionary<string, object>();
            logonBody.Add("userName", userName);
            logonBody.Add("password", pwd);
            logonBody.Add("grant_type", "password");
            string bodyContent = string.Empty;
            if (logonBody != null && logonBody.Count > 0)
            {
                int i = 0;
                foreach (string key in logonBody.Keys)
                {
                    if (i == 0)
                    {
                        bodyContent = key + "=" + logonBody[key];
                        i++;
                    }
                    else
                    {
                        bodyContent = bodyContent + "&" + key + "=" + logonBody[key];
                    }
                }
            }
            byte[] body = Encoding.Default.GetBytes(bodyContent);

            try
            {
                // Build request
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(BaseURL+"token"));
                webRequest.CookieContainer = new CookieContainer();
                webRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                webRequest.UserAgent = "Mozilla/5.0";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";

                Stream stream = webRequest.GetRequestStream();
                stream.Write(body, 0, body.Length);

                // Get response
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                if (response != null)
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                        logonCookie = JsonConvert.DeserializeObject<TokenResponse>(streamReader.ReadToEnd());

                }

            }
            catch (WebException we)
            {
                throw we;
            }
            catch (Exception e)
            {
                throw e;
            }

            return logonCookie;
        }
        private TokenResponse GetRefreshToken( string refresh_token, int userId)
        {

            TokenResponse logonCookie = new TokenResponse();

            Dictionary<string, object> logonBody = new Dictionary<string, object>();
            logonBody.Add("refresh_token", refresh_token);
            logonBody.Add("userId", userId);
            logonBody.Add("grant_type", "refresh_token");
            string bodyContent = string.Empty;
            if (logonBody != null && logonBody.Count > 0)
            {
                int i = 0;
                foreach (string key in logonBody.Keys)
                {
                    if (i == 0)
                    {
                        bodyContent = key + "=" + logonBody[key];
                        i++;
                    }
                    else
                    {
                        bodyContent = bodyContent + "&" + key + "=" + logonBody[key];
                    }
                }
            }
            byte[] body = Encoding.Default.GetBytes(bodyContent);

            try
            {
                // Build request
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(BaseURL+"token"));
                webRequest.CookieContainer = new CookieContainer();
                webRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                webRequest.UserAgent = "Mozilla/5.0";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";

                Stream stream = webRequest.GetRequestStream();
                stream.Write(body, 0, body.Length);

                // Get response
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                if (response != null)
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                        logonCookie = JsonConvert.DeserializeObject<TokenResponse>(streamReader.ReadToEnd());

                }

            }
            catch (WebException we)
            {
                throw we;
            }
            catch (Exception e)
            {
                throw e;
            }

            return logonCookie;
        }
    }

    public class SessionContext
    {
        public void SetAuthenticationToken(string name, bool isPersistant, StudentLogin.Models.Students.StudentUser userData)
        {
            //string data = null;
            //if (userData != null)
            //    data = new JavaScriptSerializer().Serialize(userData);

            string data = null;
            if (userData != null)
            {
                var user = new
                {
                   // access_token = userData.access_token,
                    //customerCode = userData.customerCode,
                    name = userData.userName,
                    //refresh_token = userData.refresh_token,
                    //userGroup = userData.userGroup,
                    userId = userData.userId,
                    userName = userData.userName,
                    isReg=userData.userGroup=="rstudent" ? true : false
                };

                data = new JavaScriptSerializer().Serialize(user);
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddMinutes(120), isPersistant, data);

            string cookieData = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieData)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };

            HttpContext.Current.Response.Cookies.Add(cookie);

            HttpCookie accToken = new HttpCookie("accessToken", userData.access_token)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };
            HttpContext.Current.Response.Cookies.Add(accToken);
        }

        public StudentLogin.Models.Students.StudentUser GetUserData()
        {
            StudentLogin.Models.Students.StudentUser userData = null;

            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                    userData = new JavaScriptSerializer().Deserialize(ticket.UserData, typeof(StudentLogin.Models.Students.StudentUser)) as StudentLogin.Models.Students.StudentUser;

                    HttpCookie tokenCookies = HttpContext.Current.Request.Cookies["accessToken"];
                    if (tokenCookies != null)
                    {
                        userData.access_token = tokenCookies.Value;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return userData;
        }
    }
}