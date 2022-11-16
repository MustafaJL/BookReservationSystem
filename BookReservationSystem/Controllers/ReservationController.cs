using BookReservationSystem.Models.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BookReservationSystem.Controllers
{
    public class ReservationController : Controller
    {

        private readonly BookReservationSystemEntities _context;


        public ReservationController()
        {
            _context = new BookReservationSystemEntities();
        }
        // GET: Reservation
        public ActionResult Index()
        {

            var reservations = _context.Reservations.Include(x => x.Client).Include(x => x.Book).ToList();
            return View(reservations);
        }

        public ActionResult Create()
        {
            var clients = _context.Clients.ToList();
            var books = _context.Books.ToList();

            var viewModel = new ReservationModel
            {
                Clients = clients,
                Books = books,
                Reservation = new Reservation()
            };
            return View(viewModel);
        }

        [HttpPost]

        public ActionResult Create(ReservationModel model)
        {
            if (ModelState.IsValid)
            {


                var newClient = new Reservation
                {
                    Client_ID = model.Reservation.Client_ID,
                    Book_ID = model.Reservation.Book_ID,
                    StartDate = model.Reservation.StartDate,
                    EndDate = model.Reservation.EndDate
                };
                _context.Reservations.Add(newClient);
                _context.SaveChanges();




            }
            else
            {
                ModelState.AddModelError("error", "Model error");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault(x => x.Reservation_ID == id);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return Json("success");
        }


        public bool dateValidation(DateTime d1, DateTime d2)
        {
            if (d2 > d1)
            {
                return true;
            }
            else return false;
        }
    }
}