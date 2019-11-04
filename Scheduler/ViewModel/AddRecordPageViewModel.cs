using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Scheduler.Models;
using Scheduler.Services;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class AddRecordPageViewModel : INotifyPropertyChanged
    {
        private IScheduleService _scheduleService;
        private ListViewPageViewModel _pg;
        private string _title = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation { get; set; }
        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(DisplayTitle));
            }}

        public string DisplayTitle => $"Entered title: {_title}";

        public string Text { get;  set; }
        public DateTime Date { get; set; } 
        public DateTime MinDate { get; set; } = DateTime.Now;
        public TimeSpan SelectedStartTime { get; set; }
        public TimeSpan SelectedEndTime { get; set; }

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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