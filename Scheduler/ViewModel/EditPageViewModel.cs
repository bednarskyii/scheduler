using System;
using System.Collections.Generic;
using System.Globalization;
using Scheduler.Converter;
using Scheduler.Data;
using Scheduler.Enums;
using Scheduler.Models;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class EditPageViewModel
    {
        public IList<RecordStatuses> ListOfStatuses { get; set; }
        public INavigation Navigation { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string SelectedStatus { get; set; }
        public int SelectedStatusIndex { get; set; }
        public DateTime? Date { get; set; }
        public DateTime MinDate { get; set; } = DateTime.Now;
        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; set; }

        private IDatabaseRepository _database;
        private IValueConverter _statusConverter;
        private IList<RecordStatuses> _requiredStatuses;
        private SingleDateRecord _currentObject;
        private ListViewPageViewModel _pg;

        public EditPageViewModel(INavigation navigation, ListViewPageViewModel pg, SingleDateRecord currentObject)
        {
            _database = new DatabaseRepository();
            _statusConverter = new EnumToStringWithSpacesConverter();
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
            _database.DeleteItemByIdAsync(_currentObject.Id);

            SingleDateRecord updatedRecord = GetUpdatedObject(_currentObject);
            _database.SaveItemAsync(updatedRecord);
            ReturnToPreviousPage();
        }

        private void OnCancelTapped(object obj)
        {
            ReturnToPreviousPage();
        }

        private void ReturnToPreviousPage()
        {
            Navigation.PopModalAsync();
            _pg.InitializeListWithDate();
        }

        private SingleDateRecord GetUpdatedObject(SingleDateRecord curObject)
        {
            curObject.ExpirationTime = Date;
            curObject.TextBody = Text;
            curObject.Title = Title;

            curObject.Status = (RecordStatuses)_statusConverter.ConvertBack(SelectedStatus, typeof(RecordStatuses), null, CultureInfo.InvariantCulture);

            return curObject;
        }
    }
}
