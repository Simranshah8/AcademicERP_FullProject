using System.Web.Mvc;

namespace TeacherLogin.Areas.StudentLeave
{
    public class StudentLeaveAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StudentLeave";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StudentLeave_default",
                "StudentLeave/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}