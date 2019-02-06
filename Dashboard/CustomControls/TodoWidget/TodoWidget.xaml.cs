using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DashboardLib.ViewModels;
using System.Diagnostics;

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

        private void BtnDel(object sender, RoutedEventArgs e)
        {
            // TodoModel x = ((Button)sender).Tag as TodoModel;
            // todoList.Remove(x);
        }

        private void CheckChangeState(object sender, RoutedEventArgs e)
        {
            // TodoModel x = ((CheckBox)sender).Tag as TodoModel;
            // if (x.Done)
            // {
            //     todoList[todoList.IndexOf(x)].Done = false;
            // }else
            // {
            //     todoList[todoList.IndexOf(x)].Done = true;
            // }
        }

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
    }
}
