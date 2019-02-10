using DashboardLib.Services;
using DashboardLib.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.ComponentModel;

namespace DashboardLib.ViewModels
{
    public class NotesWidgetViewModel: BaseViewModel
    {
        public NotesWidgetViewModel()
        {
            storageController = JsonStorageService.Instance.CreateControllerForFile<NotesModel>("notes.txt");
        }

        public NotesWidgetViewModel(IJsonStorageService storageService)
        {
            storageController = storageService.CreateControllerForFile<NotesModel>("notes.txt");
        }

        private IJsonStorageController<NotesModel> storageController;
        private NotesModel notes = new NotesModel();

        public NotesModel Notes
        {
            get { return notes; }
            private set { if (value != notes) { notes = value; NotifyPropertyChanged("Notes"); } }
        }

        public override async Task Initialize()
        {
            List<NotesModel> savedNotesList = await storageController.LoadList();
            if (savedNotesList.Count > 0)
                Notes = savedNotesList[0];

            notes.PropertyChanged += onNotesPropertyChanged;
        }

        public override Task Destroy()
        {
            return Task.CompletedTask;
        }

        public void ClearNotes() {
            notes.Content = string.Empty;
        }

        private void onNotesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
                storageController.SaveList(new List<NotesModel>() { notes });
        }
    }
}