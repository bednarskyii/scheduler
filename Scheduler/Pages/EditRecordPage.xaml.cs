using System;
using System.Collections.Generic;
using Scheduler.Models;
using Scheduler.ViewModel;
using Xamarin.Forms;

namespace Scheduler.Pages
{
    public partial class EditRecordPage : ContentPage
    {
        public EditRecordPage(ListViewPageViewModel pg, SingleDateRecord item)
        {
            InitializeComponent();
            BindingContext = new EditPageViewModel(Navigation, pg, item);
        }
    }
}
