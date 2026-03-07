using DFWGeeksAndNerds.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFWGeeksAndNerds.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Events()
        {
            return View();
        }
    }
}
