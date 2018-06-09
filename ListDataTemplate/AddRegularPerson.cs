using System;
using System.Collections.Generic;
using System.Linq;

namespace ListDataTemplate
{
    public class AddRegularPerson
    {
        private int _newCardColor;

        public List<RegularPerson> AddPerson(List<RegularPerson> regularPersons, int typePerson)
        {
            _newCardColor += 1;
            var cardNumber = Convert.ToInt32(regularPersons.Last().CardNumber) + (typePerson == 0 ? 127 : 198);
            regularPersons.Add(new RegularPerson
            {
                CardColor = _newCardColor,
                CardNumber = $"{cardNumber}",
                Room = "Wartezimmer",
                TypePerson = typePerson,
                StartTime = DateTime.Now,
                WaitTime = "0:0 min."
            });
            return regularPersons;
        }
    }
}