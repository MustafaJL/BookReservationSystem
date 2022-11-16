
using BookReservationSystem.Models.ViewModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BookReservationSystem.Controllers
{
    public class ClientController : Controller
    {

        private readonly BookReservationSystemEntities _context;

        public ClientController()
        {
            _context = new BookReservationSystemEntities();
        }
        // GET: Book
        public ActionResult Index()
        {
            var clients = _context.Clients.Include(x => x.Type).ToList();
            return View(clients);
        }

        public ActionResult Create()
        {
            var types = _context.Types.ToList();
            var viewModel = new ClientTypes
            {
                Types = types,
                Client = new Client()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ClientTypes model)
        {
            if (ModelState.IsValid)
            {
                var client = _context.Clients.Where(x => x.Name == model.Client.Name).FirstOrDefault();
                if (client == null)
                {
                    var newClient = new Client
                    {
                        Name = model.Client.Name,
                        DateOfBirth = model.Client.DateOfBirth,
                        Type_ID = model.Client.Type_ID,
                    };
                    _context.Clients.Add(newClient);
                    _context.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("error", "Client Name already exist!!");
                    var types = _context.Types.ToList();
                    var viewModel = new ClientTypes
                    {
                        Types = types,
                        Client = model.Client
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
            var client = _context.Clients.SingleOrDefault(x => x.Client_ID == id);
            var types = _context.Types.ToList();


            var viewModel = new ClientTypes
            {
                Types = types,
                Client = client
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ClientTypes model)
        {
            if (ModelState.IsValid)
            {
                var client = _context.Clients.Where(x => x.Name == model.Client.Name).FirstOrDefault(x => x.Client_ID != model.Client.Client_ID);
                var dbObj = _context.Clients.SingleOrDefault(x => x.Client_ID == model.Client.Client_ID);
                if (client == null)
                {
                    dbObj.Name = model.Client.Name;
                    dbObj.DateOfBirth = model.Client.DateOfBirth;
                    dbObj.Type_ID = model.Client.Type_ID;

                    _context.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("error", "Client Name already exist!!");
                    var types = _context.Types.ToList();
                    var viewModel = new ClientTypes
                    {
                        Types = types,
                        Client = model.Client
                    };
                    return View(viewModel);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var client = _context.Clients.SingleOrDefault(x => x.Client_ID == id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return Json("success");
        }

    }
}