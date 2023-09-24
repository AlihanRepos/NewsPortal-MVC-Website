using System;

using System.Linq;
using System.Net;

using System.Web.Mvc;

using NewsPortalProject.Models;
using NewsPortalProject.Repositories;

namespace NewsPortalProject.Controllers
{
    public class NewsCategoriesController : Controller
    {
        NewsCategoryRepositories newsCategoryRepositories=new NewsCategoryRepositories();
        // GET: NewsCategories
      
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                newsCategoryRepositories.Add(newsCategory);
                return RedirectToAction("SourceNews","Source");
            }

            return View(newsCategory);
        }

      
        public ActionResult PartialGetAllCategories()
        {
            return PartialView(newsCategoryRepositories.GetAll().ToList());
        }
       
    }
}
