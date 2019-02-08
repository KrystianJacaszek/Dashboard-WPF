using DashboardLib.ApiModules;
using DashboardLib.Models;
using System.ComponentModel;
using System.Linq;

namespace DashboardLib.ViewModels
{
    public class QuotesWidgetViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private string title;
        private string date;
        private string category;
        private string author;
        private string quote;

        private QuotesModel quotesModel;
        private QuotesApi quotesApi = QuotesApi.Instance;


        public string Title
        {
            get { return title; }
            private set { if (value != title) { title = value; NotifyPropertyChanged("Title"); } }
        }

        public string Date
        {
            get { return date; }
            private set { if (value != date) { date = value; NotifyPropertyChanged("Date"); } }
        }

        public string Category
        {
            get { return category; }
            private set { if (value != category) { category = value; NotifyPropertyChanged("Category"); } }
        }

        public string Author
        {
            get { return author; }
            private set { if (value != author) { author = value; NotifyPropertyChanged("Author"); } }
        }

        public string Quote
        {
            get { return quote; }
            private set { if (value != quote) { quote = value; NotifyPropertyChanged("Quote"); } }
        }

        public QuotesModel Quotes
        {
            get { return quotesModel; }
            private set { if (value != quotesModel) { quotesModel = value; NotifyPropertyChanged("Quotes"); } }
        }


        public async void Initialize()
        {
            QuotesApi.Rootobject quotesData = await quotesApi.LoadQuotes("a");

            if (quotesData != null)
            {
                QuotesListEntry[] quotesList = quotesData.contents.quotes.Select(item =>
                {
                    QuotesListEntry entry = new QuotesListEntry();

                    entry.Title = item.title;
                    entry.Date = item.date;
                    entry.Category = item.category;
                    entry.Author = item.author;
                    entry.Quote = $"\"{item.quote}\"";

                    return entry;

                }).ToArray();

                Quotes = new QuotesModel(quotesList);
            }

        }
    }

}
