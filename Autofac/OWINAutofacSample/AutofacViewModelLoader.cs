using System;
using Autofac;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Serialization;

public class AutofacViewModelLoader : DefaultViewModelLoader
{
    private readonly IContainer container;

    public AutofacViewModelLoader(IContainer container)
    {
        this.container = container;
    }

    protected override object CreateViewModelInstance(Type viewModelType, IDotvvmRequestContext context)
    {
        return container.Resolve(viewModelType);
    }

    public override void DisposeViewModel(object instance)
    {
        // TODO: Can we dispose in any way?
        // This works, but might lead to memory leaks as the lifetime is the whole application
        base.DisposeViewModel(instance);
    }
}
