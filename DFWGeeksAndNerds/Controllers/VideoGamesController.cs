using Microsoft.AspNetCore.Mvc;
using DFWGeeksAndNerds.Models;

namespace DFWGeeksAndNerds.Controllers
{
    public class VideoGamesController : Controller

    {
        public IActionResult Videogames()
        {
            return View();
        }

        public IActionResult Nintendo()
        {
            return View();
        }

        public IActionResult Xbox()
        {
            return View();
        }

        public IActionResult PlayStation()
        {
            return View();
        }

        public IActionResult STEAM()
        {
            return View();
        }
    }

}
