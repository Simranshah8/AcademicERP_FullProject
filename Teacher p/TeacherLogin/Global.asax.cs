using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security.Cryptography;
using System.Web.Security;

namespace TeacherLogin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
            //self change
            GlobalFilters.Filters.Add(new ValidateInputAttribute(false));
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {

            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    TeacherLogin.Models.SessionContext sessContext = new TeacherLogin.Models.SessionContext();
                    TeacherLogin.Models.Teacher.TeacherLogin loginUser = sessContext.GetUserData();
                    TeacherLogin.Models.UserASP user = new TeacherLogin.Models.UserASP(loginUser.userName);
                    
                    user.access_token = loginUser.access_token;
                    user.token_type = loginUser.token_type;
                    user.expires_in = loginUser.expires_in;
                    user.refresh_token = loginUser.refresh_token;
                    user.userName = loginUser.userName;
                    user.userId = loginUser.userId;
                    user.customerCode = loginUser.customerCode;
                    user.userGroup = loginUser.userGroup;
                    user.Password = loginUser.Password;


                    HttpContext.Current.User = user;
                }
            }
            else
            {
                Exception exception = Server.GetLastError();
                if (exception is CryptographicException)
                {
                    FormsAuthentication.SignOut();
                }
            }

        }

    }
}
