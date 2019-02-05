using DashboardLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace DashboardLib.ViewModels
{
    public class TodoWidgetViewModel
    {
        public TodoWidgetViewModel()
        {

            ObservableCollection<TodoModel> todoList = new ObservableCollection<TodoModel>();

        }
        public void btnAdd(object sender)
        {
            Debug.WriteLine("btnAdd");


            //if (textBoxTaskInput.Text != String.Empty)
            //{
            //    todoList.Add(new TodoModel(textBoxTaskInput.Text));
            //    taskList.ItemsSource = todoList;
            //    textBoxTaskInput.Text = String.Empty;
            //    Debug.WriteLine("BtnAdd");
            //    SaveListAsync();

            //}

        }

        public void btnDel(object sender)
        {
      
        }
        public void checkChangeStated(object sender)
        {
           
        }
    }
}