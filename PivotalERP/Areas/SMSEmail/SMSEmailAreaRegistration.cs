using System.Web.Mvc;

namespace PivotalERP.Areas.SMSEmail
{
    public class SMSEmailAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SMSEmail";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SMSEmail_default",
                "SMSEmail/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}