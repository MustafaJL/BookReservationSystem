using System.Collections.Generic;

namespace BookReservationSystem.Models.ViewModel
{
    public class ClientTypes
    {
        public IEnumerable<Type> Types { get; set; }

        public Client Client { get; set; }
    }
}