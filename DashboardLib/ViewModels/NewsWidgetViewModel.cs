using DashboardLib.ApiModules;
using DashboardLib.Models;
using System;
using System.ComponentModel;
using System.Linq;

namespace DashboardLib.ViewModels
{
    public class NewsWidgetViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private string title;
        private DateTime date;
        private string newsSource;
        private int newsNumber;
        private string newsText;

        private NewsModel newsModel;
        private NewsApi newsApi = NewsApi.Instance;


        public string Title
        {
            get { return title; }
            private set { if (value != title) { title = value; NotifyPropertyChanged("Title"); } }
        }

        public DateTime Date
        {
            get { return date; }
            private set { if (value != date) { date = value; NotifyPropertyChanged("Date"); } }
        }

        public string NewsSource
        {
            get { return newsSource; }
            private set { if (value != newsSource) { newsSource = value; NotifyPropertyChanged("NewsSource"); } }
        }

        public int NewsNumber
        {
            get { return newsNumber; }
            private set { if (value != newsNumber) { newsNumber = value; NotifyPropertyChanged("NewsNumber"); } }
        }

        public string NewsText
        {
            get { return newsText; }
            private set { if (value != newsText) { newsText = value; NotifyPropertyChanged("NewsText"); } }
        }

        public NewsModel News
        {
            get { return newsModel; }
            private set { if (value != newsModel) { newsModel = value; NotifyPropertyChanged("News"); } }
        }


        public async void Initialize()
        {
            NewsApi.Rootobject newsData = await newsApi.LoadNews("a");

            if (newsData != null)
            {
                NewsListEntry[] newsList = newsData.articles.Select(item =>
                {
                    NewsListEntry entry = new NewsListEntry();

                    entry.Title = item.title;
                    entry.Date = item.publishedAt;
                    entry.NewsSource = item.source.name;
                    entry.NewsNumber = newsApi.CurrentNewsPage;
                    entry.NewsText = item.description;
                    if (entry.NewsText == null)
                        entry.NewsText = item.content;
                    entry.Image = item.urlToImage;
                    entry.NewsUrl = item.url;


                    return entry;

                }).ToArray();

                News = new NewsModel(newsList);
            }

        }
    }
}
