using Autofac;
using Autofac.Features.ResolveAnything;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using DotVVM.Framework.ViewModel.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DotVVM.Framework.Runtime;
using OWINAutofacSample.Services;

namespace OWINAutofacSample
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/default.dothtml");
            config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
        }

        public void ConfigureServices(IDotvvmServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SampleLoggingService>()
                .As<ILoggingService>()
                .InstancePerLifetimeScope();

            // The following is required to get Autofac to create instances of viewmodels
            // in AutofacViewModelLoader.
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            var container = builder.Build();
            services.Services.AddSingleton<IViewModelLoader>(s => new AutofacViewModelLoader(container));
            services.Services.AddSingleton<IStaticCommandServiceLoader>(s => new AutofacStaticCommandServiceLoader(container));
        }
    }
}
