using CarteiraDigital.Models;
using CarteiraDigital.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarteiraDigital.Controllers
{
    public class UserController : Controller
    {
        private readonly PersonRepository personRepository;
        private readonly IHttpContextAccessor _contxt;

        public UserController(NHibernate.ISession session, IHttpContextAccessor contxt)
        {
            personRepository = new PersonRepository(session);
            _contxt = contxt;
        }

        public ActionResult Index()
        {
            JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));
            return View();
        }

        public ActionResult Details(int id) { return View(); }

        public ActionResult Create() { return View(); }

        public ActionResult Edit(int id) { return View(); }

        public ActionResult Delete(int id) { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
