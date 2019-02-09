using Newtonsoft.Json;
using System.ComponentModel;

namespace DashboardLib.Models
{
    public class TodoModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        [JsonConstructor]
        public TodoModel(string content, string id, TodoStatus status) {
            this.content = content;
            this.id = id;
            this.status = status;
        }

        public TodoModel(string content)
        {
            this.content = content;
            id = System.Guid.NewGuid().ToString();
            status = TodoStatus.Unfinished;
        }

        public TodoModel(string content, TodoStatus status)
        {
            this.content = content;
            id = System.Guid.NewGuid().ToString();
            this.status = status;
        }

        private string content;
        private string id;
        private TodoStatus status;
        
        public string Content
        {
            get { return content; }
            private set { if (value != content) { content = value; NotifyPropertyChanged("Content"); } }
        }

        public string Id
        {
            get { return id; }
            private set { if (value != id) { id = value; NotifyPropertyChanged("Id"); } }
        }

        public TodoStatus Status
        {
            get { return status; }
            private set { if (value != status) { status = value; NotifyPropertyChanged("Status"); } }
        }

        public void ToggleStatus()
        {
            if (Status == TodoStatus.Unfinished)
                Status = TodoStatus.Done;
            else
                Status = TodoStatus.Unfinished;
        }
    }
    
    public enum TodoStatus
    {
        Unfinished = 1,
        Done = 2,
    }
}