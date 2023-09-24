using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NewsPortalProject.DataAccessLayer;
using NewsPortalProject.Models;
using NewsPortalProject.Models.Id;
using NewsPortalProject.Repositories;

namespace NewsPortalProject.Controllers
{
    public class NewsCommentsController : Controller
    {
        
         NewsCommentRepositories commentRepository = new NewsCommentRepositories();
      
        [HttpPost]

        public ActionResult UserAddNewsComment(NewsComment newsComment)
        {
            newsComment.UserId = Id.UserId;
            bool status=commentRepository.Add(newsComment);
            if (status)
            {
                return RedirectToAction("MainNewsAll", "NewsCasts");
            }
            return RedirectToAction("MainNewsAll", "NewsCasts");

        }
    }
}
