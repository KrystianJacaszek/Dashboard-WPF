using Windows.UI.Xaml.Controls;
using DashboardLib.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;

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

        private void NotesTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            string inputValue = NotesTextBox.Text;
            VM.TextChanged(inputValue);
            VM.TextLeft(inputValue);

        }

        private void BtnClear_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NotesTextBox.Text = string.Empty;
            VM.TextClear();
           
        }
    }
}
