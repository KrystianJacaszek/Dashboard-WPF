using DashboardLib.Services;
using DashboardLib.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace DashboardLib.ViewModels
{
    public class NotesWidgetViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private string charLeft;

        private NotesModel notesModel = new NotesModel();

        private readonly string path = @"CustomControls\NotesWidget\Assets";
        private readonly string fileName = "notes.txt";

        public string CharLeft {
            get { return charLeft; }
            private set { if (value != charLeft) { charLeft = value; NotifyPropertyChanged("CharLeft"); } }
        }

        private string notesContent;

        public string NotesContent
        {
            get { return notesContent; }
            private set { if (value != notesContent) { notesContent = value; NotifyPropertyChanged("NotesContent"); } }
        }

        private JsonStorageServices JSS = new JsonStorageServices();


        public void Initialize()
        {
            Init();
        }

        public async Task Init()

        {
            TextLeft("");

            if (notesModel.NotesContent.Length==0)
            {
                notesModel = await JSS.JsonDeserializeNotesAsync(path, fileName);
                NotesContent = notesModel.NotesContent;

            }



        }

        public void TextChanged(string content) {

            notesModel.NotesContent = content;
            JSS.JsonSerializeNotesAsync(path, fileName, notesModel);

        }

        public void TextLeft(string content) {

            CharLeft = (1000 - content.Length).ToString();
        }

        public void TextClear() {

            notesModel.NotesContent = string.Empty;
            JSS.JsonSerializeNotesAsync(path, fileName, notesModel);

        }

    }
}