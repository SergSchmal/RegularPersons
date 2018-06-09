using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ListDataTemplate
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            CreateList();
        }

        public ObservableCollection<RegularPerson> RegularPersons { get; set; }

        private void CreateList()
        {
            var regularPersons = new List<RegularPerson>();
            for (int i = 0; i < 5; i++)
            {
                regularPersons.Add(new RegularPerson
                {
                    CardColor = i,
                    CardNumber = (i + 1) * 49 / 2,
                    Room = i == 0 ? "Büro" : "Wartezimmer",
                    TypePerson = i % 2 == 0 ? 0 : 1,
                    WaitTime = $"{i * 10}"
                });
            }
            RegularPersons = new ObservableCollection<RegularPerson>(regularPersons);
        }


        public ICommand CallCommand => new RelayCommand<RegularPerson>(CallExecute);

        private void CallExecute(RegularPerson person)
        {
            var foundPerson = RegularPersons.FirstOrDefault(p => p.CardNumber == person.CardNumber);
            if (foundPerson != null) foundPerson.StatusPerson = foundPerson.StatusPerson == 0 ? 1 : 0;
        }

    }
}