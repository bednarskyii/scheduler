using System;
using System.Collections.Generic;
using Scheduler.ViewModel;
using Xamarin.Forms;

namespace Scheduler.Pages
{
    public partial class AddRecordPage : ContentPage
    {
        public AddRecordPage(ListViewPageViewModel pg)
        {
            BindingContext = new AddRecordPageViewModel(Navigation, pg);
            InitializeComponent();
        }
    }
}
