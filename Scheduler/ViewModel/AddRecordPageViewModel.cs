using System;
using System.Threading.Tasks;
using Scheduler.Data;
using Scheduler.Models;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class AddRecordPageViewModel
    {
        private IDatabaseRepository _database;
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
            _database = new DatabaseRepository();
            SaveCommand = new Command(() => OnSaveTapped());
            CancelCommand = new Command(() => OnCancelTapped());
            Navigation = navigation;

            _pg = pg;
        }

        private async Task OnSaveTapped()
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                await _database.SaveItemAsync(new SingleDateRecord { Title = Title,
                                                                                   TextBody = Text,
                                                                                   Status = Enums.RecordStatuses.Scheduled,
                                                                                   ExpirationTime = Date,
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