using Microsoft.AspNetCore.Mvc;
using DFWGeeksAndNerds.Models;

namespace DFWGeeksAndNerds.Controllers
{
    public class PodcastsController : Controller
    {
        public IActionResult Podcasts()
        {
            return View();
        }
    }
}