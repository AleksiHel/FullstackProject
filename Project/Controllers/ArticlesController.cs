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
        private readonly IWebHostEnvironment _hostingEnvironment;

        [HttpGet("")]
        public IActionResult Index()
        {
            var Articles = DatabaseManipulator.GetAll<Article>("Article").Where(article => article.IsPublic && article.PublishingDate <= DateTime.Today).ToList();

            return View(Articles);


        }



        [HttpGet("NewPost")]
        [Authorize(Roles = "admin")]
        public IActionResult NewPost()
        {
            return View();
        }


        [HttpPost("NewPost")]
        [Authorize]
        public async Task<IActionResult> NewPost(ArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Slug = new SlugHelper().GenerateSlug(model.Title);
            var check = DatabaseManipulator.GetBySlug<Article>(Slug);

            // No duplicate slugs or title
            // kinda meh I'd rather use ids
            // mebe fix
            if (check != null) { return View(model); };

            var titleImage = model.TitleImage;
            var imagePath = "";

            if (titleImage != null && titleImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(titleImage.FileName);
                var filePath = "wwwroot/images/Articles/" + fileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await titleImage.CopyToAsync(stream);
                }

               imagePath = "images/Articles/" + fileName;
            }
            else
            {
                imagePath = "images/Articles/default.webp";
            }


            var loggedInUserId = DatabaseManipulator.GetUsersID(User.Identity.Name);

            var Article = new Article
            {
                AuthorId = loggedInUserId,
                Title = model.Title,
                Content = model.Content,
                PublishingDate = model.PublishingDate,
                IsPublic = model.IsPublic,
                Slug = Slug,
                ImagePath = imagePath
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
