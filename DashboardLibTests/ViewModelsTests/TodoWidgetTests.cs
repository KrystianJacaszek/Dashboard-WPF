using DashboardLib.Models;
using DashboardLib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DashboardLibTests
{
    [TestClass]
    class TodoWidgetTests
    {
        private TodoWidgetViewModel VM;

        [TestInitialize]
        public async Task Init()
        {

            VM = new TodoWidgetViewModel();
            await VM.Initialize();

            Assert.IsInstanceOfType(VM, typeof(TodoWidgetViewModel));

        }
        [TestMethod]
        public void CreateNewModel_AddTask_Obesrvable()
        {

            Assert.AreEqual(VM.TodoList.Count, 0);

            string content = "Test Task Todo";
            VM.AddTodo(content);

            Assert.AreEqual(VM.TodoList.Count, 1);

            Assert.IsInstanceOfType(VM.TodoList[0], typeof(TodoModel));
            Assert.AreEqual(VM.TodoList[0].Content, content);

        }

        [TestMethod]
        public void ChangeToggleTodoStatus()
        {

            TodoStatus DefStatus = VM.TodoList[0].Status;
            VM.TodoList[0].ToggleStatus();
            TodoStatus AfterToggleChange = VM.TodoList[0].Status;

            Assert.AreEqual(DefStatus, false);
            Assert.AreNotEqual(DefStatus, AfterToggleChange);
        }

        [TestMethod]
        public void DeleteTodo()
        {

            Assert.AreEqual(VM.TodoList.Count, 1);

            string id = VM.TodoList[0].Id;

            VM.DeleteTodo(id);

            Assert.AreEqual(VM.TodoList.Count, 0);

        }

        [TestCleanup]
        public async Task Destroy()
        {

            await VM.Destroy();
            VM = null;

        }

    }
}
