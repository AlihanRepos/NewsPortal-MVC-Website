using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NewsPortalProject.Models;
using NewsPortalProject.Models.Id;
using NewsPortalProject.Repositories;


namespace NewsPortalProject.Controllers
{
    public class NewsSharesController : Controller
    {
       
        NewsShareRepositories shareRepositories = new NewsShareRepositories();
        UserRepository userRepository = new UserRepository();
        public ActionResult NotUserFound()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ShareToUser(int NewsCastId,string Email)
        {
            User user=userRepository.GetShareEmailControl(Email);

            if (user==null)
            {
              return  RedirectToAction("NotUserFound");
            }
            else
            {
                NewsShare newsShare= new NewsShare();
                newsShare.UserId = Id.UserId;
                newsShare.NewsCastId = NewsCastId;
                newsShare.SendUserId=user.UserId;
               if (shareRepositories.Add(newsShare))
                {
                    return RedirectToAction("GetShareTable");

                }
                return RedirectToAction("NotUserFound");
            }
        }

        public ActionResult GetShareTable()
        {
            int id;
            id=Id.UserId;
            var newsShare = shareRepositories.GetAll(id);
            return View(newsShare);
        }

        public ActionResult SharredNotifation()
        {
            int id;
            id=Id.UserId;
            var newsShare = shareRepositories.GetAllNotifation(id);
            return PartialView(newsShare);
        }



    }
}
