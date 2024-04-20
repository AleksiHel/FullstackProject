using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Message = new Message
            {
                FullName = model.FirstName + " " + model.LastName,
                Email = model.Email,
                ContactMessage = model.ContactMessage,
            };

            DatabaseManipulator.Save(Message);

            return RedirectToAction("");
        }
    }
}
