﻿using DashboardLib.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using Windows.Storage;
using System.Collections.Generic;
using DashboardLib.Services;

namespace DashboardLib.ViewModels
{
    public class TodoWidgetViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private ObservableCollection<TodoModel> todoList = new ObservableCollection<TodoModel>();

        private readonly string path = @"CustomControls\TodoWidget\Assets";
        private readonly string fileName = "todolist.txt";

        private JsonStorageServices JSS = new JsonStorageServices();

        public ObservableCollection<TodoModel> TodoList
        {
            get { return todoList; }
            private set { if (value != todoList) { todoList = value; NotifyPropertyChanged("TodoList"); } }
        }

        public void Initialize()

        {
            if (JSS.JsonDeserializeTodo(path, fileName) != null)
            {
                foreach (TodoModel tm in JSS.JsonDeserializeTodo(path, fileName))
                {
                    todoList.Add(tm);
                }
            }
            
        }

        public void AddTodo(string content)
        {
            TodoModel newTodo = new TodoModel(content);
            TodoList.Add(newTodo);

            JSS.JsonSerializeTodo(path, fileName, todoList);
        }

        public void DeleteTodo(string id)
        {
            TodoModel targetTodo = TodoList.First(item => item.Id == id);
            TodoList.Remove(targetTodo);

            JSS.JsonSerializeTodo(path, fileName, todoList);
        }

        public void ToggleTodoStatus(string id)
        {
            TodoModel targetTodo = TodoList.First(item => item.Id == id);
            targetTodo.ToggleStatus();

            JSS.JsonSerializeTodo(path, fileName, todoList);
        }

    }
}