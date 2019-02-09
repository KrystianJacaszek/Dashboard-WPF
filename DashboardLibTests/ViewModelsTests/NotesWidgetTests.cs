using DashboardLib.Models;
using DashboardLib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DashboardLibTests
{
    [TestClass]
    class NotesWidgetTests
    {
        private NotesWidgetViewModel VM;

        [TestInitialize]
        public async Task Init()
        {

            VM = new NotesWidgetViewModel();
            await VM.Initialize();

            Assert.IsInstanceOfType(VM, typeof(NotesWidgetViewModel));

        }

        [TestMethod]
        public void ClearNotes() {


            string testText = "Testing text";
            VM.Notes.Content = testText;

            Assert.AreEqual(testText, VM.Notes.Content);

            VM.ClearNotes();

            Assert.IsNull(VM.Notes.Content);

        }

        [TestCleanup]
        public async Task Destroy()
        {

            await VM.Destroy();
            VM = null;

        }
    }
}
