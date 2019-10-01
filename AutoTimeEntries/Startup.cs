using Owin;
using System.Web.Http;

namespace GSAutoTimeEntries
{
    public class Startup
    {
        // Se for usar a fetchApi para fazer as requests, habilitar 'Cors' em 'Startup.config' e adicionar o attribute 'EnableCors' 
        // nos Controllers desejados colocando os endpoints de onde se espera as request, caso seja pra habilitar todos colocar '*'
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    }
}