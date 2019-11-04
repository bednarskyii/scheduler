using System;
using System.Collections.Generic;
using Scheduler.ViewModel;
using Xamarin.Forms;

namespace Scheduler.Pages
{
    public partial class ExperementalPage : ContentPage
    {
        public ExperementalPage()
        {
            InitializeComponent();

            BindingContext = new ExperementalPageViewModel();
        }
    }
}
