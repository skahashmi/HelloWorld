using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using System.Reflection;
using System.Web.Http.WebHost;
using SignalR.WebAPI.Common;

namespace SignalR.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var httpControllerRouteHandler = typeof(HttpControllerRouteHandler).GetField("_instance",
                          BindingFlags.Static |
                          BindingFlags.NonPublic);

            if (httpControllerRouteHandler != null)
            {
                httpControllerRouteHandler.SetValue(null,
                    new Lazy<HttpControllerRouteHandler>(() => new SessionHttpControllerRouteHandler(), true));
            }

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
