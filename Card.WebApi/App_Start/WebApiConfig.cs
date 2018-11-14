using Card.Core;
using Card.Interface.IRepo;
using Card.Interface.IServices;
using Card.Repository;
using Card.WebApi.DependencyInjection;
using Card.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;

namespace Card.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new CustomExceptionFilter());//Excepiton filter to capture log

            //Dependency Injection
            var container = new UnityContainer();
            container.RegisterType<ICardService, CardService>();
            container.RegisterType<ICardRepositoy, CardRepositoy>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            config.DependencyResolver = new UnityResolver(container);



            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
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
