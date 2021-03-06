﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: null, url: "", defaults: new { controller = "Blurbs", action = "List", category = (string)null, page = 1 });
            routes.MapRoute(name: null, url: "Page{page}", defaults: new { controller = "Blurbs", action = "List", category = (string)null }, constraints: new { page = @"\d+" });
            routes.MapRoute(name: null, url: "{controller}", defaults: new { action = "List"});
            routes.MapRoute(name: null, url: "{controller}/Page{page}", defaults: new { action = "List"},constraints: new {page=@"\d+" });
            routes.MapRoute(name: null, url: "{controller}/{action}", defaults: new { action = "List", category = (string)null,page=1 }, constraints: new { controller = "Product",action="List" });
            routes.MapRoute(name: null, url: "{controller}/{action}", defaults: new { action = "List", category = (string)null,page=1 }, constraints: new { controller = "Blurb",action="List" });
            routes.MapRoute(name: null, url: "{controller}/{category}", defaults: new { action = "List" },constraints: new {controller="Product"});
            routes.MapRoute(name: null, url: "{controller}/{category}", defaults: new { action = "List" }, constraints: new { controller = "Blurb" });
            routes.MapRoute(name: null, url: "{controller}/{action}");
            routes.MapRoute(name: null, url: "{controller}/{category}/Page{page}", defaults: new { action = "List"},constraints: new {page=@"\d+" });
            routes.MapRoute(name: null, url: "{category}", defaults: new { controller = "Blurbs", action = "List", page = 1 });
            routes.MapRoute(name: null, url: "{category}/Page{page}", defaults: new { controller = "Blurbs", action = "List" }, constraints: new { page = @"\d+" });
          
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults:new {id=UrlParameter.Optional }
                );
        }
    }
}
