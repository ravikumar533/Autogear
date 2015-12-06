﻿using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace AutogearWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
              name: "Questions",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.UseDataContractJsonSerializer = true;
        }
    }
}
