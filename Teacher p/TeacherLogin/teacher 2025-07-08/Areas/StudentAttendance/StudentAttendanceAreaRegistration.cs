using System.Web.Mvc;

namespace TeacherLogin.Areas.StudentAttendance
{
    public class StudentAttendanceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StudentAttendance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StudentAttendance_default",
                "StudentAttendance/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}