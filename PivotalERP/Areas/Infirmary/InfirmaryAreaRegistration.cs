using System.Web.Mvc;

namespace PivotalERP.Areas.Infirmary.Controllers
{
    public class InfirmaryAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Infirmary";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Infirmary_default",
                "Infirmary/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}