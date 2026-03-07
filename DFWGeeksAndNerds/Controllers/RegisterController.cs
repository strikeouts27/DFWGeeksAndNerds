using Microsoft.AspNetCore.Mvc;
using DFWGeeksAndNerds.Models;

namespace DFWGeeksAndNerds.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}