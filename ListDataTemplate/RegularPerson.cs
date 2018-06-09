using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ListDataTemplate
{
    public class RegularPerson : INotifyPropertyChanged
    {
        private int _statusPerson;
        private string _waitTime;
        private DateTime _startTime;
        private string _room;
        private int _waitTimeStatus;
        public int CardColor { get; set; }

        public string CardNumber { get; set; }

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

        public string Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged();
            }
        }

        public string WaitTime
        {
            get => _waitTime;
            set
            {
                _waitTime = value;
                OnPropertyChanged();
            }
        }

        public int WaitTimeStatus
        {
            get => _waitTimeStatus;
            set
            {
                _waitTimeStatus = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}