# Autofac with DotVVM and OWIN

In `Startup.cs`, we replace the default `IServiceProvider` for DotVVM with a custom implementation. This is very similar to the ASP.NET Core method `ConfigureServices()` that may be defined in its Startup that is also used for providing your own `IServiceProvider`.

This approach replaces the default Dependency Injection in the whole application. Thanks to this, implementing a custom `IViewModelLoader` is not needed and dependency injection also works for `@service` directives.

```cs
var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(
    applicationPhysicalPath,
    serviceProviderFactoryMethod: CreateServiceProvider
);
```

The `AutofacServiceProvider` implementation from `Autofac.Extensions.DependencyInjection` is used, making the configuration very simple:

```cs
private IServiceProvider CreateServiceProvider(IServiceCollection services)
{
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
```

Make sure you register all viewmodel dependencies and static command services in the Autofac container.