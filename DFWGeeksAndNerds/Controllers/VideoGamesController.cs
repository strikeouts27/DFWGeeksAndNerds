using Microsoft.AspNetCore.Mvc;
using DFWGeeksAndNerds.Models;

namespace DFWGeeksAndNerds.Controllers
{
    public class VideoGamesController : Controller

    {
        public IActionResult VideoGames()
        {
            return View();
        }
    }

}
