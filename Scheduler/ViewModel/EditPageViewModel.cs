using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Scheduler.Converter;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.ScheduleService;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class EditPageViewModel
    {
        public INavigation Navigation { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime MinDate { get; set; } = DateTime.Now;
        public string SelectedStatus { get; set; }
        public IList<RecordStatuses> ListOfStatuses { get; set; }


        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }

        private IValueConverter _statusConverter;
        private ScheduleRecord _currentObject;
        private ListViewPageViewModel _pg;
        private IScheduleService _scheduleService;
        private IList<RecordStatuses> _requiredStatuses;


        public EditPageViewModel(INavigation navigation, ListViewPageViewModel pg, ScheduleRecord currentObject)
        {
            _statusConverter = new EnumToStringWithSpacesConverter();
            _scheduleService = new SchedulerService();
            _requiredStatuses = new List<RecordStatuses>();
            _currentObject = currentObject;
            _pg = pg;
            Navigation = navigation;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            SelectRequiredStatuses();

            SaveCommand = new Command(OnSaveTapped);
            CancelCommand = new Command(OnCancelTapped);
            Title = _currentObject.Title;
            Text = _currentObject.TextBody;
            Date = _currentObject.ExpirationTime;
            SelectedStatus = _currentObject.Status.ToString();
            ListOfStatuses = _requiredStatuses;

        }

        private void SelectRequiredStatuses()
        {
            _requiredStatuses.Add(RecordStatuses.Canceled);
            _requiredStatuses.Add(RecordStatuses.Done);
            _requiredStatuses.Add(RecordStatuses.InProgress);
            _requiredStatuses.Add(RecordStatuses.OnHold);
            _requiredStatuses.Add(RecordStatuses.Scheduled);
        }

        private void OnSaveTapped(object obj)
        {
            _scheduleService.DeleteObject(_currentObject);

            ScheduleRecord updatedRecord = GetUpdatedObject(_currentObject);
            _scheduleService.AddObjectToList(updatedRecord);
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

        private ScheduleRecord GetUpdatedObject(ScheduleRecord curObject)
        {
            curObject.ExpirationTime = Date;
            curObject.TextBody = Text;
            curObject.Title = Title;

            curObject.Status = (RecordStatuses)_statusConverter.ConvertBack(SelectedStatus, typeof(RecordStatuses), null, CultureInfo.InvariantCulture);

            return curObject;
        }
    }
}
