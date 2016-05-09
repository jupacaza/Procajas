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
                name: "AdminItemsApi",
                routeTemplate: "{controller}/{type}/{name}",
                defaults: new { controller = "AdminItems", type = RouteParameter.Optional, name = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DiscrepancyApi",
                routeTemplate: "{controller}/{material}/{id}",
                defaults: new { controller = "Discrepancy", material = RouteParameter.Optional, id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "FinishedProductApi",
                routeTemplate: "{controller}/{invoiceNumber}/{material}",
                defaults: new { controller = "FinishedProduct", invoiceNumber = RouteParameter.Optional, material = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ProcessApi",
                routeTemplate: "{controller}/{department}",
                defaults: new { controller = "Process", department = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "WarehouseApi",
                routeTemplate: "{controller}/{department}/{id}",
                defaults: new { controller = "Warehouse", department = RouteParameter.Optional, id = RouteParameter.Optional }
            );
        }
    }
}
