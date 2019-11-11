using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Scheduler.Data;
using Scheduler.Models;
using Xamarin.Forms;

namespace Scheduler.ViewModel
{
    public class SearchPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IDatabaseRepository _database;
        private ObservableCollection<SingleDateRecord> _listOfItems;
        private bool _isListViewVisible;
        private bool _isEmptyImageVisible;
        private string _queryText;


        public string DisplayTitle => $"Entered title: {_queryText}";

        public string QueryText
        {
            get => _queryText;
            set
            {
                _queryText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QueryText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayTitle)));
            }
        }

        public ObservableCollection<SingleDateRecord> ListOfItems
        {
            get
            {
                return _listOfItems;
            }
            set
            {
                _listOfItems = value;
                if (_listOfItems == null)
                {
                    IsEmptyImageVisible = true;
                    IsListViewVisible = false;
                }
                else
                {
                    IsEmptyImageVisible = false;
                    IsListViewVisible = true;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListOfItems)));
            }
        }

        public bool IsListViewVisible
        {
            get => _isListViewVisible;
            set
            {
                _isListViewVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsListViewVisible)));
            }
        }

        public bool IsEmptyImageVisible
        {
            get => _isEmptyImageVisible;
            set
            {
                _isEmptyImageVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEmptyImageVisible)));
            }
        }

        public SearchPageViewModel()
        {
            _database = new DatabaseRepository();

            InitializeList();
        }

        public async Task InitializeList()
        {
            ListOfItems = new ObservableCollection<SingleDateRecord>(await _database.GetRecordsAsync(null,"F"));
        }
    }
}
