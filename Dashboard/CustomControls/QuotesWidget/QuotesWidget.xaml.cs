using Windows.UI.Xaml.Controls;
using DashboardLib.ViewModels;

// Szablon elementu Kontrolka użytkownika jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class QuotesWidget : UserControl
    {
        private readonly QuotesWidgetViewModel VM;

        public QuotesWidget()
        {
            VM = new QuotesWidgetViewModel();
            VM.Initialize();

            DataContext = VM;
            InitializeComponent();
        }
    }
}
