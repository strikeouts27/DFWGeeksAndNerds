using DFWGeeksAndNerds.Models;
using DFWGeeksAndNerds.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DFWGeeksAndNerds.Controllers
{
    public class EventsController : Controller
    {
        // This was created in EventDataServices.cs and registered in Program.cs
        // this is called dependency injection. we are asking the framework to give us an instance of the service when the controller is created.
        // we create a read only because the service was NOT accesible to the entire controller. So we made this copy of it for the controller to use. 
        private readonly EventsDataService _eventDataService;


        public EventsController(EventsDataService eventDataService )
        {
            _eventDataService = eventDataService;
        }
        public async Task<ActionResult> Index()
        {
            
            var model = new EventViewModel();
            return View(model);
        }

        // attribute tags specify on what type of HTTP Methods Type that the following method will be called on. GET requests for GET requests. POST for POST and so on. 
        [HttpGet]
        public async Task<IActionResult> Events()
        {
            // quite a few things happen on this line of code. 
            // What happens: Controller -> Services -> API Controller -> Database -> API COntroller -> Services -> Controller -> View 
            var events = await _eventDataService.GetEventsAsync();
            // by now the view is getting data back. 
            // view models are data for the front end. 
            // we must unpack the dto and make it in a format that the event view model can understand. 
            // this refences 
            var model = EventViewModel.ConvertToViewModelList(events);
            var calander = new CalanderViewModel();
            calander.Events = new List<EventViewModel>(model);
            // the finished product returns to the view. 
            return View(calander);
            //return View();
        }

        // create a post method for the form
       
        public IActionResult AddEvent()
        {
            return View(new EventViewModel()); 
        }


        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Save to Database here
                // 2. Redirect to a "Success" or "Index" page
                // conversion 
                var eventDTO = EventViewModel.ConvertToEventDTO(model);
                await _eventDataService.CreateEventAsync(eventDTO);
            }
            return RedirectToAction("Events");
        }

        // New action for the success page
        public IActionResult Success()
        {
            return View();
        } 

        //public IActionResult Calander(int? year, int? month)
        //{
        //    var today = DateTime.Today;

        //    var model = new CalendarViewModel
        //    {
        //        Year = year ?? today.Year,
        //        Month = month ?? today.Month
        //    };

        //    return View(model);
        //}

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

        [HttpPost]
        public async Task<IActionResult> PrevMonth(string modelJson) {
            var calander = JsonConvert.DeserializeObject<CalanderViewModel>(modelJson);
            await calander.MovePrevious(); 
            return View("Events", calander); 
        }

        [HttpPost]
        public async Task<IActionResult> NextMonth(string modelJson)
        {
            // JsonConvert.DeserializeObject will not work with private set attributes 
            var calander = JsonConvert.DeserializeObject<CalanderViewModel>(modelJson);
            await calander.MoveNext(); 
            return View("Events", calander);
        }
    }
        
}
