using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace AspNetCoreAutofacSample.Services
{

	public class SampleLoggingService : ILoggingService, IDisposable
	{

		[AllowStaticCommand]
		public void Log(string message)
		{
			Debug.WriteLine(message);
		}

		public void Dispose()
		{
			Debug.WriteLine("Service disposed");
		}
	}

}