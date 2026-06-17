using System.Web.Mvc;

namespace TeacherLogin.Areas.QuickAccess
{
    public class QuickAccessAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QuickAccess";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QuickAccess_default",
                "QuickAccess/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}