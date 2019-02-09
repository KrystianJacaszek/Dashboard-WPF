using System.ComponentModel;
using System.Threading.Tasks;

namespace DashboardLib.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public abstract Task Destroy();
        public abstract Task Initialize();
    }
}
