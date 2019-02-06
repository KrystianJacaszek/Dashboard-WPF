using System;
using System.ComponentModel;

namespace DashboardLib.Models
{
    public class ClockModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentDate
        {
            get {
                DateTime now = DateTime.Now;

                return now.ToString("dddd, MMMM") + " " + Ordinal(now.Day);
            }
        }

        public string CurrentTime
        {
            get {
                DateTime now = DateTime.Now;

                return now.ToString("h:mm") + now.ToString("tt").ToLower();
            }
        }

        public void Update()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDate"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTime"));
        }

        private string Ordinal(int number)
        {
            if (number <= 0)
                return number.ToString();

            if (number % 100 >= 11 && number % 100 <= 13)
                return number + "th";

            switch (number % 10)
            {
                case 1:
                    return number + "st";

                case 2:
                    return number + "nd";

                case 3:
                    return number + "rd";

                default:
                    return number + "th";
            }
        }
    }
}
