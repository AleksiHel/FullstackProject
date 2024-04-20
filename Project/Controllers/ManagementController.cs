using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Project.Models;

namespace Project.Controllers
{
    public class ManagementController : Controller
    {
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

        [Authorize]
        public ActionResult Delete(ObjectId id)
        {


            DatabaseManipulator.Delete(DatabaseManipulator.GetById<Message>(id, "Message"));

            return RedirectToAction("index");
        }

        public ActionResult markanswered(ObjectId id)
        {

            var entry = DatabaseManipulator.GetById<Message>(id, "Message");

            entry.Answered = true;

            DatabaseManipulator.Save(entry);


            return RedirectToAction("index");
        }

        public IActionResult ManageArticles()
        {
            var messages = DatabaseManipulator.GetAll<Message>("Message");
            var articles = DatabaseManipulator.GetAll<Article>("Article");

            var ViewModel = new ManagementViewModel
            {
                Messages = messages,
                Articles = articles
            };


            return PartialView("_Articles", ViewModel);
        }

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

        [Authorize]

        public IActionResult Edit(ObjectId articleID)
        {
            if (articleID != ObjectId.Empty)
            {
                var article = DatabaseManipulator.GetById<Article>(articleID, "Article");

                return View(article);
            }

            return RedirectToAction("index", "index/manage");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(Article model)
        {



            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Article = new Article
            {
                _id = model._id,
                AuthorId = model.AuthorId,
                Title = model.Title,
                Content = model.Content,
                PublishingDate = model.PublishingDate,
                IsPublic = model.IsPublic
            };

            DatabaseManipulator.Save(Article);

            return RedirectToAction("index");
        }

    }
}
