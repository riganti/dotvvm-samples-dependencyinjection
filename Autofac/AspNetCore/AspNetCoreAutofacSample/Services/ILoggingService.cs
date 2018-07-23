using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace AspNetCoreAutofacSample.Services
{
	public interface ILoggingService
	{
		[AllowStaticCommand]
		void Log(string message);
	}
}