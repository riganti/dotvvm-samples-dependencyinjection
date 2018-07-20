using System;
using System.Collections.Generic;
using System.Linq;

namespace OWINAutofacSample.Services
{
    public interface ILoggingService
    {
        void Log(string message);
    }
}