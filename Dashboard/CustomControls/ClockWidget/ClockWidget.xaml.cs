using DashboardLib.ViewModels;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class ClockWidget : UserControl
    {
        public ClockWidget()
        {
            VM = new ClockWidgetViewModel();
            VM.Initialize();

            DataContext = VM;

            InitializeComponent();
        }

        private readonly ClockWidgetViewModel VM;
    }
}
