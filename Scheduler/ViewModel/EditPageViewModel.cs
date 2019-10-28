using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IList<string> ListOfStatuses { get; set; }


        public Command<object> SaveCommand { get; set; }
        public Command<object> CancelCommand { get; set; }


        private ScheduleRecord _currentObject;
        private ListViewPageViewModel _pg;
        private IScheduleService _scheduleService;


        public EditPageViewModel(INavigation navigation, ListViewPageViewModel pg, ScheduleRecord currentObject)
        {
            _scheduleService = new SchedulerService();
            _currentObject = currentObject;
            _pg = pg;
            Navigation = navigation;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            SaveCommand = new Command<object>(OnSaveTapped);
            CancelCommand = new Command<object>(OnCancelTapped);
            Title = _currentObject.Title;
            Text = _currentObject.TextBody;
            Date = _currentObject.ExpirationTime;
            SelectedStatus = _currentObject.Status.ToString();
            ListOfStatuses = AddWhiteSpaces( Enum.GetNames(typeof(RecordStatuses)).ToList() );
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

            SelectedStatus = SelectedStatus.Replace(" ", "");
            Enum.TryParse(SelectedStatus, out RecordStatuses statusValue);
            curObject.Status = statusValue;

            return curObject;
        }

        private List<string> AddWhiteSpaces(List<string> curList)
        {
            List<string> modefiedList = new List<string>();
            foreach (string item in curList)
            {
                switch (item)
                {
                    case "InProgress":
                        modefiedList.Add("In Progress");
                        break;
                    case "OnHold":
                        modefiedList.Add("On Hold");
                        break;
                    default:
                        modefiedList.Add(item);
                        break;
                }
            }
            return modefiedList;
        }
    }
}
