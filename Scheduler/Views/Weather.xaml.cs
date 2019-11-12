using Scheduler.ViewModel;
using Xamarin.Forms;

namespace Scheduler.Views
{
    public partial class Weather : ContentView
    {
        public Weather()
        {
            BindingContext = new WeatherViewModel();
            InitializeComponent();
        }
    }
    
}
