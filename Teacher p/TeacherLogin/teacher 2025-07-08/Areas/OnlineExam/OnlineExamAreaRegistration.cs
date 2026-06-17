using System.Web.Mvc;

namespace TeacherLogin.Areas.OnlineExam
{
    public class OnlineExamAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OnlineExam";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OnlineExam_default",
                "OnlineExam/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}