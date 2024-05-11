using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var websiteprofile = DatabaseManipulator.GetAll<WebsiteProfile>("WebsiteProfile");

            try
            {
                return View(websiteprofile[0]);

            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
