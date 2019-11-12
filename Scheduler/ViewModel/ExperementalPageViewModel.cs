using System;
using System.ComponentModel;
using Scheduler.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Scheduler.ViewModel
{
    public class ExperementalPageViewModel : INotifyPropertyChanged
    {
        private string _title;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Orientation { get; set; }

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

        public ExperementalPageViewModel()
        {
            IDeviceOrientationService service = DependencyService.Get<IDeviceOrientationService>();
            DeviceOrientation orientation = service.GetOrientation();
            Orientation = orientation.ToString();
        }
    }
}
