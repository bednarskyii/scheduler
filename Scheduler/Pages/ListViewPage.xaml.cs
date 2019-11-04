using System;
using System.Collections.Generic;
using Scheduler.ViewModel;
using Xamarin.Forms;

namespace Scheduler.Pages
{
    public partial class ListViewPage : ContentPage
    {
        public ListViewPage()
        {
            InitializeComponent();

            BindingContext = new ListViewPageViewModel(Navigation);
        }
    }
}
