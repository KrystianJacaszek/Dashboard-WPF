using DashboardLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DashboardLib.Services
{
    class JsonStorageServices
    {
        private string text;

        public async Task JsonSerializeTodoAsync(string path, string fileName, object obj)
        {
            
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            await SaveListAsync(path, fileName, json);

        }

        

        public async Task<ObservableCollection<TodoModel>> JsonDeserializeTodoAsync(string path, string fileName)

        {

            await LoadListSync(path, fileName);

            ObservableCollection<TodoModel> todoList = new ObservableCollection<TodoModel>();

            if (text != null)
            {

                todoList = JsonConvert.DeserializeObject<ObservableCollection<TodoModel>>(text);

            }

            return todoList;
        }

        public async Task SaveListAsync(string path, string fileName, string obj)
        {

            try
            {

                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile newFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(newFile, obj);
                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync(path);
                await newFile.MoveAsync(assetsFolder, fileName, NameCollisionOption.ReplaceExisting);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }
        }

        public async Task LoadListSync(string path, string fileName)
        {
            try
            {

                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync(path);
                StorageFile newFile = await assetsFolder.GetFileAsync(fileName);

                this.text = await FileIO.ReadTextAsync(newFile);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }


        }
    }
}
