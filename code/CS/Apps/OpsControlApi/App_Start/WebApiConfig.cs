﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Org.OpsControlApi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services

      // Web API routes
      config.MapHttpAttributeRoutes();

      //config.Routes.MapHttpRoute(
      //    name: "DefaultApi",
      //    routeTemplate: "api/{controller}/{id}",
      //    defaults: new { id = RouteParameter.Optional }
      //);

      var formatters = GlobalConfiguration.Configuration.Formatters;
      var jsonFormatter = formatters.JsonFormatter;
      var settings = jsonFormatter.SerializerSettings;
      settings.Formatting = Formatting.Indented;
      settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

      config.EnsureInitialized();
    }
  }
}