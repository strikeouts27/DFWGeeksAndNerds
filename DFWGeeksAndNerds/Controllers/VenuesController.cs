using Microsoft.AspNetCore.Mvc;

namespace DFWGeeksAndNerds.Controllers
{
    public class Venues : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
