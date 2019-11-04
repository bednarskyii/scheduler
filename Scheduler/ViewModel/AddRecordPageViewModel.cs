using System;
using System.Threading.Tasks;
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
        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } 
        public DateTime MinDate { get; set; } = DateTime.Now;
        public TimeSpan SelectedStartTime { get; set; }
        public TimeSpan SelectedEndTime { get; set; }


        public AddRecordPageViewModel(INavigation navigation, ListViewPageViewModel pg)
        {
            SaveCommand = new Command(() => OnSaveTapped());
            CancelCommand = new Command(() => OnCancelTapped());
            _scheduleService = new SchedulerService();
            Navigation = navigation;

            _pg = pg;
        }

        private async Task OnSaveTapped()
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                await _scheduleService.AddObjectToListAsync(new SingleDateRecord { Title = Title,
                                                                                   TextBody = Text,
                                                                                   Status = Enums.RecordStatuses.Scheduled,
                                                                                   ExpirationTime = Date.Date,
                                                                                   StartTime = Convert.ToDateTime(SelectedStartTime.ToString()),
                                                                                   EndTime = Convert.ToDateTime(SelectedEndTime.ToString())});
            }

            await ReturnToPreviousPage();
        }

        private async Task OnCancelTapped()
        {
            await ReturnToPreviousPage();
        }

        private async Task ReturnToPreviousPage()
        {
            await Navigation.PopModalAsync();
            await _pg.InitializeListWithDate();
        }
    }
}