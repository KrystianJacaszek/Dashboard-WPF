using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DashboardLib.Models
{
    public class NewsModel : INotifyPropertyChanged
    {
        public NewsModel(NewsListEntry[] newsList)
        {
            this.newsList = new ObservableCollection<NewsListEntry>(newsList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private ObservableCollection<NewsListEntry> newsList = new ObservableCollection<NewsListEntry>();

        public string CurrentTitle => newsList[0].Title;
        public DateTime CurrentDate => newsList[0].Date;
        public string CurrentNewsSource => newsList[0].NewsSource;
        public string CurrentNewsUrl => newsList[0].NewsUrl;
        public int CurrentNewsNumber => newsList[0].NewsNumber;
        public string CurrentNewsDescription => newsList[0].NewsText;
        public string CurrentImage => newsList[0].Image;

        private ObservableCollection<NewsListEntry> NewsList
        {
            get { return newsList; }
            set
            {
                if (value != newsList)
                {
                    newsList = value;
                    NotifyPropertyChanged("CurrentTitle");
                    NotifyPropertyChanged("CurrentDate");
                    NotifyPropertyChanged("CurrentNewsSource");
                    NotifyPropertyChanged("CurrentNewsUrl");
                    NotifyPropertyChanged("CurrentNewsNumber");
                    NotifyPropertyChanged("CurrentNewsDescription");
                    NotifyPropertyChanged("CurrentImage");
                }
            }
        }

    }

    public class NewsListEntry
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string NewsSource { get; set; }
        public string NewsUrl { get; set; }
        public int NewsNumber { get; set; }
        public string NewsText { get; set; }
        public string Image { get; set; }
    }
}