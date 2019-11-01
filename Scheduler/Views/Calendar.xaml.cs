using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Services;
using Xamarin.Forms;

namespace Scheduler.Views
{
    public partial class Calendar : ContentView
    {
        private Button _selectedDayButton;
        private DateTime _selectedDate { get; set; } = DateTime.Now;
        private IScheduleService _scheduleService;

        public static readonly BindableProperty SelectedDayProperty = BindableProperty.Create(
                                                         propertyName: "SelectedDay",
                                                         defaultValue: DateTime.Today,
                                                         returnType: typeof(DateTime),
                                                         declaringType: typeof(Calendar),
                                                         defaultBindingMode: BindingMode.OneWayToSource);

        public DateTime SelectedDay
        {
            get { return (DateTime)GetValue(SelectedDayProperty); }
            set { SetValue(SelectedDayProperty, value); }
        }

        public Calendar()
        {
            _scheduleService = new SchedulerService();
            InitializeComponent();
            FillCalendar(_selectedDate.Month, _selectedDate.Year);
        }

        private async Task FillCalendar(int month, int year)
        {
            int counter = 0;
            DateTime monthStart = new DateTime(year, month, 1);
            DayOfWeek firstDay = monthStart.DayOfWeek;
            
            MonthLable.Text = _selectedDate.ToString("y");

            if (firstDay != DayOfWeek.Sunday)
            {
                counter -= (int)firstDay;
            }
            else
            {
                counter -= 7;
            }

            var currentDay = monthStart.AddDays(counter);

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    currentDay = currentDay.AddDays(1);

                    Button button = new Button()
                    {
                        BackgroundColor = Color.Transparent,
                        BorderColor = currentDay == DateTime.Today ? Color.Black : Color.Transparent,
                        BorderWidth = 2.0,
                        CornerRadius = 10,
                        Text = currentDay.Day.ToString(),
                        TextColor = currentDay.Month == month ? Color.Black : Color.Gray,
                        CommandParameter = currentDay.Date
                     };

                    if (j == 5 || j == 6)
                        button.TextColor = Color.Gray;

                    button.Clicked += OnDayClicked;

                    if (await _scheduleService.IsDateHasRecords(currentDay.Date))
                    {
                        //TODO change this logic to more effective 
                        CalendarArea.Children.Add(new Label { Text = ".", TextColor = Color.Red, Margin = 3, FontSize = 30, HorizontalOptions = LayoutOptions.CenterAndExpand }, j, i);
                    }

                    CalendarArea.Children.Add(button, j, i);

                }
            }
        }

        private void OnNextMonthClicked(object sender, EventArgs e)
        {
            _selectedDate = _selectedDate.AddMonths(1) ;
            CleanCalendar();
        }

        private void OnPreviousMonthClicked(object sender, EventArgs e)
        {
            _selectedDate = _selectedDate.AddMonths(-1);
            CleanCalendar();
        }

        private void OnDayClicked(object sender, EventArgs e)
        {
            if(_selectedDayButton != null)
            {
                _selectedDayButton.BackgroundColor = Color.Transparent;
            }
            _selectedDayButton = (Button)sender;
            DateTime date = (DateTime)_selectedDayButton.CommandParameter;
            _selectedDayButton.BackgroundColor = Color.DarkGray;
            SelectedDay = date;
        }

        private void CleanCalendar()
        {
            CalendarArea.Children.Clear();
            FillCalendar(_selectedDate.Month, _selectedDate.Year);
        }

    }
}
