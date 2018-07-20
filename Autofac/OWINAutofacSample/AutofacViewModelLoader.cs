using System;
using System.Web;
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
        var scope = container.BeginLifetimeScope();
        context.HttpContext.SetItem(typeof(AutofacViewModelLoader).FullName, scope);

        return scope.Resolve(viewModelType);
    }

    public override void DisposeViewModel(object instance)
    {
        var scope = HttpContext.Current.GetOwinContext().GetDotvvmContext().HttpContext.GetItem<ILifetimeScope>(typeof(AutofacViewModelLoader).FullName);
        scope?.Dispose();
    }
}
