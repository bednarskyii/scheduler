using System;
using System.Globalization;
using System.IO;
using Plugin.Multilingual;
using Scheduler.Data;
using Xamarin.Forms;

namespace Scheduler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.DeviceCultureInfo;

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
