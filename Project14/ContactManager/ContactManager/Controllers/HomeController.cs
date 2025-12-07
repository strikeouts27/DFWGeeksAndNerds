using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;

namespace ContactManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IContactRepository _repository;

    public HomeController(ILogger<HomeController> logger, IContactRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IActionResult Index()
    {
        var contacts = _repository.GetAllContacts();
        ViewBag.ContactCount = _repository.Count();
        return View(contacts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
