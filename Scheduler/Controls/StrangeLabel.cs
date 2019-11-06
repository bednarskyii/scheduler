using System;
using Xamarin.Forms;

namespace Scheduler.Controls
{
    public class StrangeLabel : Label
    {
        public static readonly BindableProperty CharCountProperty = BindableProperty.Create(
                                                         propertyName: "CharCount",
                                                         defaultValue: 0,
                                                         returnType: typeof(int),
                                                         declaringType: typeof(StrangeLabel));

        public int CharCount
        {
            get { return (int)GetValue(CharCountProperty); }
            set { SetValue(CharCountProperty, value); }
        }
    }
}