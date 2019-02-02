using DashboardLib.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{

    public sealed partial class TodoWidget : UserControl
    {

        private ObservableCollection<Todo> todoList;


        public TodoWidget()
        {

            TodoModel todoModel = new TodoModel();
            DataContext = todoModel;
            InitializeComponent();

            todoList = new ObservableCollection<Todo>();

            taskList.ItemsSource = todoList;

            InitializeComponent();
        }



        private void BtnAdd(object sender, RoutedEventArgs e)
            {
            if (textBoxTaskInput.Text != String.Empty)
            {
                todoList.Add(new Todo(textBoxTaskInput.Text));
                taskList.ItemsSource = todoList;
                textBoxTaskInput.Text = String.Empty;
            }

        }

        private void BtnDel(object sender, RoutedEventArgs e)
        {
           Todo x = ((Button)sender).Tag as Todo;
            todoList.Remove(x);
           
    
        }


    }
}
