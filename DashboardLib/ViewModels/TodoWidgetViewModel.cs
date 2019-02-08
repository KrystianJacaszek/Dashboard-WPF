using DashboardLib.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DashboardLib.Services;

namespace DashboardLib.ViewModels
{
    public class TodoWidgetViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private ObservableCollection<TodoModel> todoList = new ObservableCollection<TodoModel>();

        private readonly string path = @"CustomControls\TodoWidget\Assets";
        private readonly string fileName = "todolist.txt";

        private JsonStorageServices JSS = new JsonStorageServices();

        public ObservableCollection<TodoModel> TodoList
        {
            get { return todoList; }
            private set { if (value != todoList) { todoList = value; NotifyPropertyChanged("TodoList"); } }
        }

        public void Initialize() {
            Init();
        }

        public async Task Init()

        {
            if (todoList.Count == 0)
            {
                TodoList = await JSS.JsonDeserializeTodoAsync(path, fileName);

            }

        }


        public void AddTodo(string content)
        {
            TodoModel newTodo = new TodoModel(content);
            TodoList.Add(newTodo);

            JSS.JsonSerializeTodoAsync(path, fileName, todoList);
        }

        public void DeleteTodo(string id)
        {
            TodoModel targetTodo = TodoList.First(item => item.Id == id);
            TodoList.Remove(targetTodo);

            JSS.JsonSerializeTodoAsync(path, fileName, todoList);
        }

        public void ToggleTodoStatus(string id)
        {
            TodoModel targetTodo = TodoList.First(item => item.Id == id);
            targetTodo.ToggleStatus();

            JSS.JsonSerializeTodoAsync(path, fileName, todoList);
        }

    }
}