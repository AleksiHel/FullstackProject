using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Project.Models;

namespace Project.Controllers
{
    public class ManagementController : Controller
    {

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var messages = DatabaseManipulator.GetAll<Message>("Message");
            var articles = DatabaseManipulator.GetAll<Article>("Article");

            var ViewModel = new ManagementViewModel
            {
                Messages = messages,
                Articles = articles
            };

            return View(ViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteMessage(ObjectId id)
        {
            DatabaseManipulator.Delete(DatabaseManipulator.GetById<Message>(id, "Message"));
            return RedirectToAction("index");
        }

        [Authorize(Roles = "admin")]

        public ActionResult markanswered(ObjectId id)
        {

            var entry = DatabaseManipulator.GetById<Message>(id, "Message");

            entry.Answered = true;

            DatabaseManipulator.Save(entry);


            return RedirectToAction("index");
        }
        [Authorize(Roles = "admin")]

        public IActionResult ManageArticles()
        {
            var articles = DatabaseManipulator.GetAll<Article>("Article");
            
            var ViewModel = new ManagementViewModel { Articles = articles };

            return PartialView("_Articles", ViewModel);
        }

        public ActionResult DeleteArticle(ObjectId ArticleID)
        {


            DatabaseManipulator.Delete(DatabaseManipulator.GetById<Article>(ArticleID, "Article"));

            return RedirectToAction("index");
        }

        public ActionResult DeleteService(ObjectId ServiceId)
        {


            DatabaseManipulator.Delete(DatabaseManipulator.GetById<Service>(ServiceId, "Service"));

            return RedirectToAction("index");
        }


        [Authorize(Roles = "admin")]

        public IActionResult ManageMessages()
        {
            var messages = DatabaseManipulator.GetAll<Message>("Message");
            var articles = DatabaseManipulator.GetAll<Article>("Article");

            var ViewModel = new ManagementViewModel
            {
                Messages = messages,
                Articles = articles
            };


            return PartialView("_Messages", ViewModel);
        }

        [Authorize(Roles = "admin")]

        public IActionResult ManageServices()
        {
            var services = DatabaseManipulator.GetAll<Service>("Service").ToList();

            return PartialView("_Services", services);
        }
        [Authorize(Roles = "admin")]

        public IActionResult AddService()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddService(Service model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DatabaseManipulator.Save(model);

            return RedirectToAction("index");

        }

        [Authorize(Roles = "admin")]

        public IActionResult EditService(ObjectId ServiceId)
        {
            if (ServiceId != ObjectId.Empty)
            {
                var service = DatabaseManipulator.GetById<Service>(ServiceId, "Service");

                return View(service);
            }

            return PartialView("_Services");
        }

        [Authorize(Roles = "admin")]

        public IActionResult EditArticle(ObjectId articleID)
        {
            if (articleID != ObjectId.Empty)
            {
                var article = DatabaseManipulator.GetById<Article>(articleID, "Article");

                return View(article);
            }

            return PartialView("_Articles");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditService(Service model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DatabaseManipulator.Save(model);

            return RedirectToAction("index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditArticle(Article model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            DatabaseManipulator.Save(model);

            return RedirectToAction("index");
        }
        [Authorize(Roles = "admin")]

        public IActionResult FilterMessages(string filter)
        {
            List<Message> messages = new List<Message>();

            if (filter == "unanswered")
            {
                messages = DatabaseManipulator.GetAll<Message>("Message")
                                                  .Where(x => x.Answered == false)
                                                  .ToList();
            } else if (filter == "answered")
            {
                messages = DatabaseManipulator.GetAll<Message>("Message")
                                                  .Where(x => x.Answered == true)
                                                  .ToList();
            } else if (filter == "all")
            {
                messages = DatabaseManipulator.GetAll<Message>("Message");
            }





            var articles = DatabaseManipulator.GetAll<Article>("Article");

            var ViewModel = new ManagementViewModel
            {
                Messages = messages,
                Articles = articles
            };


            return PartialView("_Messages", ViewModel);
        }

        [Authorize(Roles = "admin")]

        public IActionResult FilterArticles(string filter)
        {
            List<Article> articles = new List<Article>();

            if (filter == "notpublic")
            {
                articles = DatabaseManipulator.GetAll<Article>("Article")
                                                  .Where(x => x.IsPublic == false)
                                                  .ToList();
            }
            else if (filter == "public")
            {
                articles = DatabaseManipulator.GetAll<Article>("Article")
                                                  .Where(x => x.IsPublic == true)
                                                  .ToList();
            }
            else if (filter == "all")
            {
                articles = DatabaseManipulator.GetAll<Article>("Article");
            }





            var messages = DatabaseManipulator.GetAll<Message>("Message");

            var ViewModel = new ManagementViewModel
            {
                Messages = messages,
                Articles = articles
            };


            return PartialView("_Articles", ViewModel);
        }

    }
}
