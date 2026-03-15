using DFWGeeksAndNerds.Models;
using Microsoft.AspNetCore.Mvc;
namespace DFWGeeksAndNerds.Controllers;

public class AnimeController : Controller
{
    public IActionResult Anime()
    {
        return View();
    }
}