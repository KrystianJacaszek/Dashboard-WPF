using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardLib.Models
{
    public class Todo
    {
        public string Task { get; set; }
        public bool Done { get; set; }

        public Todo() { }

        public Todo(string task)
        {

            this.Task = task;
            this.Done = false;
        }

       // [JsonConstructor]
        public Todo(string task,bool done)
        {

            this.Task = task;
            this.Done =done;
        }


    }
}
