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
    public class UsersController : Controller
    {
        UserRepository userRepository = new UserRepository();
        public ActionResult UsersDetail()
        {
            User user=userRepository.GetUserDetail(Id.UserId);
            return View(user);
        }
        [HttpGet]
       public ActionResult UsersUpdate()
        {
            User user = userRepository.GetUserDetail(Id.UserId);

            return View(user);
        }
        
        [HttpPost]

        public ActionResult UsersUpdate(User user, HttpPostedFileBase Image)
        {
            if (Image != null && Image.ContentLength > 0)
            {
                string imagePath = "";
                string imageName = "";

                imageName = Guid.NewGuid().ToString() + Path.GetFileName(Image.FileName);
                imagePath = Path.Combine(Server.MapPath("~/Images/UserImages"), imageName);
                Image.SaveAs(imagePath);
                user.ImageUrl = imageName;

            }
            user.UserId=Id.UserId;
            bool status = userRepository.Update(user);
            if (status)
            {
                return RedirectToAction("UsersDetail");
            }


            ViewBag.ErrorMessage = "Invalid Try Again";
            return View(user);
        }
    }
}
