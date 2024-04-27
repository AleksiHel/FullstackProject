using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{

    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            var ViewModel = new MessageViewModel();

            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var ViewModel = new MessageViewModel();

                return View(ViewModel);
            }

            var Message = new Message
            {
                FullName = model.FirstName + " " + model.LastName,
                Email = model.Email,
                ContactMessage = model.ContactMessage,
                Subject = model.Subject
            };

            DatabaseManipulator.Save(Message);

            return RedirectToAction("");
        }
    }
}
