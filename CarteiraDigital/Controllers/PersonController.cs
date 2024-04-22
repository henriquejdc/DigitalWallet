using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarteiraDigital.Models;
using System.Linq;
using System.Threading.Tasks;
using CarteiraDigital.Repositories;
using System.Text.Json;

namespace CarteiraDigital.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonRepository personRepository;
        private readonly IHttpContextAccessor _contxt;
        public PersonController(NHibernate.ISession session, IHttpContextAccessor contxt)
        {
            personRepository = new PersonRepository(session);
            _contxt = contxt;
        }

        public ActionResult Index()
        {
            ViewBag.Count = personRepository.CountPeople().ToString();
            return View(personRepository.FindAll().ToList());
        }

        [HttpGet]
        public ActionResult SearchFilter(Person person)
        {
            var personReturn = personRepository.FindByName(person.Name);
            return View("Index", personReturn);
        }

        [HttpGet]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Person person = await personRepository.FindByID(id.Value);
            if (person == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(person);
        }

        public ActionResult Create() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                await personRepository.Add(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Person person = await personRepository.FindByID(id.Value);
            if (person == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            Person person
        )
        {
            if (ModelState.IsValid)
            {
                var old = await personRepository.FindByID(person.Id);
                old.Name = person.Name;
                old.Email = person.Email;
                old.Salary = person.Salary;
                old.AccountLimit = person.AccountLimit;
                old.MinimumValue = person.MinimumValue;
                old.Username = person.Username;

                await personRepository.Update(old);
                
                var pessoa = JsonSerializer.Deserialize<Person>(
                    _contxt.HttpContext.Session.GetString("User")
                );
                if (pessoa.Id == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Details", new { person.Id });
                }
            }
            return View(person);
        }

        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Person person = await personRepository.FindByID(id.Value);
            if (person == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await personRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
