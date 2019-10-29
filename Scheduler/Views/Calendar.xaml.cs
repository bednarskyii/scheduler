using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Scheduler.Views
{
    public partial class Calendar : ContentView
    {
        private DateTime _selectedDate { get; set; } = DateTime.Now;
        private DateTime _selectedDay { get; set; } = DateTime.Now;

        public static readonly BindableProperty SelectedDayProperty = BindableProperty.Create(
                                                         propertyName: "SelectedDay",
                                                         returnType: typeof(DateTime),
                                                         declaringType: typeof(Calendar),
                                                         defaultBindingMode: BindingMode.OneWayToSource,
                                                         propertyChanged: OnSelectedDayChanged);

        public DateTime SelectedDay
        {
            get { return (DateTime)GetValue(SelectedDayProperty); }
            set { SetValue(SelectedDayProperty, value); }
        }

        public Calendar()
        {
            InitializeComponent();
            FillCalendar(_selectedDate.Month, _selectedDate.Year);
        }

        private void FillCalendar(int month, int year)
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
                            BackgroundColor = currentDay == DateTime.Today ? Color.LightSkyBlue : Color.White,
                            Text = currentDay.Day.ToString(),
                            TextColor = currentDay.Month == month ? Color.Black : Color.Gray,
                            CommandParameter = currentDay.Date
                        };

                    if (j == 5 || j == 6)
                        button.TextColor = Color.Gray;

                    button.Clicked += OnDayClicked;

                    CalendarArea.Children.Add(button, j, i);
                }
            }
        }

        private void OnNextMonthClicked(object sender, EventArgs e)
        {
            _selectedDate = _selectedDate.AddMonths(1) ;
            CalendarArea.Children.Clear();
            FillCalendar(_selectedDate.Month, _selectedDate.Year);
        }

        private void OnPreviousMonthClicked(object sender, EventArgs e)
        {
            _selectedDate = _selectedDate.AddMonths(-1);
            CalendarArea.Children.Clear();
            FillCalendar(_selectedDate.Month, _selectedDate.Year);
        }

        private void OnDayClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            DateTime date = (DateTime)button.CommandParameter;
            SelectedDay = date;
        }

        private static void OnSelectedDayChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //q
        }

    }
}
