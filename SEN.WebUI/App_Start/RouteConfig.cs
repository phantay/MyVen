using System.Web.Mvc;
using System.Web.Routing;

namespace SEN.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "DangKy", url: "DangKy", defaults: new { controller = "Home", action = "DangKy" });
            routes.MapRoute(name: "DangNhap", url: "DangNhap", defaults: new { controller = "Home", action = "DangNhap" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}