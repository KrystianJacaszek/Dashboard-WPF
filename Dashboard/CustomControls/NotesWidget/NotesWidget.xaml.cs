using Windows.UI.Xaml.Controls;
using DashboardLib.ViewModels;
using Windows.UI.Xaml;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class NotesWidget : UserControl
    {
        public NotesWidget()

        { 
            VM = new NotesWidgetViewModel();
            VM.Initialize();

            DataContext = VM;

            InitializeComponent();
        }

        private readonly NotesWidgetViewModel VM;

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            VM.ClearNotes();
        }
    }
}
