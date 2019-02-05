using DashboardLib.Models;
using Windows.UI.Xaml.Controls;

// Szablon elementu Kontrolka użytkownika jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class QuotesWidget : UserControl
    {
        public QuotesWidget()
        {
            QuotesModel quotesModel = new QuotesModel();
            InitializeComponent();
            SetupQuote();
        }
        private void SetupQuote()
        {
            Category.Text = "Inspire";
            Title.Text = "Title of Quote";
            Date.Text = "Date of Quote";
            Author.Text = "Author of Quote";
            Quote.Text = "Quote";
        }
    }
}
