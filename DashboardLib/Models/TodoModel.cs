using System.ComponentModel;

namespace DashboardLib.Models
{
    public class TodoModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        
        public TodoModel(string content)
        {
            this.content = content;
            status = TodoStatus.Unfinished;
        }

        public TodoModel(string content, TodoStatus status)
        {
            this.content = content;
            this.status = status;
        }

        private string content;
        private TodoStatus status;
        
        public string Content
        {
            get { return content; }
            private set { if (value != content) { content = value; NotifyPropertyChanged("Content"); } }
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