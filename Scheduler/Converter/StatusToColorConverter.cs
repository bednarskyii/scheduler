using System;
using System.Globalization;
using Scheduler.Enums;
using Xamarin.Forms;

namespace Scheduler.Converter
{
    public class StatusToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((RecordStatuses)value)
            {
                case RecordStatuses.Canceled:
                    return Color.Red;
                case RecordStatuses.Done:
                    return Color.Green;
                case RecordStatuses.InProgress:
                    return Color.Chocolate;
                case RecordStatuses.OnHold:
                    return Color.Gray;
                case RecordStatuses.Scheduled:
                    return Color.Blue;
                default:
                    return Color.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
