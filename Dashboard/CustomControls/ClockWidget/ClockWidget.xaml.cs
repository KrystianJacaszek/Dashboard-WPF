using DashboardLib.Models;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class ClockWidget : UserControl
    {
        public ClockWidget()
        {
            ClockModel clockModel = new ClockModel();
            DataContext = clockModel;

            InitializeComponent();
        }
    }
}
