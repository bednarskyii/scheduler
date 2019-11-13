using System;
using System.Collections.Generic;
using Scheduler.ViewModel;
using Xamarin.Forms;

namespace Scheduler.Pages
{
    public partial class SearchItemsPage : ContentPage
    {
        public SearchItemsPage()
        {
            BindingContext = new SearchPageViewModel();
            InitializeComponent();
        }
    }
}
