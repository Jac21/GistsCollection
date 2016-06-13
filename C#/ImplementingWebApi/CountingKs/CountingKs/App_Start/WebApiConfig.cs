using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace CountingKs
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Food",
                routeTemplate: "api/nutrition/foods/{id}",
                defaults: new { controller = "Foods", id = RouteParameter.Optional }
                // constraints: new { id = "/d+" }
            );

            config.Routes.MapHttpRoute(
            name: "Measures",
            routeTemplate: "api/nutrition/foods/{id}/measures/{measuresId}",
            defaults: new { controller = "measures", measuresId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Diaries",
                routeTemplate: "api/user/diaries/{diaryId}",
                defaults: new { controller = "diaries", diaryId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DiaryEntries",
                routeTemplate: "api/user/diaries/{diaryId}/entries/{id}",
                defaults: new { controller = "diaries", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DiarySummary",
                routeTemplate: "api/user/diaries/{diaryId}/summary",
                defaults: new { controller = "diarysummary"}
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}