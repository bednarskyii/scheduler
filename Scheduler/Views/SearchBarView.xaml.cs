using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Scheduler.Views
{
    public partial class SearchBarView : ContentView
    {
        public static readonly BindableProperty SelectedTitleProperty = BindableProperty.Create(
                                                 propertyName: "SelectedTitle",
                                                 returnType: typeof(string),
                                                 declaringType: typeof(SearchBarView),
                                                 defaultBindingMode: BindingMode.OneWayToSource);

        public string SelectedTitle
        {
            get { return (string)GetValue(SelectedTitleProperty); }
            set { SetValue(SelectedTitleProperty, value); }
        }

        public SearchBarView()
        {
            InitializeComponent();
        }
    }
}
