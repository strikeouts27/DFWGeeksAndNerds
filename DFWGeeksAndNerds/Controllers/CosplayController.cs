using Microsoft.AspNetCore.Mvc;

namespace DFWGeeksAndNerds.Controllers
{
    public class CosplayController : Controller
    {
        public IActionResult Cosplay()
        {
            return View();
        }
    }
}
