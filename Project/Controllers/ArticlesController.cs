using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Project.Models;
using Slugify;

namespace Project.Controllers
{
    [Route("Articles")]
    public class ArticlesController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            var Articles = DatabaseManipulator.GetAll<Article>("Article").Where(article => article.IsPublic && article.PublishingDate <= DateTime.Today).ToList();

            return View(Articles);
        }


        //public IActionResult Read(string ArticleName)
        //{
        //    if (ArticleName != null)
        //    {

        //        string notFormattedTitle = ArticleName.Replace("-", " ");

        //        var ArticleMongo = DatabaseManipulator.GetByTitle<Article>(notFormattedTitle);

        //        // no reading non public posts with get
        //        if (ArticleMongo.IsPublic)
        //        {
        //            return View(ArticleMongo);

        //        }

        //    }

        //    return RedirectToAction("");
        //}

        [HttpGet("NewPost")]
        [Authorize(Roles = "admin")]
        public IActionResult NewPost()
        {
            return View();
        }


        [HttpPost("NewPost")]
        [Authorize]
        public async Task<IActionResult> NewPost(Article model)
        {

            model.Slug = new SlugHelper().GenerateSlug(model.Title);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var check = DatabaseManipulator.GetBySlug<Article>(model.Slug);

            // No duplicate slugs or title
            // kinda meh mebe fix
            if (check != null) { return View(model); };

            var loggedInUserId = DatabaseManipulator.GetUsersID(User.Identity.Name);

            var Article = new Article
            {
                AuthorId = loggedInUserId,
                Title = model.Title,
                Content = model.Content,
                PublishingDate = model.PublishingDate,
                IsPublic = model.IsPublic,
                Slug = model.Slug
            };

            DatabaseManipulator.Save(Article);

            return RedirectToAction("");
        }

        [HttpGet("/Article/{slug}")]
        public IActionResult Read(string slug)
        {

            var testi = DatabaseManipulator.GetBySlug<Article>(slug);

            return View(testi);
        }


    }
}
