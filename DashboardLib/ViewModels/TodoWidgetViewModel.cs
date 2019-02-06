using DashboardLib.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DashboardLib.ViewModels
{
    public class TodoWidgetViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private ObservableCollection<TodoModel> todoList = new ObservableCollection<TodoModel>();

        public ObservableCollection<TodoModel> TodoList
        {
            get { return todoList; }
            private set { if (value != todoList) { todoList = value; NotifyPropertyChanged("TodoList"); } }
        }

        public void Initialize()
        {
            // LoadListAsync();
        }

        public void AddTodo(string content)
        {
            TodoModel newTodo = new TodoModel(content);
            TodoList.Add(newTodo);

            // SaveListAsync();
        }

        public void DeleteTodo(string id)
        {
            TodoModel targetTodo = TodoList.First(item => item.Id == id);
            TodoList.Remove(targetTodo);

            // SaveListAsync();
        }

        public void ToggleTodoStatus(string id)
        {
            TodoModel targetTodo = TodoList.First(item => item.Id == id);
            targetTodo.ToggleStatus();

            // SaveListAsync();
        }

        //public async System.Threading.Tasks.Task SaveListAsync()
        //{


        //    string json = JsonConvert.SerializeObject(todoList.ToArray(), Formatting.Indented);
        //    Debug.WriteLine("SAVE");
        //    Debug.WriteLine(json);
        //    try
        //    {

        //        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        //        StorageFile sampleFile = await storageFolder.CreateFileAsync("todolist.txt", CreationCollisionOption.ReplaceExisting);
        //        await FileIO.WriteTextAsync(sampleFile, json);
        //        StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        //        StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Assets");
        //        await sampleFile.MoveAsync(assetsFolder, "todolist.txt", NameCollisionOption.ReplaceExisting);

        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e);

        //    }


        //}

        //public async System.Threading.Tasks.Task LoadListAsync()
        //{
        //    try
        //    {

        //        StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        //        StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Assets");
        //        StorageFile sampleFile = await assetsFolder.GetFileAsync("todolist.txt");

        //        string text = await FileIO.ReadTextAsync(sampleFile);
        //        Debug.WriteLine("laod");
        //        Debug.WriteLine(text);

        //        List<TodoModel> list = new List<TodoModel>();

        //        list = JsonConvert.DeserializeObject<List<TodoModel>>(text);

        //        foreach (TodoModel task in list)
        //        {
        //            todoList.Add(task);
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e);

        //    }


        //}
    }
}