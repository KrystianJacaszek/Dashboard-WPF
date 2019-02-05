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

    public sealed partial class TodoWidget : UserControl
    {

        private TodoWidgetViewModel todoWidgetViewModel = new TodoWidgetViewModel();

        private ObservableCollection<TodoModel> todoList;

        public TodoWidget()
        {

            DataContext = todoWidgetViewModel;
            InitializeComponent();

            todoList = new ObservableCollection<TodoModel>();

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

                List<TodoModel> list = new List<TodoModel>();

                list = JsonConvert.DeserializeObject<List<TodoModel>>(text);

                foreach (TodoModel task in list) {
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
            todoWidgetViewModel.btnAdd(sender);

            if (textBoxTaskInput.Text != String.Empty)
            {
                todoList.Add(new TodoModel(textBoxTaskInput.Text));
                taskList.ItemsSource = todoList;
                textBoxTaskInput.Text = String.Empty;
                Debug.WriteLine("BtnAdd");
                SaveListAsync();
                
            }
            

        }

        private void BtnDel(object sender, RoutedEventArgs e)
        {
            TodoModel x = ((Button)sender).Tag as TodoModel;
            todoList.Remove(x);
        }

        private void CheckChangeState(object sender, RoutedEventArgs e)
        {
            TodoModel x = ((CheckBox)sender).Tag as TodoModel;
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
