using Scheduler.Pages;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class MainPageViewModel 
    {
        public INavigation Navigation { get; set; }
        public Command MoveToSchedule { get; set; }
        public Command MoveToAbout { get; set; }


        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            MoveToSchedule = new Command(OnMoveToSchedule);
            MoveToAbout = new Command(OnMoveToAbout);

        }

        private void OnMoveToSchedule()
        {
            // TODO need to fix
            Navigation.PushModalAsync(new ListViewPage());
        }

        private void OnMoveToAbout()
        {
            // TODO need to fix
            Navigation.PushModalAsync(new AboutPage());
        }

    }
}
