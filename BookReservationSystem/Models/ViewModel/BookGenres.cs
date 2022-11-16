using System.Collections.Generic;

namespace BookReservationSystem.Models.ViewModel
{
    public class BookGenres
    {
        public IEnumerable<Genre> Genres { get; set; }

        public Book Book { get; set; }
    }
}