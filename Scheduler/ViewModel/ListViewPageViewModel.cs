using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Scheduler.Models;
using Scheduler.Pages;
using Scheduler.ScheduleService;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class ListViewPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IScheduleService<ScheduleRecord> _schedulerService;
        private ObservableCollection<ScheduleRecord> _listOfItems;

        public INavigation Navigation { get; set; }

        public ObservableCollection<ScheduleRecord> ListOfItems
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

        public ScheduleRecord SelectedItem { get; set; }
        public Command DeleteCommand { get; set; }
        public Command AddRecordCommand { get; set; }
        public Command EditRecordCommand { get; set; }

        public ListViewPageViewModel(INavigation navigation)
        {
            DeleteCommand = new Command(() => OnDeleteTapped());
            AddRecordCommand = new Command(OnAddRecordTapped);
            EditRecordCommand = new Command(OnEditRecordTapped);
            _schedulerService = new SchedulerService<ScheduleRecord>();

            Navigation = navigation;

            InitializeList();
        }

        public void InitializeList()
        {
            ListOfItems = new ObservableCollection<ScheduleRecord>(_schedulerService.GetAll());

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

                var res = await GetConfirmResult(config);

                if (res)
                {
                    _schedulerService.DeleteObject(SelectedItem);
                    ListOfItems.Remove(SelectedItem);
                }
            }
            else
            {
                UserDialogs.Instance.Alert("Select an item, please");
            }
        }

        private void OnAddRecordTapped()
        {
            Navigation.PushModalAsync(new AddRecordPage(this));
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

        private async Task<bool> GetConfirmResult(ConfirmConfig config)
        {
            return await UserDialogs.Instance.ConfirmAsync(config);
        }
    }
}
