using Microsoft.AspNetCore.Mvc;

namespace DFWGeeksAndNerds.Controllers
{
    public class CollectablesController : Controller
    {
        public IActionResult Collectables()
        {
            return View();
        }
    }
}
