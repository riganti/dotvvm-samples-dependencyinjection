using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace OWINAutofacSample.Services
{

    public class SampleLoggingService : ILoggingService
    {

        [AllowStaticCommand]
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }

}