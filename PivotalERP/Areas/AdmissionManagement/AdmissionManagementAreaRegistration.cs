using System.Web.Mvc;

namespace PivotalERP.Areas.AdmissionManagement
{
    public class AdmissionManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdmissionManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdmissionManagement_default",
                "AdmissionManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}