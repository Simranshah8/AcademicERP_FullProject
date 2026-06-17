using System.Web.Mvc;

namespace TeacherLogin.Areas.LessonPlan
{
    public class LessonPlanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LessonPlan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LessonPlan_default",
                "LessonPlan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}