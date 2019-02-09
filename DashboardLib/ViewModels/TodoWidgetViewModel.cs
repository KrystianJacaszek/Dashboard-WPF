using DashboardLib.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DashboardLib.Services;
using System.Collections.Generic;

namespace DashboardLib.ViewModels
{
    public class TodoWidgetViewModel : BaseViewModel
    {
        public TodoWidgetViewModel()
        {
            storageController = JsonStorageService.Instance.CreateControllerForFile<TodoModel>(@"CustomControls\TodoWidget\Assets", "todolist.txt");
        }

        public TodoWidgetViewModel(IJsonStorageService storageService)
        {
            storageController = storageService.CreateControllerForFile<TodoModel>(@"CustomControls\TodoWidget\Assets", "todolist.txt");
        }

        private IJsonStorageController<TodoModel> storageController;
        private ObservableCollection<TodoModel> todoList = new ObservableCollection<TodoModel>();

        public ObservableCollection<TodoModel> TodoList
        {
            get { return todoList; }
            private set { if (value != todoList) { todoList = value; NotifyPropertyChanged("TodoList"); } }
        }

        public override async Task Initialize() {
            List<TodoModel> savedTodoList = await storageController.LoadList();

            TodoList = new ObservableCollection<TodoModel>(savedTodoList);
        }

        public override Task Destroy()
        {
            return Task.CompletedTask;
        }

        public void AddTodo(string content)
        {
            TodoModel newTodo = new TodoModel(content);
            TodoList.Add(newTodo);

            saveCurrentTodoList();
        }

        public void DeleteTodo(string id)
        {
            TodoModel targetTodo = TodoList.First(item => item.Id == id);
            TodoList.Remove(targetTodo);

            saveCurrentTodoList();
        }

        public void ToggleTodoStatus(string id)
        {
            TodoModel targetTodo = TodoList.First(item => item.Id == id);
            targetTodo.ToggleStatus();

            saveCurrentTodoList();
        }

        private async Task saveCurrentTodoList()
        {
            await storageController.SaveList(todoList.ToList());
        }
    }
}