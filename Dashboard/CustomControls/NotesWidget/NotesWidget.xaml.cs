using DashboardLib.Models;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class NotesWidget : UserControl
    {
        public NotesWidget()
          
        {
            NotesModel notesModel = new NotesModel();
            this.InitializeComponent();
        }

        private void NotesTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            NotesTextLeft.Text = (500-NotesTextBox.Text.Length).ToString();

        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NotesTextBox.Text = string.Empty;
        }
    }
}
