using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using FoodOrder;
using FoodOrder.DataAccess;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.Mvc;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace FoodOrder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            
            //ModelValidatorProviders.Providers.Add(new DataAnnotationsModelValidatorProvider());

            //app.UseNinjectMiddleware(() => NinjectWebCommon.Bootstrapper.Kernel);
            //var webApiConfiguration = new HttpConfiguration();
            //webApiConfiguration.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional, controller = "values" });


            app.UseNinjectMiddleware(CreateKernel);

            ConfigureAuth(app);

            //GlobalConfiguration.Configuration.ServiceResolver.SetResolver(new NinjectDependencyResolver(kernel));
            //ModelValidatorProviders.Providers.Add(new DataAnnotationsModelValidatorProvider());


        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Load("FoodOrder*.dll");

                kernel.Bind<IOwinContext>().ToMethod(context => HttpContext.Current.GetOwinContext());
                kernel.Bind<ApplicationUserManager>().ToSelf();

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}