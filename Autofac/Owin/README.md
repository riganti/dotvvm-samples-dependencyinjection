# Autofac with DotVVM and OWIN

In `DotvvmStartup.cs`, we replace default `IViewModelLoader` and `IStaticCommandServiceLoader` with custom implementations:

```
services.Services.AddSingleton<IViewModelLoader>(s => new AutofacViewModelLoader(container));
services.Services.AddSingleton<IStaticCommandServiceLoader>(s => new AutofacStaticCommandServiceLoader(container));
``` 

These classes are responsible for creating a per-request scope, resolving components of the required types and disposing the scope when the request ends.

Make sure you register all viewmodels and static command services in the Autofac container.

