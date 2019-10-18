using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Scheduler.Models;
using Scheduler.Pages;
using Scheduler.ScheduleService;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
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
        public Command<object> AddRecordCommand { get; set; }

        public MainPageViewModel(INavigation navigation)
        {
            DeleteCommand = new Command(() => OnDeleteTapped());
            AddRecordCommand = new Command<object>(OnAddRecordTapped);
            _schedulerService = new SchedulerService<ScheduleRecord>();

            Navigation = navigation;

            InitializeList();
        }

        public void InitializeList()
        {
            _schedulerService.AddObjectToList(new ScheduleRecord { Title = "FirstMoqTitle", ExpirationTime = DateTime.Now, TextBody = "asdfsfasfdasdfas" });
            _schedulerService.AddObjectToList(new ScheduleRecord { Title = "SecondMoqTitle", ExpirationTime = DateTime.Now, TextBody = "asdfsfasfdasfadsfadsfadsfsadasdfasdsd" });

            ListOfItems = new ObservableCollection<ScheduleRecord>(_schedulerService.GetAll());
        }

        private void OnDeleteTapped()
        {
           // if(ShowAlert("Do you want to delete the record?", "Loh", "Yes", "No").Result)
            _schedulerService.DeleteObject(SelectedItem);
            ListOfItems.Remove(SelectedItem);
        }

        private void OnAddRecordTapped(object obj)
        {
            ReturnAddRecordPage();
        }

        private void ReturnAddRecordPage()
        {
            Navigation.PushModalAsync(new AddRecordPage(this));
        }

        private async Task<bool> ShowAlert(string question, string message, string var1, string var2)
        {
             return await Application.Current.MainPage.DisplayAlert(question, message, var1, var2);
        }

    }
}
