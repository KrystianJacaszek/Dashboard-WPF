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



// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{

    public sealed partial class TodoWidget : UserControl
    {

        private ObservableCollection<Todo> todoList;

       // public event PropertyChangedEventHandler PropertyChanged;

        public TodoWidget()
        {

            TodoModel todoModel = new TodoModel();
            DataContext = todoModel;
            InitializeComponent();

            todoList = new ObservableCollection<Todo>();

            taskList.ItemsSource = todoList;

            LoadListAsync();

            InitializeComponent();
        }

        public async System.Threading.Tasks.Task SaveListAsync() {


            string json = JsonConvert.SerializeObject(todoList.ToArray(), Formatting.Indented);
            Debug.WriteLine("SAVE");
            Debug.WriteLine(json);
            try
            {

                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile sampleFile = await storageFolder.CreateFileAsync("todolist.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile, json);
                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Assets");
                await sampleFile.MoveAsync(assetsFolder, "todolist.txt", NameCollisionOption.ReplaceExisting);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }


        }

        public async System.Threading.Tasks.Task LoadListAsync() {
            try
            {

                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Assets");
                StorageFile sampleFile = await assetsFolder.GetFileAsync("todolist.txt");

                string text = await FileIO.ReadTextAsync(sampleFile);
                Debug.WriteLine("laod");
                Debug.WriteLine(text);

                List<Todo> list = new List<Todo>();

                list = JsonConvert.DeserializeObject<List<Todo>>(text);

                foreach (Todo task in list) {
                    todoList.Add(task);
                }


            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }
            

        }


        private void BtnAdd(object sender, RoutedEventArgs e)
            {
            if (textBoxTaskInput.Text != String.Empty)
            {
                todoList.Add(new Todo(textBoxTaskInput.Text));
                taskList.ItemsSource = todoList;
                textBoxTaskInput.Text = String.Empty;
                Debug.WriteLine("BtnAdd");
                SaveListAsync();
                
            }
            

        }

        private void BtnDel(object sender, RoutedEventArgs e)
        {
            Todo x = ((Button)sender).Tag as Todo;
            todoList.Remove(x);
        }

        private void CheckChangeState(object sender, RoutedEventArgs e)
        {
            Todo x = ((CheckBox)sender).Tag as Todo;
            if (x.Done)
            {
                todoList[todoList.IndexOf(x)].Done = false;
            }else
            {
                todoList[todoList.IndexOf(x)].Done = true;
            }
            SaveListAsync();
        }


    }
}
