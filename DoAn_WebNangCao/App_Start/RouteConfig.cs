using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAn_WebNangCao
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CreateExam",
                url: "{controller}/{action}/{idLinhVuc}",
                defaults: new { controller = "Test", action = "CreateExam", idLinhVuc = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NewQuiz",
                url: "{controller}/{action}/{type}/{quiz_idx}",
                defaults: new { controller = "Test", action = "NewQuiz", type = UrlParameter.Optional, quiz_idx = UrlParameter.Optional }
           );
        }
    }
}
