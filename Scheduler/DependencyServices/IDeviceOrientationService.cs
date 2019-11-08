using System;
using Xamarin.Forms.Internals;

namespace Scheduler.DependencyServices
{
    public interface IDeviceOrientationService
    {
        DeviceOrientation GetOrientation(); 
    }
}
