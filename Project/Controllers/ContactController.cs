using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{

    public class ContactController : Controller
    {
        public IActionResult Index(string SelectedValue)
        {
            var ViewModel = new MessageViewModel
            {
                Selected = SelectedValue,
                Services = DatabaseManipulator.GetAll<Service>("Service")
            };

            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageViewModel model)
        {

            

            if (!ModelState.IsValid)
            {


                var ViewModel = new MessageViewModel
                {
                    Selected = model.Subject,
                    Services = DatabaseManipulator.GetAll<Service>("Service")
                };
                    return View(ViewModel);

            }


        

            var Message = new Message
            {
                FullName = model.FirstName + " " + model.LastName,
                Email = model.Email,
                ContactMessage = model.ContactMessage,
                Subject = model.Subject,
                DateSubmitted  = DateTime.Now
            };

            DatabaseManipulator.Save(Message);

            return RedirectToAction("");
        }
    }
}
