using DashboardLib.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DashboardLib.ViewModels
{
    public class NotesWidgetViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private string NotesContent;

        private string CharLeft;

        public string charLeft {
            get { return CharLeft; }
            private set { if (value != charLeft) { charLeft = value; NotifyPropertyChanged("CharLeft"); } }
        }

        private readonly string path = @"CustomControls\NotesWidget\Assets";
        private readonly string fileName = "notes.txt";

        private JsonStorageServices JSS = new JsonStorageServices();


        public void Initialize()
        {
            Init();
        }

        public async Task Init()

        {
            if (NotesContent is null)
            {
                NotesContent = await JSS.JsonDeserializeNotesAsync(path, fileName);

            }

        }

        public void TextChanged(string content) {

            NotesContent = content;
            JSS.JsonSerializeNotesAsync(path, fileName, NotesContent);

        }

        public void TextLeft(string content) {

            CharLeft = (500 - content.Length).ToString();
        }

        public void TextClear() {
            NotesContent = String.Empty;
            JSS.JsonSerializeNotesAsync(path, fileName, NotesContent);

        }

    }
}