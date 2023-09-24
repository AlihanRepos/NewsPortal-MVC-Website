using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsPortalProject.Models;
using NewsPortalProject.Models.Id;
using NewsPortalProject.Repositories;

namespace NewsPortalProject.Controllers
{
    public class NewsFavsController : Controller
    {
        NewsFavRepositories favRepositories = new NewsFavRepositories();
      


        public ActionResult UserFavDetail()
        {
            int id = Id.UserId;
            var newsFav = favRepositories.UserGetFav(Convert.ToInt32(id)); 
            return View(newsFav);

        }

        [HttpPost]
        public ActionResult UserFavAdd(NewsFav entity)
        {
            int id = Id.UserId;
            entity.UserId = id;

            favRepositories.UserAdd(entity);

            return RedirectToAction("UserFavDetail");

        }

        [HttpPost]
        public ActionResult UserDelFav(int NewsCastId)
        {
            int id = Id.UserId;
         
            favRepositories.UserDelFav(id,NewsCastId);

            return RedirectToAction("UserFavDetail");

        }

    }
}
