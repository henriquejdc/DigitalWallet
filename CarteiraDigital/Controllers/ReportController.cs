using CarteiraDigital.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarteiraDigital.Models.ViewModels;
using CarteiraDigital.Models;
using System.Text.Json;

namespace CarteiraDigital.Controllers
{
    public class ReportController : Controller
    {
        private readonly InflowRepository inflowRepository;
        private readonly OutflowRepository outflowRepository;
        private readonly IHttpContextAccessor _contxt;
        public ReportController(NHibernate.ISession session, IHttpContextAccessor contxt)
        {
            inflowRepository = new InflowRepository(session);
            outflowRepository = new OutflowRepository(session);
            _contxt = contxt;
        }

        public ActionResult Index()
        {
            ReportFormViewModel report = new ReportFormViewModel();
            var pessoa = JsonSerializer.Deserialize<Person>(_contxt.HttpContext.Session.GetString("User"));
            if (pessoa.Id == 1)
            {
                report.Inflow = inflowRepository.FindAll();
                ViewBag.CountInflows = inflowRepository.CountAllInflows().ToString();
                report.Outflow = outflowRepository.FindAll();
                ViewBag.CountOutflows = outflowRepository.CountAllOutflows().ToString();
            }
            else
            {
                report.Inflow = inflowRepository.FindAllById(pessoa.Id);
                ViewBag.CountInflows = inflowRepository.CountUserInflows(pessoa.Id); 
                report.Outflow = outflowRepository.FindAllById(pessoa.Id);
                ViewBag.CountOutflows = outflowRepository.CountUserOutflows(pessoa.Id);
            }
            return View(report);
        }

        [HttpGet]
        public ActionResult SearchFilter(Filter filter)
        {
            ReportFormViewModel returnFilter = new ReportFormViewModel();
            returnFilter.Outflow = outflowRepository.SearchFilter(filter);
            returnFilter.Inflow = inflowRepository.SearchFilter(filter);
            return View("Index", returnFilter);
        }

        public ActionResult Details(int id) { return View(); }

        public ActionResult Create() { return View(); }

        public ActionResult Delete(int id) { return View(); }

        public ActionResult Edit(int id) { return View(); }

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
