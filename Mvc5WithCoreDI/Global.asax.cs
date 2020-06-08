using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mvc5WithCoreDI.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mvc5WithCoreDI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var services = new ServiceCollection();
            ConfigureServices(services);

            var resolver = new DotnetCoreDIDefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolver);
        }

        private void ConfigureServices(ServiceCollection services)
        {
            var controllers = typeof(MvcApplication).Assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => typeof(IController).IsAssignableFrom(t))
                .Where(t => t.Name.EndsWith("Controller"));

            foreach (var controller in controllers)
            {
                services.AddTransient(controller);
            }

            services.AddScoped<IHelloService, HelloService>();

            services.AddHttpClient();

            services.AddLogging(builder=>
            {
                builder.AddDebug();
                builder.SetMinimumLevel(LogLevel.Trace);
            });
        }
    }
}
