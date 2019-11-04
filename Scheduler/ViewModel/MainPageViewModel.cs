using System;
using Scheduler.Models;
using Scheduler.Pages;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class MainPageViewModel 
    {
        public INavigation Navigation { get; set; }

        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

    }
}
