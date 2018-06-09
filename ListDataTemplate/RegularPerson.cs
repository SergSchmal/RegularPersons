using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ListDataTemplate
{
    public class RegularPerson : INotifyPropertyChanged
    {
        private int _statusPerson;
        public int CardColor { get; set; }

        public int CardNumber { get; set; }

        public int TypePerson { get; set; }

        public int StatusPerson
        {
            get => _statusPerson;
            set
            {
                _statusPerson = value;
                OnPropertyChanged();
            }
        }

        public string Room { get; set; }

        public string WaitTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}