using System;
using System.ComponentModel;

namespace Scheduler.ViewModel
{
    public class ExperementalPageViewModel : INotifyPropertyChanged
    {
        private string _title;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged (string title)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(title));
        }

        public string Title
        {
            get => _title;
            set
            {
                if (value == _title)
                    return;

                _title = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(DisplayTitle));
            }
        }

        public string DisplayTitle => $"Entered title: {_title}";
    }
}
