
using DashboardLib.Models;
using DashboardLib.Services;
using DashboardLib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Moq;
using System;
using System.Threading.Tasks;

namespace DashboardLibTests
{
    [TestClass]
    public class ClockWidgetTests
    {
        private ClockWidgetViewModel VM;
        private Mock<ITimerService> timerServiceMock;

        [TestInitialize]
        public async Task SetupViewModel()
        {
            timerServiceMock = new Mock<ITimerService>();
            timerServiceMock.SetupProperty(timerService => timerService.Interval);
            timerServiceMock.Setup(timerService => timerService.Start());

            VM = new ClockWidgetViewModel(timerServiceMock.Object);
            await VM.Initialize();
        }

        [TestCleanup]
        public async Task CleanupViewModel()
        {
            await VM.Destroy();
            VM = null;

            timerServiceMock.Reset();
        }

        [UITestMethod]
        public void Initialize_CreatesFunctionalTimer()
        {
            timerServiceMock.VerifySet(timerService => timerService.Interval = It.IsAny<TimeSpan>(), Times.Once);
            timerServiceMock.Verify(timerService => timerService.Start(), Times.Once);
        }

        [UITestMethod]
        public void Initialize_Successful()
        {
            Assert.IsInstanceOfType(VM.Clock, typeof(ClockModel));
        }
    }
}
