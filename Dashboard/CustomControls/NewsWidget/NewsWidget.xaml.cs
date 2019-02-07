using Windows.UI.Xaml.Controls;
using DashboardLib.ViewModels;

// Szablon elementu Kontrolka użytkownika jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class NewsWidget : UserControl
    {
        private readonly NewsWidgetViewModel VM;

       public NewsWidget()
        {
            VM = new NewsWidgetViewModel();
            DataContext = VM;
            VM.Initialize();

            InitializeComponent();
        }
    }
}
