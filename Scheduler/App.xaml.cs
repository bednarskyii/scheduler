﻿using System;
using System.IO;
using Scheduler.Data;
using Scheduler.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scheduler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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
