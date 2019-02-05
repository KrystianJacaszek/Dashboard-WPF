using DashboardLib.Models;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class ClockWidget : UserControl
    {
        public ClockWidget()
        {
            DataContext = VM;

            InitializeComponent();
        }

        private readonly ClockWidgetViewModel VM = new ClockWidgetViewModel();
    }

    class ClockWidgetViewModel
    {
        public ClockModel ClockModel { get; set; } = new ClockModel();
    }
}
