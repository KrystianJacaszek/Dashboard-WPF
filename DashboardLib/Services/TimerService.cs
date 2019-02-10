using System;
using Windows.UI.Xaml;

namespace DashboardLib.Services
{
    public interface ITimerService
    {
        event EventHandler<EventArgs> Tick;

        TimeSpan Interval { get; set; }
        bool IsEnabled { get; }

        void Start();
        void Stop();
    }

    public class TimerService : ITimerService {
        private TimerService()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
        }

        public static TimerService Instance
        {
            get
            {
                if (instance == null)
                    instance = new TimerService();

                return instance;
            }
        }

        private static TimerService instance;
        private DispatcherTimer timer;

        public event EventHandler<EventArgs> Tick;

        public TimeSpan Interval {
            get { return timer.Interval; }
            set { if (value != timer.Interval) { timer.Interval = value; } }
        }

        public bool IsEnabled => timer.IsEnabled;

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private void OnTick(EventArgs e)
        {
            Tick?.Invoke(this, e);
        }

        private void Timer_Tick(object sender, object e)
        {
            EventArgs args = new EventArgs();

            OnTick(args);
        }
    }
}
