using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Data;
using Xamarin.Forms;

namespace Scheduler.Views
{
    public partial class Calendar : ContentView
    {
        private readonly IDatabaseRepository _database;
        private Button _selectedDayButton;
        private List<DateTime> _datesWithRecords;


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
            _datesWithRecords = new List<DateTime>();
            _database = new DatabaseRepository();
            InitializeComponent();
            FillCalendar(SelectedDay.Month, SelectedDay.Year);
        }

        private async Task FillCalendar(int month, int year)
        {
            int counter = 0;
            DateTime monthStart = new DateTime(year, month, 1);
            DayOfWeek firstDay = monthStart.DayOfWeek;
            
            MonthLable.Text = SelectedDay.ToString("y");

            if (firstDay != DayOfWeek.Sunday)
            {
                counter -= (int)firstDay;
            }
            else
            {
                counter -= 7;
            }

            DateTime currentDay = monthStart.AddDays(counter);

            _datesWithRecords = await _database.GetAllDatesWithRecordsByMonth(SelectedDay);

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

                    if (_datesWithRecords.Contains(currentDay.Date))
                    {
                        CalendarArea.Children.Add(new Label { Text = ".", TextColor = (Color)Application.Current.Resources["DayWithRecordsDotColor"], Margin = 3, FontSize = 30, HorizontalOptions = LayoutOptions.CenterAndExpand }, j, i);
                    }

                    CalendarArea.Children.Add(button, j, i);

                }
            }
        }

        private void OnNextMonthClicked(object sender, EventArgs e)
        {
            SelectedDay = SelectedDay.AddMonths(1) ;
            UpdateCalendar();
        }

        private void OnPreviousMonthClicked(object sender, EventArgs e)
        {
            SelectedDay = SelectedDay.AddMonths(-1);
            UpdateCalendar();
        }

        private void OnDayClicked(object sender, EventArgs e)
        {
            if(_selectedDayButton != null)
            {
                _selectedDayButton.BackgroundColor = Color.Transparent;
            }
            _selectedDayButton = (Button)sender;
            _selectedDayButton.BackgroundColor = Color.DarkGray;
            DateTime date = (DateTime)_selectedDayButton.CommandParameter;
            SelectedDay = date;
        }

        private void UpdateCalendar()
        {
            CalendarArea.Children.Clear();
            FillCalendar(SelectedDay.Month, SelectedDay.Year);
        }

    }
}
