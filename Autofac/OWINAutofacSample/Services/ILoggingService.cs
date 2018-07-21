using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace OWINAutofacSample.Services
{
    public interface ILoggingService
    {
        [AllowStaticCommand]
        void Log(string message);
    }
}