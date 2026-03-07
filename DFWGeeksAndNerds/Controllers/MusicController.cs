using Microsoft.AspNetCore.Mvc;
using DFWGeeksAndNerds.Models;

namespace DFWGeeksAndNerds.Controllers
{
    public class MusicController : Controller
    {
        public IActionResult Music()
        {
            return View();
        }
    }
}