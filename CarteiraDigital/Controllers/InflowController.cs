using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarteiraDigital.Models;
using System.Linq;
using System.Threading.Tasks;
using CarteiraDigital.Repositories;
using CarteiraDigital.Models.ViewModels;
using System.Globalization;
using System.Text.Json;

namespace CarteiraDigital.Controllers
{
    public class InflowController : Controller
    {
        private readonly InflowRepository inflowRepository;
        private readonly PersonRepository personRepository;
        private readonly IHttpContextAccessor _contxt;
        public InflowController(NHibernate.ISession session, IHttpContextAccessor contxt)
        {
            inflowRepository = new InflowRepository(session);
            personRepository = new PersonRepository(session);
            _contxt = contxt;
        }

        public ActionResult Index()
        {
            var pessoa = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));
            if (pessoa.Id == 1)
            {
                ViewBag.Total = inflowRepository.SumAmount().ToString("C2", CultureInfo.CurrentCulture);
                ViewBag.Count = inflowRepository.CountAllInflows();
                return View(inflowRepository.FindAll().ToList());
            }
            else
            {
                ViewBag.TotalUser = inflowRepository.SumAmount().ToString("C2", CultureInfo.CurrentCulture);
                ViewBag.Count = inflowRepository.CountUserInflows(pessoa.Id);
                return View(inflowRepository.FindAllById(pessoa.Id).ToList());
            }
        }

        [HttpGet]
        public ActionResult SearchFilter(Filter filter)
        {
            var returnFilter = inflowRepository.SearchFilter(filter);
            double sum = 0;
            foreach(var inflow in returnFilter)
            {
                sum += inflow.InflowAmount;
            }
            ViewBag.Total = sum.ToString("C2", CultureInfo.CurrentCulture);
            return View("Index", returnFilter.ToList());
        }

        public async Task<ActionResult> Details(long? id)
        { 
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            Inflow inflow = await inflowRepository.FindByID(id.Value);
            if (inflow == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(inflow);
        }

        public ActionResult Create()
        {
            InflowFormViewModel inflowFormViewModel = new InflowFormViewModel() { };
            inflowFormViewModel.People = personRepository.FindAll().ToList();
            return View(inflowFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Inflow Inflow)
        {
            Person person = await personRepository.FindByID(Inflow.Person.Id);
            person.Balance = person.Balance + Inflow.InflowAmount;
            Inflow.Person = person;
            await inflowRepository.Add(Inflow);
            await personRepository.Update(person);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Inflow inflow = await inflowRepository.FindByID(id.Value);
            if (inflow == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(inflow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            Inflow inflow
        )
        {
            if (ModelState.IsValid)
            {
                var antigo = await inflowRepository.FindByID(inflow.Id);
                antigo.InflowDate = inflow.InflowDate;
                antigo.InflowDescription = inflow.InflowDescription;
                
                await inflowRepository.Update(antigo);
                return RedirectToAction("Index");
            }
            return View(inflow);
        }

        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Inflow inflow = await inflowRepository.FindByID(id.Value);
            Person person = await personRepository.FindByID(inflow.Person.Id);
            person.Balance = person.Balance - inflow.InflowAmount;
            inflow.Person = person;
            if (inflow == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            await personRepository.Update(person);
            return View(inflow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await inflowRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
