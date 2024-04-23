using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            var services = DatabaseManipulator.GetAll<Service>("Service");
            return View(services);
        }
    }
}
