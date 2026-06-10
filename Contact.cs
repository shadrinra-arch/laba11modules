using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace laba9modules.Models
{
    public class Contact : INotifyPropertyChanged
    {
        private string _name;
        private string _phone;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
