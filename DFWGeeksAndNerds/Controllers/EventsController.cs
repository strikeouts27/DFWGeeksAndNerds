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

        // create a post method for the form
        [HttpPost]
        public IActionResult AddEvent(EventViewModel eventViewModel)
        {
            return RedirectToAction("Events"); 
        }
    }
}
