using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;

namespace ListDataTemplate
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DateTime _currentTime;
        private string _patientsTabHeader;
        private string _escortsTabHeader;
        private ObservableCollection<RegularPerson> _regularPersons;
        private ObservableCollection<RegularPerson> _regularPatients;
        private ObservableCollection<RegularPerson> _regularEscorts;
        private readonly AddRegularPerson _addRegularPerson;

        public MainViewModel()
        {
            CreateList();
            DispatcherTimerSetup();
            _addRegularPerson = new AddRegularPerson(this);
        }

        public ObservableCollection<RegularPerson> RegularPersons
        {
            get => _regularPersons;
            set
            {
                _regularPersons = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RegularPerson> RegularPatients
        {
            get => _regularPatients;
            set
            {
                _regularPatients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RegularPerson> RegularEscorts
        {
            get => _regularEscorts;
            set
            {
                _regularEscorts = value;
                OnPropertyChanged();
            }
        }

        private void CreateList()
        {
            var startTime = DateTime.Now;
            var regularPersons = new List<RegularPerson>();
            for (int i = 0; i < 5; i++)
            {
                regularPersons.Add(new RegularPerson
                {
                    CardColor = i,
                    CardNumber = $"223{(i + 1) * 27 / 2}",
                    Room = "Wartezimmer",
                    TypePerson = i % 2 == 0 ? 0 : 1,
                    StartTime = startTime,
                    WaitTime = "0:0 min."
                });
            }
            RegularPersons = new ObservableCollection<RegularPerson>(regularPersons);
            CreateCollections(regularPersons);
        }

        private void CreateCollections(List<RegularPerson> regularPersons)
        {
            var regularPatients = regularPersons.Where(p => p.TypePerson == 0).ToList();
            RegularPatients = new ObservableCollection<RegularPerson>(regularPatients);
            PatientsTabHeader = $"Patients ({regularPatients.Count})";
            var regularEscorts = regularPersons.Where(p => p.TypePerson == 1).ToList();
            RegularEscorts = new ObservableCollection<RegularPerson>(regularEscorts);
            EscortsTabHeader = $"Escorts ({regularEscorts.Count})";
        }

        private void DispatcherTimerSetup()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dispatcherTimer.Tick += CurrentTimeText;
            dispatcherTimer.Start();
        }

        private void CurrentTimeText(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now;
        }

        public string PatientsTabHeader
        {
            get => _patientsTabHeader;
            set
            {
                _patientsTabHeader = value;
                OnPropertyChanged();
            }
        }

        public string EscortsTabHeader
        {
            get => _escortsTabHeader;
            set
            {
                _escortsTabHeader = value;
                OnPropertyChanged();
            }
        }

        public DateTime CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                foreach (var regularPerson in RegularPersons)
                {
                    var minutes = value.Subtract(regularPerson.StartTime).Minutes;
                    var seconds = value.Subtract(regularPerson.StartTime).Seconds;
                    regularPerson.WaitTime = regularPerson.StatusPerson == 0 
                        ? $"{minutes}:{seconds} min."
                        : "";
                    var currentSeconds = minutes * 60 + seconds;
                    if (currentSeconds < 60)
                        regularPerson.WaitTimeStatus = 0;
                    else if (currentSeconds < 3 * 60)
                        regularPerson.WaitTimeStatus = 1;
                    else 
                        regularPerson.WaitTimeStatus = 2;
                }
                OnPropertyChanged();
            }
        }

        public ICommand CallCommand => new RelayCommand<RegularPerson>(CallExecute);

        private void CallExecute(RegularPerson person)
        {
            var foundPerson = RegularPersons.FirstOrDefault(p => p.CardNumber == person.CardNumber);
            if (foundPerson != null)
            {
                foundPerson.StatusPerson = foundPerson.StatusPerson == 0 ? 1 : 0;
                if (foundPerson.StatusPerson == 0)
                {
                    foundPerson.StartTime = DateTime.Now;
                    foundPerson.Room = "Wartezimmer";
                    foundPerson.WaitTime = "0:0 min.";
                    foundPerson.WaitTimeStatus = 0;
                }
                else
                {
                    foundPerson.Room = "Büro";
                    foundPerson.WaitTime = "";
                }
            }
        }

        public ICommand AddPatientCommand => new RelayCommand(AddPatientExecute);

        private void AddPatientExecute()
        {
            var regularPersons = _addRegularPerson.AddPerson(RegularPersons.ToList(), 0);
            RegularPersons = new ObservableCollection<RegularPerson>(regularPersons);
            CreateCollections(regularPersons);
        }

        public ICommand AddEscortCommand => new RelayCommand(AddEscortExecute);

        private void AddEscortExecute()
        {
            var regularPersons = _addRegularPerson.AddPerson(RegularPersons.ToList(), 1);
            RegularPersons = new ObservableCollection<RegularPerson>(regularPersons);
            CreateCollections(regularPersons);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}