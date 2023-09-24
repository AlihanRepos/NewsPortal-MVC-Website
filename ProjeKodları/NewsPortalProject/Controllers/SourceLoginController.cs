using NewsPortalProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsPortalProject.Models;
using NewsPortalProject.Models.Id;

namespace NewsPortalProject.Controllers
{
    public class SourceLoginController : Controller
    {
        // GET: SourceLogin
        NewsSourceRepositories sourceRepositories=new NewsSourceRepositories();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(NewsSource newsSource)
        {

            bool status = sourceRepositories.LoginControl(newsSource);
            if (status)
            {
                return RedirectToAction("SourceNews", "Source");
            }
            ViewBag.ErrorMessage = "Invalid Corpation Name Password Try Again";
            return View();
        }


        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(NewsSource newsSource)
        {
            bool status = sourceRepositories.Add(newsSource);
            if (status)
            {
                return RedirectToAction("Login");
            }
            ViewBag.ErrorMessage = "Invalid Corpation Name Password Try Again";
            return View(newsSource);
        }

        public ActionResult LogOut()
        {
            Id.UserId = 0;
            Id.NewsCastId = 0;
            return RedirectToAction("Index","Login");
        }
    }
}