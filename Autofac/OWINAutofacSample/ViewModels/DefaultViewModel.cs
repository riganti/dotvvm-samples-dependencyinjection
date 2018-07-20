﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using OWINAutofacSample.Services;

namespace OWINAutofacSample.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
		public string Title { get; set;}

		public DefaultViewModel(ILoggingService logger)
		{
            logger.Log("Default View Model created.");

			Title = "Hello from DotVVM!";
		}
    }
}
