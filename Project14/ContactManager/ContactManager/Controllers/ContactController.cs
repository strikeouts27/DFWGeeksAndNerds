using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET: Contact
        public IActionResult Index()
        {
            var contacts = _repository.GetAllContacts();
            return View(contacts);
        }

        // GET: Contact/Details/5
        public IActionResult Details(int id)
        {
            var contact = _repository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // GET: Contact/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Contact/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _repository.AddContact(contact);
                TempData["SuccessMessage"] = $"Contact '{contact.FullName}' was successfully added.";
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contact/Edit/5
        public IActionResult Edit(int id)
        {
            var contact = _repository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateContact(contact);
                TempData["SuccessMessage"] = $"Contact '{contact.FullName}' was successfully updated.";
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contact/Delete/5
        public IActionResult Delete(int id)
        {
            var contact = _repository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _repository.GetContactById(id);
            if (contact != null)
            {
                var fullName = contact.FullName;
                _repository.DeleteContact(id);
                TempData["SuccessMessage"] = $"Contact '{fullName}' was successfully deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}
