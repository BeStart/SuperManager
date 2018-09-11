using System.Web.Mvc;

namespace SuperManager.UI.Areas.Manager
{
    public class ManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Manager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Manager_default",
                "Manager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                null,
                new string[] { "SuperManager.UI.Areas.Manager.Controllers" }
            );
        }
    }
}