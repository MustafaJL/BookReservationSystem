
using BookReservationSystem.Models.ViewModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BookReservationSystem.Controllers
{
    public class BookController : Controller
    {

        private readonly BookReservationSystemEntities _context;

        public BookController()
        {
            _context = new BookReservationSystemEntities();
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = _context.Books.Include(x => x.Genre).ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new BookGenres
            {
                Genres = genres,
                Book = new Book()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(BookGenres model)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Where(x => x.Name == model.Book.Name).FirstOrDefault();
                if (book == null)
                {
                    var newBook = new Book
                    {
                        Name = model.Book.Name,
                        NumberInStock = model.Book.NumberInStock,
                        Genre_ID = model.Book.Genre_ID,
                    };
                    _context.Books.Add(newBook);
                    _context.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("error", "Book Name already exist!!");
                    var genres = _context.Genres.ToList();
                    var viewModel = new BookGenres
                    {
                        Genres = genres,
                        Book = model.Book
                    };
                    return View(viewModel);
                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var book = _context.Books.SingleOrDefault(x => x.Book_ID == id);
            var genres = _context.Genres.ToList();

            var viewModel = new BookGenres
            {
                Genres = genres,
                Book = book
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(BookGenres model)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Where(x => x.Name == model.Book.Name).FirstOrDefault(x => x.Book_ID != model.Book.Book_ID);
                var dbObj = _context.Books.SingleOrDefault(x => x.Book_ID == model.Book.Book_ID);
                if (book == null)
                {
                    dbObj.Name = model.Book.Name;
                    dbObj.NumberInStock = model.Book.NumberInStock;
                    dbObj.Genre_ID = model.Book.Genre_ID;

                    _context.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("error", "Book Name already exist!!");
                    var genres = _context.Genres.ToList();
                    var viewModel = new BookGenres
                    {
                        Genres = genres,
                        Book = model.Book
                    };
                    return View(viewModel);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Book_ID == id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Json("success");
        }

    }
}