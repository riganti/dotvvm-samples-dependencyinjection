using System;
using System.Web;
using Autofac;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Serialization;

namespace OWINAutofacSample
{

    public class AutofacViewModelLoader : DefaultViewModelLoader
    {
        private readonly IContainer container;

        public AutofacViewModelLoader(IContainer container)
        {
            this.container = container;
        }

        protected override object CreateViewModelInstance(Type viewModelType, IDotvvmRequestContext context)
        {
            var scope = AutofacUtils.GetOrCreateScope(context, container);
            return scope.Resolve(viewModelType);
        }

        public override void DisposeViewModel(object instance)
        {
            var context = HttpContext.Current.GetOwinContext().GetDotvvmContext();
            AutofacUtils.DisposeScope(context);
        }
    }

}