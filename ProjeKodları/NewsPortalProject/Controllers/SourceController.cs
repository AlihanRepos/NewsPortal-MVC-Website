using NewsPortalProject.Models;
using NewsPortalProject.Models.Id;
using NewsPortalProject.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tweetinvi.Core.Models;

namespace NewsPortalProject.Controllers
{
    public class SourceController : Controller
    {
        NewsCastRepository castRepository=new NewsCastRepository();
        NewsCategoryRepositories categoryRepository=new NewsCategoryRepositories();
        [HttpGet]
        public ActionResult AddNews()
        {
            var caetegoryList=categoryRepository.GetAll();
            return View(caetegoryList);
        }


        [HttpPost]
        public ActionResult AddNews(NewsCast newsCast,HttpPostedFileBase Image)
        {
            if (Image != null && Image.ContentLength > 0)
            {
                string imagePath = "";
                string imageName = "";

                imageName = Guid.NewGuid().ToString() + Path.GetFileName(Image.FileName);
                imagePath = Path.Combine(Server.MapPath("~/Images/NewsImages"), imageName);
                Image.SaveAs(imagePath);
                newsCast.MultimediaUrl = imageName;
            }
            newsCast.NewsSourceId = Id.SourceId;

            bool status=castRepository.Add(newsCast);
            if (status)
            {
                return RedirectToAction("SourceNews");

            }
            return View();
        }

        public ActionResult SourceNews()
        {
           IEnumerable<object> objects=castRepository.SourceGetMainAllNews(Id.SourceId);
           return View(objects);
        }
    }
}