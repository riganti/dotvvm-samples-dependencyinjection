using Autofac;
using DotVVM.Framework.Hosting;

namespace OWINAutofacSample
{

    public static class AutofacUtils
    {
        public static ILifetimeScope GetOrCreateScope(IDotvvmRequestContext context, IContainer container)
        {
            var scope = context.HttpContext.GetItem<ILifetimeScope>(typeof(AutofacViewModelLoader).FullName);
            if (scope == null)
            {
                scope = container.BeginLifetimeScope();
                context.HttpContext.SetItem(typeof(AutofacViewModelLoader).FullName, scope);
            }

            return scope;
        }

        public static void DisposeScope(IDotvvmRequestContext context)
        {
            var scope = context.HttpContext.GetItem<ILifetimeScope>(typeof(AutofacViewModelLoader).FullName);
            if (scope != null)
            {
                scope.Dispose();
                context.HttpContext.SetItem(typeof(AutofacViewModelLoader).FullName, (ILifetimeScope) null);
            }
        }
    }
}