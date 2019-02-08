using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DashboardLib.Models
{
    public class QuotesModel : INotifyPropertyChanged
    {
        public QuotesModel(QuotesListEntry[] quotesList)
        {
            this.quotesList = new ObservableCollection<QuotesListEntry>(quotesList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private ObservableCollection<QuotesListEntry> quotesList = new ObservableCollection<QuotesListEntry>();

        public string CurrentTitle => quotesList[0].Title;
        public string CurrentDate => quotesList[0].Date;
        public string CurrentQuoteCategory => quotesList[0].Category;
        public string CurrentQuoteAuthor => quotesList[0].Author;
        public string CurrentQuoteDescription => quotesList[0].Quote;

        private ObservableCollection<QuotesListEntry> QuotesList
        {
            get { return quotesList; }
            set
            {
                if (value != quotesList)
                {
                    quotesList = value;
                    NotifyPropertyChanged("CurrentTitle");
                    NotifyPropertyChanged("CurrentDate");
                    NotifyPropertyChanged("CurrentQuoteCategory");
                    NotifyPropertyChanged("CurrentQuoteAuthor");
                    NotifyPropertyChanged("CurrentQuoteDescription");
                }
            }
        }

    }

    public class QuotesListEntry
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
    }

}
