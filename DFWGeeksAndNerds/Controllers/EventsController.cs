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

        [HttpPost]
        public IActionResult Create(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Save to Database here
                // 2. Redirect to a "Success" or "Index" page
                return RedirectToAction("Success"); 
            }
            return View(model);
        }

        // New action for the success page
        public IActionResult Success()
        {
            return View();
        } 
        
    }
        
}
