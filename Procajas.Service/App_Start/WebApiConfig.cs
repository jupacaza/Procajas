using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Procajas.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "AdminItemApi",
                routeTemplate: "api/{controller}/{type}/{name}",
                defaults: new { controller = "AdminItem", type = RouteParameter.Optional, name = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DiscrepancyApi",
                routeTemplate: "api/{controller}/{material}/{id}",
                defaults: new { controller = "Discrepancy", material = RouteParameter.Optional, id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "FinishedProductApi",
                routeTemplate: "api/{controller}/{invoiceNumber}/{material}",
                defaults: new { controller = "FinishedProduct", invoiceNumber = RouteParameter.Optional, material = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ProcessApi",
                routeTemplate: "api/{controller}/{department}",
                defaults: new { controller = "Process", department = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "WarehouseApi",
                routeTemplate: "api/{controller}/{department}/{id}",
                defaults: new { controller = "Warehouse", department = RouteParameter.Optional, id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
