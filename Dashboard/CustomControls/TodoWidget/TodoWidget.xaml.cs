using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DashboardLib.ViewModels;
using System.Diagnostics;
using Windows.UI.Xaml.Data;
using System;
using DashboardLib.Models;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{

    public sealed partial class TodoWidget : UserControl
    {
        public TodoWidget()
        {

            VM = new TodoWidgetViewModel();
            VM.Initialize();

            DataContext = VM;

            InitializeComponent();
        }

        private readonly TodoWidgetViewModel VM;

        private void TaskAdd_Click(object sender, RoutedEventArgs e)
        {
            string inputValue = TaskTextBox.Text;
            Debug.WriteLine(inputValue);

            if (inputValue != string.Empty)
            {
                VM.AddTodo(inputValue);

                TaskTextBox.Text = string.Empty;
            }
        }

        private void TodoCheckBox_Click(object sender, RoutedEventArgs e)
        {
            string todoId = ((CheckBox)sender).Tag as string;

            VM.ToggleTodoStatus(todoId);
        }

        private void TodoDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string todoId = ((Button)sender).Tag as string;

            VM.DeleteTodo(todoId);
        }
    }

    public class TodoStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a string");

            switch (value)
            {
                case TodoStatus.Done:
                    return true;

                case TodoStatus.Unfinished:
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
