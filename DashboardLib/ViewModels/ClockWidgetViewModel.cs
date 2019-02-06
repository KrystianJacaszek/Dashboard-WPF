using DashboardLib.Models;
using System;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace DashboardLib.ViewModels
{
    public class ClockWidgetViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private ClockModel clock = new ClockModel();

        public ClockModel Clock
        {
            get { return clock; }
            private set { if (value != clock) { clock = value; NotifyPropertyChanged("Clock"); } }
        }

        public void Initialize()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            Clock.Update();
        }
    }
}
