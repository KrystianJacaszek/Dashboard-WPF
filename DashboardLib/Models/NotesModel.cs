using System.ComponentModel;

namespace DashboardLib.Models
{
    public class NotesModel
    {

        private string notesContent;

        public string NotesContent
        {
            get { return notesContent; }
            set { this.notesContent = value; }
        }

        public NotesModel(string Content) {
            this.notesContent = Content;
        }
        public NotesModel() {
            this.notesContent = "";
        }

    }
}
