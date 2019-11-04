using Scheduler.Models;
using Scheduler.Services;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class AddRecordPageViewModel
    {
        private IScheduleService _scheduleService;
        private ListViewPageViewModel _pg;

        public INavigation Navigation { get; set; }
        public Command<object> SaveCommand { get; set; }
        public Command<object> CancelCommand { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public AddRecordPageViewModel(INavigation navigation, ListViewPageViewModel pg)
        {
            SaveCommand = new Command<object>(OnSaveTapped);
            CancelCommand = new Command<object>(OnCancelTapped);
            _scheduleService = new SchedulerService();
            Navigation = navigation;

            _pg = pg;
        }

        private void OnSaveTapped(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                _scheduleService.AddObjectToList(new ScheduleRecord { Title = Title, TextBody = Text, Status = Enums.RecordStatuses.Scheduled });
            }

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