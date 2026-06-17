using System.Web.Mvc;

namespace TeacherLogin.Areas.StudentComplaint
{
    public class StudentComplaintAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StudentComplaint";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StudentComplaint_default",
                "StudentComplaint/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}