using NewsPortalProject.Models;
using NewsPortalProject.Models.Id;
using NewsPortalProject.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewsPortalProject.Controllers
{

    public class LoginController : Controller
    {
        
        
        UserRepository userRepository =new UserRepository();
       
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult Login() { 
            return View();
        
        }
        [HttpPost]
        public ActionResult Login(User user) {
           bool status= userRepository.LoginControl(user);
            if (status)
            {
                Id.UserId = userRepository.GetUserId(user);
                return RedirectToAction("MainNewsAll", "NewsCasts");
            }
            ViewBag.ErrorMessage = "Invalid Email Password Try Again";
            return View();
        }
     

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user,HttpPostedFileBase Image)
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

                bool status = userRepository.Add(user);
                if (status)
                {
                    return RedirectToAction("Login");
                }
            
           
            ViewBag.ErrorMessage = "Invalid Email Password Try Again";
            return View(user);
        }

        public ActionResult LogOut()
        {
            Id.UserId = 0;
            Id.NewsCastId = 0;
            return RedirectToAction("Index");
        }
     
    }
}
