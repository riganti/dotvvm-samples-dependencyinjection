using System;
using System.Reflection;
using System.Web.Hosting;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using OWINAutofacSample.Services;

[assembly: OwinStartup(typeof(OWINAutofacSample.Startup))]
namespace OWINAutofacSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;

            ConfigureAuth(app);

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(
                applicationPhysicalPath,
                debug: IsInDebugMode(),
                serviceProviderFactoryMethod: CreateServiceProvider
            );

            // use static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)
            });
        }
        private IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            // Create the Autofac container builder
            var builder = new ContainerBuilder();

            // Find all modules with container configuration in current assembly
            builder.RegisterAssemblyModules(typeof(Startup).GetTypeInfo().Assembly);

            // Combine the rules with the services already registered in the IServiceCollection
            builder.Populate(services);

            // Add your own services if not provided in a Module, this should be done after Populate
            builder.RegisterType<SampleLoggingService>()
                .As<ILoggingService>()
                .InstancePerLifetimeScope();

            // Create and return the container
            var applicationContainer = builder.Build();
            return new AutofacServiceProvider(applicationContainer);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
        }

        private bool IsInDebugMode()
        {
#if !DEBUG
			return false;
#endif
            return true;
        }
    }
}
