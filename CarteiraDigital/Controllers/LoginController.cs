using CarteiraDigital.Models;
using CarteiraDigital.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace CarteiraDigital.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly PersonRepository personRepository;
        private readonly IHttpContextAccessor _contxt;
        public LoginController(ILogger<LoginController> logger, NHibernate.ISession session,IHttpContextAccessor contxt)
        {
            personRepository = new PersonRepository(session);
            _logger = logger;
            _contxt = contxt;
        }

        [HttpPost]
        public IActionResult Logar(string username, string password)
        {
            var user = personRepository.FindByUsername(username);
            if(username == null && password == null)
            {
                ViewBag.Message = "Usuário ou senha incorretos";
                return View("Login");
            }
            else
            {
                if((user?.Password ?? string.Empty) != password )
                {
                    ViewBag.Message = "Usuario ou senha incorretos";
                    return View("Login");
                }
                else
                {
                    string jsonString = JsonSerializer.Serialize(user);
                    _contxt.HttpContext.Session.SetString("User", jsonString);
                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult Login(){ return View(); }

        public IActionResult Index(){ return View(); }

        public IActionResult Privacy(){ return View(); } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
