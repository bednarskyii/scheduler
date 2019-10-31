using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Scheduler.Models;
using Scheduler.Pages;
using Scheduler.Services;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class ListViewPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IScheduleService _schedulerService;
        private ObservableCollection<SingleDateRecord> _listOfItems;
        private DateTime _selectedDay = DateTime.Now.Date;

        public INavigation Navigation { get; set; }

        public ObservableCollection<SingleDateRecord> ListOfItems
        {
            get
            {
                return _listOfItems;
            }
            set
            {
                _listOfItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListOfItems)));
            }
        }

        public DateTime SelectedDay
        {
            set
            {
                _selectedDay = value;
                _ = InitializeListWithDate();
            }
            get
            {
                return _selectedDay;
            }
        }
        public SingleDateRecord SelectedItem { get; set; }
        public Command DeleteCommand { get; set; }
        public Command AddRecordCommand { get; set; }
        public Command EditRecordCommand { get; set; }

        public ListViewPageViewModel(INavigation navigation)
        {
            DeleteCommand = new Command(() => OnDeleteTapped());
            AddRecordCommand = new Command(() => OnAddRecordTapped());
            EditRecordCommand = new Command(OnEditRecordTapped);
            _schedulerService = new SchedulerService();

            Navigation = navigation;

            InitializeListWithDate();
        }

        public async Task InitializeListWithDate()
        {
             ListOfItems = new ObservableCollection<SingleDateRecord>(await _schedulerService.GetRecordAsync(_selectedDay));
        }

        private async Task OnDeleteTapped()
        {
            if (SelectedItem != null)
            {
                ConfirmConfig config = new ConfirmConfig()
                {
                    Message = "Delete the record?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                var res = await UserDialogs.Instance.ConfirmAsync(config);

                if (res)
                {
                    await _schedulerService.DeleteObjectAsync(SelectedItem.Id);
                    ListOfItems.Remove(SelectedItem);
                }
            }
            else
            {
                UserDialogs.Instance.Alert("Select an item, please");
            }
        }

        private async Task OnAddRecordTapped()
        {
            await Navigation.PushModalAsync(new AddRecordPage(this));
        }

        private void OnEditRecordTapped()
        {
            if (SelectedItem != null)
            {
                Navigation.PushModalAsync(new EditRecordPage(this, SelectedItem));
            }
            else
            {
                UserDialogs.Instance.Alert("Select an item, please");
            }
        }
    }
}
