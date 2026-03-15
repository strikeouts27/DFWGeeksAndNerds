using Microsoft.AspNetCore.Mvc;

namespace DFWGeeksAndNerds.Controllers
{
    public class BoardgamesController : Controller
    {
        public IActionResult Boardgames()
        {
            return View();
        }
    }
}
