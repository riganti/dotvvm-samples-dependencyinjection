# Autofac with DotVVM and ASP.NET Core

In `Startup.cs`, we replace the default `IServiceProvider` with a custom implementation.
This is done by defining a `ConfigureServices` method that returns an `IServiceProvider`, as explained in the [ASP.NET Core documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1#default-service-container-replacement).

This approach replaces the default Dependency Injection in the whole application. Thanks to this, implementing a custom `IViewModelLoader` is not needed and dependency injection also works for `@service` directives.

```cs
public IServiceProvider ConfigureServices(IServiceCollection services)
{
    services.AddDataProtection();
    services.AddAuthorization();
    services.AddWebEncoders();
    services.AddDotVVM();

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
```

The `AutofacServiceProvider` implementation from `Autofac.Extensions.DependencyInjection` is used, making the configuration very simple. Make sure you register all viewmodel dependencies and static command services in the Autofac container.