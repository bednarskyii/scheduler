using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Scheduler.Models;
using Scheduler.ScheduleService;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class MainPageViewModel
    {
        private IScheduleService<ScheduleRecord> _schedulerService;

        public ObservableCollection<ScheduleRecord> ListOfItems { get; set; }
        public Command<object> DeleteCommand { get; set; }

        public MainPageViewModel()
        {
            _schedulerService = new SchedulerService<ScheduleRecord>();
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            _schedulerService.AddObjectToList(new ScheduleRecord { Title = "FirstMoqTitle", ExpirationTime = DateTime.Now, TextBody = "asdfsfasfdasdfas" });
            _schedulerService.AddObjectToList(new ScheduleRecord { Title = "SecondMoqTitle", ExpirationTime = DateTime.Now, TextBody = "asdfsfasfdasfadsfadsfadsfsadasdfasdsd" });

            ListOfItems = new ObservableCollection<ScheduleRecord>(_schedulerService.GetAll());
            DeleteCommand = new Command<object>(OnDeleteTapped);
        }

        private void OnDeleteTapped(object obj)
        {
            var record = obj as ScheduleRecord;
            _schedulerService.DeleteObject(record);
        }

    }
}
