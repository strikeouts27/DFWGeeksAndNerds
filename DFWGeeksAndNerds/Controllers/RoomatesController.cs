using Microsoft.AspNetCore.Mvc;
using DFWGeeksAndNerds.Models;

namespace DFWGeeksAndNerds.Controllers
{
    public class RoomatesController : Controller
    {
        public IActionResult Roomates()
        {
            return View();
        }
    }
}