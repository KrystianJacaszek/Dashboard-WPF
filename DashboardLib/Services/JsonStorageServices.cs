using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;


namespace DashboardLib.Services
{
    class JsonStorageServices
    {
        private string text;


        public async Task JsonSerializeNotesAsync(string path, string fileName, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            await SaveListAsync(path, fileName, json);

        }



        public async Task<string> JsonDeserializeNotesAsync(string path, string fileName)

        {

            await LoadListSync(path, fileName);

            string notesContent;

            if (text != null)
            {
                notesContent = JsonConvert.Deserialize(text);
            }

            return notesContent;
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
