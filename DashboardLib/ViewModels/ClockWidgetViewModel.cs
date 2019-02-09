using DashboardLib.Models;
using DashboardLib.Services;
using System;
using System.Threading.Tasks;

namespace DashboardLib.ViewModels
{
    public class ClockWidgetViewModel : BaseViewModel
    {
        public ClockWidgetViewModel()
        {
            timerService = TimerService.Instance;
        }

        public ClockWidgetViewModel(ITimerService timerService)
        {
            this.timerService = timerService;
        }

        private ClockModel clock = new ClockModel();
        private ITimerService timerService;

        public ClockModel Clock
        {
            get { return clock; }
            private set { if (value != clock) { clock = value; NotifyPropertyChanged("Clock"); } }
        }

        public override Task Initialize()
        {
            timerService.Interval = TimeSpan.FromSeconds(1);
            timerService.Tick += Timer_Tick;
            timerService.Start();

            return Task.CompletedTask;
        }

        public override Task Destroy()
        {
            timerService.Stop();

            return Task.CompletedTask;
        }

        private void Timer_Tick(object sender, object e)
        {
            Clock.Update();
        }
    }
}
