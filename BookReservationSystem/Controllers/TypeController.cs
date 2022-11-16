using System.Linq;
using System.Web.Mvc;

namespace BookReservationSystem.Controllers
{
    public class TypeController : Controller
    {
        private readonly BookReservationSystemEntities _context;


        public TypeController()
        {
            _context = new BookReservationSystemEntities();
        }

        // GET: Type
        public ActionResult Index()
        {

            var types = _context.Types.ToList();
            return View(types);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Type model)
        {

            if (ModelState.IsValid)
            {
                var type = _context.Types.Where(x => x.Name == model.Name).FirstOrDefault();
                if (type == null)
                {
                    _context.Types.Add(model);
                    _context.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("error", "Type Name already exist!!");
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
            var type = _context.Types.SingleOrDefault(x => x.Type_ID == id);
            if (type == null)
            {
                return HttpNotFound();
            }

            return View(type);
        }

        [HttpPost]
        public ActionResult Edit(Type model)
        {
            if (ModelState.IsValid)
            {
                var type = _context.Types.Where(x => x.Name == model.Name && x.Type_ID != model.Type_ID).FirstOrDefault();

                var dbObj = _context.Types.SingleOrDefault(x => x.Type_ID == model.Type_ID);

                if (dbObj != null)
                {
                    if (type == null)
                    {
                        dbObj.Name = model.Name;
                        _context.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("error1", "Type Name already exist!!");
                        return View(model);
                    }

                }
                else
                {
                    ModelState.AddModelError("error2", "Type does not exist!!");
                    return View(model);
                }


            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var type = _context.Types.SingleOrDefault(x => x.Type_ID == id);
            _context.Types.Remove(type);
            _context.SaveChanges();
            return Json("success");
        }

    }
}