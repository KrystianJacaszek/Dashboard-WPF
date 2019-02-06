using DashboardLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Xml.Serialization;
using DashboardLib.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class NotesWidget : UserControl
    {
        public NotesWidget()
          
        {
            NotesWidgetViewModel notesWidgetViewModel = new NotesWidgetViewModel();
            DataContext = notesWidgetViewModel;
            this.InitializeComponent();
            LoadNotesAsync();
        }

        private void NotesTextBox_TextChanged(object sender, TextChangedEventArgs e) {

            NotesTextLeft.Text = (500-NotesTextBox.Text.Length).ToString();
            SaveNotesAsync();

        }

        private void BtnClear(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NotesTextBox.Text = string.Empty;
        }

        public async System.Threading.Tasks.Task SaveNotesAsync()
        {


            string json = JsonConvert.SerializeObject(NotesTextBox.Text);
      
            try
            {

                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile sampleFile = await storageFolder.CreateFileAsync("notes.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile, json);
                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Assets");
                await sampleFile.MoveAsync(assetsFolder, "notes.txt", NameCollisionOption.ReplaceExisting);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }


        }

        public async System.Threading.Tasks.Task LoadNotesAsync()
        {
            try
            {

                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Assets");
                StorageFile sampleFile = await assetsFolder.GetFileAsync("notes.txt");

                string text = await FileIO.ReadTextAsync(sampleFile);

                NotesTextBox.Text = (string)JsonConvert.DeserializeObject(text);


            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }


        }
    }
}
