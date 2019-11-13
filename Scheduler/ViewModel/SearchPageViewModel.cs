using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
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
        private bool _isStartImageVisible = true;

        private string _queryText;

        public string QueryText
        {
            get => _queryText;
            set
            {
                _queryText = value;
                if (!string.IsNullOrEmpty(_queryText)) 
                    InitializeList(null, _queryText);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QueryText)));
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
                    IsStartImageVisible = true;
                    IsEmptyImageVisible = false;
                    IsListViewVisible = false;
                }
                if ( _listOfItems.Count == 0)
                {
                    IsEmptyImageVisible = true;
                    IsListViewVisible = false;
                    IsStartImageVisible = false;
                }
                else
                {
                    IsListViewVisible = true;
                    IsEmptyImageVisible = false;
                    IsStartImageVisible = false;
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

        public bool IsStartImageVisible
        {
            get => _isStartImageVisible;
            set
            {
                _isStartImageVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsStartImageVisible)));
            }
        }

        public SearchPageViewModel()
        {
            _database = new DatabaseRepository();
        }

        public async Task InitializeList(DateTime? month, string title)
        {
            ListOfItems = new ObservableCollection<SingleDateRecord>(await _database.GetRecordsAsync(month, title));
        }

    }
}
