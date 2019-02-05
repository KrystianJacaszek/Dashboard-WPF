using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DashboardLib.Models
{


    public class TodoModel

    {
        public string Task { get; set; }
        public bool Done { get; set; }

        public TodoModel() { }

        public TodoModel(string task)
        {

            this.Task = task;
            this.Done = false;
        }

        public TodoModel(string task, bool done)
        {

            this.Task = task;
            this.Done = done;
        }



    }


}