using DashboardLib.Models;
using DashboardLib.Services;
using DashboardLib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboardLibTests
{
    [TestClass]
    public class NotesWidgetTests
    {
        private NotesWidgetViewModel VM;
        private Mock<IJsonStorageService> jsonStorageServiceMock;
        private Mock<IJsonStorageController<NotesModel>> jsonStorageControllerMock;

        [TestInitialize]
        public async Task Init()
        {
            jsonStorageControllerMock = new Mock<IJsonStorageController<NotesModel>>();
            jsonStorageControllerMock.Setup(jsonStorageController => jsonStorageController.LoadList()).Returns(Task.FromResult(new List<NotesModel>()));
            jsonStorageControllerMock.Setup(jsonStorageController => jsonStorageController.SaveList(It.IsAny<List<NotesModel>>()));

            jsonStorageServiceMock = new Mock<IJsonStorageService>();
            jsonStorageServiceMock.Setup(jsonStorageService => jsonStorageService.CreateControllerForFile<NotesModel>(It.IsAny<string>(), It.IsAny<string>())).Returns(jsonStorageControllerMock.Object);

            VM = new NotesWidgetViewModel(jsonStorageServiceMock.Object);
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
        public void ClearNotes_Functional()
        {
            string testText = "Test text";

            VM.Notes.Content = testText;
            Assert.AreEqual(testText, VM.Notes.Content);

            VM.ClearNotes();
            Assert.AreEqual(VM.Notes.Content, string.Empty);

            jsonStorageControllerMock.Verify(jsonStorageController => jsonStorageController.SaveList(It.IsAny<List<NotesModel>>()), Times.Exactly(2));
        }

        [UITestMethod]
        public void Initialize_Optimal()
        {
            jsonStorageServiceMock.Verify(jsonStorageService => jsonStorageService.CreateControllerForFile<NotesModel>(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            jsonStorageControllerMock.Verify(jsonStorageController => jsonStorageController.LoadList(), Times.Once);
        }

        [UITestMethod]
        public void Initialize_Successful()
        {
            Assert.IsInstanceOfType(VM.Notes, typeof(NotesModel));
        }
    }
}
