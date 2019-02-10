using DashboardLib.ApiModules;
using System;
using System.ComponentModel;

namespace DashboardLib.Models
{
    public class NewsModel : INotifyPropertyChanged
    {
        public NewsModel(string title, DateTime date, string newsText, string image, string newsUrl, string newsSource, string content)
        {
            this.title = title;
            this.date = date;
            this.newsUrl = newsUrl;
            this.newsText = newsText;
            this.newsSource = newsSource;
            this.image = image;
            this.content = content;
        }

        private NewsApi newsApi = NewsApi.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private string title;
        private DateTime date;
        private string newsUrl;
        private string newsSource;
        private string newsText;
        private string image;
        private string content;

        public string Description
        {
            get
            {
                if (newsText != null || newsText != "")
                    return newsText;
                else if (content != null || content != "")
                    return content;
                else
                    return "Click the link above.";
            }
        }

        public string Title
        {
            get { return title; }
            set { if (value != title) { title = value; NotifyPropertyChanged("Title"); } }
        }

        public DateTime Date
        {
            get { return date; }
            set { if (value != date) { date = value; NotifyPropertyChanged("Date"); } }
        }

        public string NewsUrl
        {
            get { return newsUrl; }
            set { if (value != newsUrl) { newsUrl = value; NotifyPropertyChanged("NewsUrl"); } }
        }

        public string NewsSource
        {
            get { return newsSource; }
            set { if (value != newsSource) { newsSource = value; NotifyPropertyChanged("NewsSource"); } }
        }

        public string Image
        {
            get { return image; }
            set { if (value != image) { image = value; NotifyPropertyChanged("Image"); } }
        }
    }
}
