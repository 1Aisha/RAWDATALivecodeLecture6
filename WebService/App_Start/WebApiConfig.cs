using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "PersonApi",
                routeTemplate: "api/persons/{id}",
                defaults: new { controller = "Persons", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "MovieApi",
                routeTemplate: "api/movies/{id}",
                defaults: new {controller = "Movies", id = RouteParameter.Optional}
            );
        }
    }
}
