using DFWGeeksAndNerds.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFWGeeksAndNerds.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult Index()
        {
            var model = new EventViewModel();
            return View(model);
        }
        public IActionResult Events()
        {
            var model = new EventViewModel();
            return View(model);
            //return View();
        }

        // create a post method for the form
        [HttpPost]
        public IActionResult AddEvent(EventViewModel eventViewModel)
        {
            return RedirectToAction("Events"); 
        }
    }
}
