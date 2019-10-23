using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Pages;
using Scheduler.ViewModel;
using Xamarin.Forms;

namespace Scheduler
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            Master = new MainPage();
            Detail = new NavigationPage(new ListViewPage());

            BindingContext = new MainPageViewModel(Navigation);
        }
    }
}
