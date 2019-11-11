using System;
using System.Collections.Generic;
using Scheduler.ViewModel;
using Xamarin.Forms;

namespace Scheduler.Pages
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            BindingContext = new SearchPageViewModel();
            InitializeComponent();
        }
    }
}
