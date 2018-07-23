using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using DotVVM.Framework.Hosting;
using Autofac;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using AspNetCoreAutofacSample.Services;

namespace AspNetCoreAutofacSample
{
	public class Startup
	{

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddDataProtection();
			services.AddAuthorization();
			services.AddWebEncoders();
			services.AddDotVVM();

			// create the Autofac container builder
			var builder = new ContainerBuilder();

			// find all modules with container configuration in current assembly
			builder.RegisterAssemblyModules(typeof(Startup).GetTypeInfo().Assembly);

			// combine the rules with the services already registered in the IServiceCollection
			builder.Populate(services);

			// add your own services, this needs to be done after Populate
			builder.RegisterType<SampleLoggingService>()
				.As<ILoggingService>()
				.InstancePerLifetimeScope();

			// create and return the container
			var applicationContainer = builder.Build();
			return new AutofacServiceProvider(applicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();

			// use DotVVM
			var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

			// use static files
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(env.WebRootPath)
			});
		}
	}
}
