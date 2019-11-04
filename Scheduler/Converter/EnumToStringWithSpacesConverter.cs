using System;
using System.Collections.Generic;
using System.Globalization;
using Scheduler.Enums;
using Xamarin.Forms;

namespace Scheduler.Converter
{
    public class EnumToStringWithSpacesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IList<string> modefiedList = new List<string>();

            foreach (RecordStatuses item in (List<RecordStatuses>)value)
            {
                switch (item)
                {
                    case RecordStatuses.InProgress:
                        modefiedList.Add("In Progress");
                        break;
                    case RecordStatuses.OnHold:
                        modefiedList.Add("On Hold");
                        break;
                    default:
                        modefiedList.Add(item.ToString());
                        break;
                }
            }

            return modefiedList;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;
            status = status.Replace(" ", "");
            Enum.TryParse(status, out RecordStatuses statusValue);

            return statusValue;
        }
    }
}
