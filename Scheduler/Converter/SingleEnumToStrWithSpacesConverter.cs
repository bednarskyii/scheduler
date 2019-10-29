using System;
using System.Globalization;
using Scheduler.Enums;
using Xamarin.Forms;

namespace Scheduler.Converter
{
    public class SingleEnumToStrWithSpacesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((RecordStatuses)value)
            {
                case RecordStatuses.InProgress:
                    return "In Progress";
                case RecordStatuses.OnHold:
                    return "On Hold";
                default:
                    return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
