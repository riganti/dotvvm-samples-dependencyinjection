using DotVVM.Framework.ViewModel;
using System;
using System.Diagnostics;

public class SampleLoggingService : ILoggingService
{
    [AllowStaticCommand]
    public void Log(string message)
    {
        Debug.WriteLine(message);
    }
}
