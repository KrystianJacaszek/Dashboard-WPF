using DashboardLib.Models;
using DashboardLib.Services;
using DashboardLib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DashboardLibTests
{
    [TestClass]
    public class TodoWidgetTests
    {
        private TodoWidgetViewModel VM;
        private Mock<IJsonStorageService> jsonStorageServiceMock;
        private Mock<IJsonStorageController<TodoModel>> jsonStorageControllerMock;

        [TestInitialize]
        public async Task Init()
        {
            jsonStorageControllerMock = new Mock<IJsonStorageController<TodoModel>>();
            jsonStorageControllerMock.Setup(jsonStorageController => jsonStorageController.LoadList()).Returns(Task.FromResult(new List<TodoModel>()));
            jsonStorageControllerMock.Setup(jsonStorageController => jsonStorageController.SaveList(It.IsAny<List<TodoModel>>()));

            jsonStorageServiceMock = new Mock<IJsonStorageService>();
            jsonStorageServiceMock.Setup(jsonStorageService => jsonStorageService.CreateControllerForFile<TodoModel>(It.IsAny<string>())).Returns(jsonStorageControllerMock.Object);

            VM = new TodoWidgetViewModel(jsonStorageServiceMock.Object);
            await VM.Initialize();
        }

        [TestCleanup]
        public async Task Destroy()
        {
            await VM.Destroy();
            VM = null;

            jsonStorageControllerMock.Reset();
            jsonStorageServiceMock.Reset();
        }

        [UITestMethod]
        public void AddTodo_Functional()
        {
            Assert.AreEqual(VM.TodoList.Count, 0);
            
            VM.AddTodo("Test content");
            Assert.AreEqual(VM.TodoList.Count, 1);

            jsonStorageControllerMock.Verify(jsonStorageController => jsonStorageController.SaveList(It.IsAny<List<TodoModel>>()), Times.Exactly(1));
        }

        [UITestMethod]
        public void DeleteTodo_Functional()
        {
            VM.AddTodo("Test content");
            Assert.AreEqual(VM.TodoList.Count, 1);
            
            VM.DeleteTodo(VM.TodoList[0].Id);
            Assert.AreEqual(VM.TodoList.Count, 0);

            jsonStorageControllerMock.Verify(jsonStorageController => jsonStorageController.SaveList(It.IsAny<List<TodoModel>>()), Times.Exactly(2));
        }

        [UITestMethod]
        public void Initialize_Optimal()
        {
            jsonStorageServiceMock.Verify(jsonStorageService => jsonStorageService.CreateControllerForFile<TodoModel>(It.IsAny<string>()), Times.Once);
            jsonStorageControllerMock.Verify(jsonStorageController => jsonStorageController.LoadList(), Times.Once);
        }

        [UITestMethod]
        public void Initialize_Successful()
        {
            Assert.IsInstanceOfType(VM.TodoList, typeof(ObservableCollection<TodoModel>));
        }

        [UITestMethod]
        public void ToggleTodoStatus_Functional()
        {
            VM.AddTodo("Test content");
            Assert.AreEqual(VM.TodoList.Count, 1);

            Assert.AreEqual(VM.TodoList[0].Status, TodoStatus.Unfinished);
            VM.ToggleTodoStatus(VM.TodoList[0].Id);
            Assert.AreEqual(VM.TodoList[0].Status, TodoStatus.Done);

            jsonStorageControllerMock.Verify(jsonStorageController => jsonStorageController.SaveList(It.IsAny<List<TodoModel>>()), Times.Exactly(2));
        }
    }
}
