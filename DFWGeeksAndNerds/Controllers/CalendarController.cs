using Microsoft.AspNetCore.Mvc;
using System;
using DFWGeeksAndNerds.Models;

namespace DFWGeeksAndNerds.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index(int? year, int? month)
        {
            var today = DateTime.Today;
            
            var model = new CalendarViewModel
            {
                Year = year ?? today.Year,
                Month = month ?? today.Month
            };

            return View(model);
        }
    }
}