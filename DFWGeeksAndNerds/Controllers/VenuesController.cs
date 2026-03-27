using DFWGeeksAndNerds.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFWGeeksAndNerds.Controllers
{
    public class VenuesController : Controller
    {
        public IActionResult Venues()
        {
            return View();
        }
    }
}
