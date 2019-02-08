using Windows.UI.Xaml.Controls;
using DashboardLib.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class NotesWidget : UserControl
    {
        public NotesWidget()

        { 
            NotesWidgetViewModel notesWidgetViewModel = new NotesWidgetViewModel();
            DataContext = notesWidgetViewModel;
            this.InitializeComponent();
        }

        private readonly NotesWidgetViewModel notesWidgetViewModel;

        private void NotesTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            string inputValue = NotesTextBox.Text;
            notesWidgetViewModel.TextLeft(inputValue);

        }

        private void BtnClear_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NotesTextBox.Text = string.Empty;
            notesWidgetViewModel.TextClear();
           
        }
    }
}
