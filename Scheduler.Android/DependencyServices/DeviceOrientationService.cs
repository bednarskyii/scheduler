using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Scheduler.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(Scheduler.Droid.DependencyServices.DeviceOrientationService))]
namespace Scheduler.Droid.DependencyServices
{
    public class DeviceOrientationService : IDeviceOrientationService
    {
        public DeviceOrientation GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            SurfaceOrientation orientation = windowManager.DefaultDisplay.Rotation;
            bool isLandscape = orientation == SurfaceOrientation.Rotation90 ||
                orientation == SurfaceOrientation.Rotation270;
            return isLandscape ? DeviceOrientation.Landscape : DeviceOrientation.Portrait;
        }
    }
}
