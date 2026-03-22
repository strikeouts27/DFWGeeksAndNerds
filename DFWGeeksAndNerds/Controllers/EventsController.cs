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

        public IActionResult Calander(int? year, int? month)
        {
            var today = DateTime.Today;

            var model = new CalendarViewModel
            {
                Year = year ?? today.Year,
                Month = month ?? today.Month
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            // Give FullCalendar some dummy data to render out of the box
            var events = new[]
            {
                new { title = "Super Smash Bros Tournament", start = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"), color = "#378006" },
                new { title = "Anime Studio Ghibli Fest", start = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"), color = "#0000ff" }
            };

            return Json(events);
        }

    }
        
}
