using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Project.Models;

namespace Project.Controllers
{
    
    public class ArticlesController : Controller
    {
        public IActionResult Index()
        {
            var Articles = DatabaseManipulator.GetAll<Article>("Article").Where(article => article.IsPublic && article.PublishingDate <= DateTime.Today).ToList();

            return View(Articles);
        }

        public IActionResult Read(string ArticleName)
        {
            if (ArticleName != null)
            {

                string notFormattedTitle = ArticleName.Replace("-", " ");

                var ArticleMongo = DatabaseManipulator.GetByTitle<Article>(notFormattedTitle);

                // no reading non public posts with get
                if (ArticleMongo.IsPublic)
                {
                    return View(ArticleMongo);

                }

            }

            return RedirectToAction("");


        }

        [Authorize(Roles = "admin")]
        public IActionResult NewPost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewPost(Article model)
        {



            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loggedInUserId = DatabaseManipulator.GetUsersID(User.Identity.Name);

            var Article = new Article
            {
                AuthorId = loggedInUserId,
                Title = model.Title,
                Content = model.Content,
                PublishingDate = model.PublishingDate,
                IsPublic = model.IsPublic
            };

            DatabaseManipulator.Save(Article);

            return RedirectToAction("");
        }
        [Authorize]

        public IActionResult Manage()
        {

            var Articles = DatabaseManipulator.GetAll<Article>("Article").OrderByDescending(x => x.PublishingDate).ToList();


            return View(Articles);
        }

    }
}
