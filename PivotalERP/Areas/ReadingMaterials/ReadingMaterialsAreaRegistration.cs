using System.Web.Mvc;

namespace PivotalERP.Areas.ReadingMaterials
{
    public class ReadingMaterialsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ReadingMaterials";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ReadingMaterials_default",
                "ReadingMaterials/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}