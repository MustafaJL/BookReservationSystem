using System.Linq;
using System.Web.Mvc;

namespace BookReservationSystem.Controllers
{
    public class GenreController : Controller
    {

        private readonly BookReservationSystemEntities _context;

        public GenreController()
        {
            _context = new BookReservationSystemEntities();
        }


        // GET: Genre
        public ActionResult Index()
        {
            var genres = _context.Genres.ToList();
            return View(genres);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Genre model)
        {

            if (ModelState.IsValid)
            {
                var genre = _context.Genres.Where(x => x.Name == model.Name).FirstOrDefault();
                if (genre == null)
                {
                    _context.Genres.Add(model);
                    _context.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("error", "Genre Name already exist!!");
                    return View(model);
                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return HttpNotFound();
            }
            var genre = _context.Genres.SingleOrDefault(x => x.Genre_ID == id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        [HttpPost]
        public ActionResult Edit(Genre model)
        {
            if (ModelState.IsValid)
            {
                var genre = _context.Genres.Where(x => x.Name == model.Name && x.Genre_ID != model.Genre_ID).FirstOrDefault();

                var dbObj = _context.Genres.SingleOrDefault(x => x.Genre_ID == model.Genre_ID);

                if (dbObj != null)
                {
                    if (genre == null)
                    {
                        dbObj.Name = model.Name;
                        _context.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("error1", "Genre Name already exist!!");
                        return View(model);
                    }

                }
                else
                {
                    ModelState.AddModelError("error2", "Movie does not exist!!");
                    return View(model);
                }


            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Genre_ID == id);
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return Json("success");
        }
    }
}