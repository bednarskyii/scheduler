using System;
using System.Collections.Generic;
using Scheduler.Models;
using Scheduler.ScheduleService;
using Xamarin.Forms;

namespace Scheduler
{
    public partial class SchedulerPage : ContentPage
    {
        private  SScheduleService<ScheduleRecord> _schedulerService;

        public SchedulerPage()
        {
            _schedulerService = new SchedulerService<ScheduleRecord>();
            InitializeComponent();
            ShowListOfRecords();
        }

        public void ShowListOfRecords()
        {
            Label header = new Label
            {
                Text = "TODO List",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            _schedulerService.AddObjectToList(new ScheduleRecord { Title = "FirstTitle", ExpirationTime = DateTime.Now, TextBody = "asdfsfasfdasdfas" });
            _schedulerService.AddObjectToList(new ScheduleRecord { Title = "SecondTitle", ExpirationTime = DateTime.Now , TextBody = "asdfsfasfdasfadsfadsfadsfsadasdfasdsd" });

            //string[] phones = { "iPhone 7", "Samsung Galaxy S8", "Huawei P10", "LG G6" };
            List<ScheduleRecord> records = _schedulerService.GetAll();
            List<string> titles = new List<string>();
            List<string> bodies = new List<string>();

            foreach (ScheduleRecord record in records)
            {
                titles.Add(record.Title);
                bodies.Add(record.TextBody);
            }


            ListView listView = new ListView
            {
                // определяем источник данных
                ItemsSource = titles
            };

            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            Content = new StackLayout { Children = { header, listView } };

        }
    }
}
