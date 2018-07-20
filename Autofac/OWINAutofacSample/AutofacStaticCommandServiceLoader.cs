﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime;

namespace OWINAutofacSample
{
    public class AutofacStaticCommandServiceLoader : DefaultStaticCommandServiceLoader
    {
        private readonly IContainer container;

        public AutofacStaticCommandServiceLoader(IContainer container)
        {
            this.container = container;
        }

        public override object GetStaticCommandService(Type serviceType, IDotvvmRequestContext context)
        {
            return container.Resolve(serviceType);
        }
    }
}