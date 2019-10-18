using Scheduler.Models;
using Scheduler.ScheduleService;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class AddRecordPageViewModel
    {
        private IScheduleService<ScheduleRecord> _scheduleService;
        private MainPageViewModel _pg;

        public INavigation Navigation { get; set; }
        public Command<object> SaveCommand { get; set; }
        public Command<object> CancelCommand { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public AddRecordPageViewModel(INavigation navigation, MainPageViewModel pg)
        {
            Navigation = navigation;
            _scheduleService = new SchedulerService<ScheduleRecord>();
            InitializeViewModel();

            _pg = pg;
        }

        private void InitializeViewModel()
        {
            SaveCommand = new Command<object>(OnSaveTapped);
            CancelCommand = new Command<object>(OnCancelTapped);
        }

        private void OnSaveTapped(object obj)
        {
            if(!string.IsNullOrWhiteSpace(Title))
            _scheduleService.AddObjectToList(new ScheduleRecord { Title = this.Title, TextBody = this.Text });
            ReturnToPreviousPage();
        }

        private void OnCancelTapped(object obj)
        {
            ReturnToPreviousPage();
        }

        private void ReturnToPreviousPage()
        {
            Navigation.PopModalAsync();
            _pg.InitializeList();
        }
    }
}