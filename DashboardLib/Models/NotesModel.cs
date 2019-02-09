using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace DashboardLib.Models
{
    public class NotesModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public NotesModel()
        {
            content = "";
        }

        [JsonConstructor]
        public NotesModel(string content)
        {
            this.content = trimToLimit(content);
        }

        private string content;
        private readonly int maxContentLength = 500;

        public string Content
        {
            get { return content; }
            set {
                if (value != content) {
                    content = trimToLimit(value);

                    NotifyPropertyChanged("Content");
                    NotifyPropertyChanged("CharsLeftToLimit");
                }
            }
        }

        public int CharsLeftToLimit => maxContentLength - content.Length;

        private string trimToLimit(string sourceString)
        {
            return sourceString.Substring(0, Math.Min(sourceString.Length, maxContentLength));
        }
    }
}
