using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Scheduler.Views
{
    public partial class SearchBarView : ContentView
    {
        public Command OnCancel { get; set; }

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
            OnCancel = new Command(() => OnCancelClick());
            InitializeComponent();
        }

        private void OnCancelClick()
        {
            SelectedTitle = null;
        }
    }
}
