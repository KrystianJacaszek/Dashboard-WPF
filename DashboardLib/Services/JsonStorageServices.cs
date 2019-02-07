using DashboardLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DashboardLib.Services
{
    class JsonStorageServices
    {
        private List<TodoModel> todoList;
        private string text;

        public void JsonSerializeTodo(string path, string fileName, object obj)
        {
            
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            SaveListAsync(path, fileName, json);

        }

        public List<TodoModel> JsonDeserializeTodo(string path, string fileName)

        {
            LoadListSync(path, fileName);

            if (text != null)
            {
                Debug.WriteLine("loaded text!=null");
                List<TodoModel> list = new List<TodoModel>();

                list = JsonConvert.DeserializeObject<List<TodoModel>>(text);

                foreach (TodoModel task in list)
                {
                    Debug.WriteLine(task);
                    todoList.Add(task);
                }
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
                Debug.WriteLine("laod");
                Debug.WriteLine(text);
                Debug.WriteLine("laod -> END");



            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }


        }
    }
}
