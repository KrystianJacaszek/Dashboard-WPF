using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace DashboardLib.Services
{
    public interface IJsonStorageService
    {
        IJsonStorageController<EntityType> CreateControllerForFile<EntityType>(string filename);
    }

    public class JsonStorageService : IJsonStorageService
    {
        private JsonStorageService() { }

        public static JsonStorageService Instance
        {
            get
            {
                if (instance == null)
                    instance = new JsonStorageService();

                return instance;
            }
        }

        private static JsonStorageService instance;

        public IJsonStorageController<EntityType> CreateControllerForFile<EntityType>(string filename)
        {
            return new JsonStorageController<EntityType>(filename);
        }
    }

    public interface IJsonStorageController<EntityType>
    {
        Task<List<EntityType>> LoadList();
        Task SaveList(List<EntityType> list);
    }

    public class JsonStorageController<EntityType> : IJsonStorageController<EntityType>
    {
        public JsonStorageController(string fileName)
        {
            this.fileName = fileName;
        }

        private string fileName;

        public async Task<List<EntityType>> LoadList()
        {
            List<EntityType> deserializedList = new List<EntityType>();
            
            StorageFile dataFile = (StorageFile) await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);

            if (dataFile != null)
            {
                string serializedData = await FileIO.ReadTextAsync(dataFile);

                deserializedList = JsonConvert.DeserializeObject<List<EntityType>>(serializedData);
            }

            return deserializedList;
        }

        public async Task SaveList(List<EntityType> list)
        {
            string json = JsonConvert.SerializeObject(list);
            
            StorageFile dataFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(dataFile, json);
        }
    }
}
