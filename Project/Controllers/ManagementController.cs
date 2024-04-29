using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Project.Models;
using Slugify;

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
        public async Task<IActionResult> AddService(ServiceViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var titleImage = model.TitleImage;
            var imagePath = "";

            if (titleImage != null && titleImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(titleImage.FileName);
                var filePath = "wwwroot/images/Services/" + fileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await titleImage.CopyToAsync(stream);
                }

                imagePath = "images/Services/" + fileName;

            }
            else
            {
                imagePath = "images/Services/default.webp";
            }

            var Service = new Service
            {
                ServiceName = model.ServiceName,
                ServiceDescription = model.ServiceDescription,
                ServicePrice = model.ServicePrice,
                AdditionalInformation = model.AdditionalInformation,
                ImagePath = imagePath
            };


            DatabaseManipulator.Save(Service);

            return RedirectToAction("index");

        }

        [Authorize(Roles = "admin")]

        public IActionResult EditService(ObjectId ServiceId)
        {
            if (ServiceId != ObjectId.Empty)
            {
                var service = DatabaseManipulator.GetById<Service>(ServiceId, "Service");

                var ViewModel = new ServiceViewModel
                {
                    _id = service._id,
                    ServiceName = service.ServiceName,
                    ServiceDescription = service.ServiceDescription,
                    ServicePrice = service.ServicePrice,
                    AdditionalInformation = service.AdditionalInformation
                };

                return View(ViewModel);
            }

            return PartialView("_Services");
        }

        [Authorize(Roles = "admin")]

        public IActionResult EditArticle(ObjectId articleID)
        {
            if (articleID != ObjectId.Empty)
            {
                var article = DatabaseManipulator.GetById<Article>(articleID, "Article");

                var ViewModel = new ArticleViewModel
                {
                    _id = article._id,
                    AuthorId = article.AuthorId,
                    Title = article.Title,
                    Content = article.Content,
                    IsPublic = article.IsPublic,
                    PublishingDate = article.PublishingDate,
                    TitleImage = null
                };


                return View(ViewModel);
            }

            return PartialView("_Articles");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditService(ServiceViewModel model)
        {

            var titleImage = model.TitleImage;
            var imagePath = "";

            if (titleImage != null && titleImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(titleImage.FileName);
                var filePath = "wwwroot/images/Services/" + fileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await titleImage.CopyToAsync(stream);
                }

                imagePath = "images/Services/" + fileName;

            }
            else if (titleImage == null)
            {
                var data = DatabaseManipulator.GetById<Service>(model._id, "Service");
                imagePath = data.ImagePath;
            }
            // safety feature :-)
            else
            {
                imagePath = "images/Services/default.webp";
            }


            var Service = new Service
            {
                _id = model._id,
                ServiceName = model.ServiceName,
                ServiceDescription = model.ServiceDescription,
                ServicePrice = model.ServicePrice,
                AdditionalInformation = model.AdditionalInformation,
                ImagePath = imagePath

            };

            DatabaseManipulator.Save(Service);

            return RedirectToAction("");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditArticle(ArticleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var Slug = new SlugHelper().GenerateSlug(model.Title);

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

            } else if (titleImage == null)
            {
                var data = DatabaseManipulator.GetById<Article>(model._id, "Article");
                imagePath = data.ImagePath;
            }
            // safety feature :-)
            else
            {
                imagePath = "images/Articles/default.webp";
            }


            var Article = new Article
            {
                _id = model._id,
                AuthorId = model.AuthorId,
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



        public IActionResult WebsiteProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WebsiteProfile(WebsiteProfile model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            DatabaseManipulator.Save(model);

            return RedirectToAction("Index");
        }


    }
}
