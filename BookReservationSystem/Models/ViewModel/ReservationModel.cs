using System.Collections.Generic;

namespace BookReservationSystem.Models.ViewModel
{
    public class ReservationModel
    {

        public IEnumerable<Client> Clients { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public Reservation Reservation { get; set; }
    }
}