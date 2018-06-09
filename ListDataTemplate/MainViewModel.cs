using System.Collections.ObjectModel;

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
            throw new System.NotImplementedException();
        }

    }
}