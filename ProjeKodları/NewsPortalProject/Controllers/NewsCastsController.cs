using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NewsPortalProject.Models;
using NewsPortalProject.Repositories;
using Tweetinvi;
using Tweetinvi.Core.Models;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace NewsPortalProject.Controllers
{
    public class NewsCastsController : Controller
    {
        NewsCastRepository castRepository = new NewsCastRepository();
        NewsCategoryRepositories categoryRepository = new NewsCategoryRepositories();
        NewsSourceRepositories sourceRepository = new NewsSourceRepositories();

        // GET: NewsCasts
     

        [HttpPost]
        public ActionResult MainNewsCategory(NewsCast newsCast)
        {
            var list = castRepository.GetMainAllCategoryNews(newsCast);
            return View(list.ToList());
        }

        public ActionResult MainNewsAll()
        {
            var list = castRepository.GetMainAllNews();
            return View(list.ToList());
        }

        public ActionResult MainNewsPopuler()
        {
            var list = castRepository.GetMainOrderFavs();
            return View(list.ToList());
        }


        [HttpPost]
        public ActionResult MainNewsAllDetail(NewsCast newsCast)
        {
            var list = castRepository.GetMainNewsDetails(newsCast);
            return View(list.ToList());
        }

        [HttpPost]
        public ActionResult SearchNews(string query)
        {
            var results=castRepository.SearchNews(query);
            return View(results);
        }

        public ActionResult SendTweet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendTweet(NewsCast entity)
        {
            Task<bool> status = castRepository.SendTweetsCast(entity);

            if (status.Result)
            {
                return View();
            }
            return RedirectToAction("MainNewsAll","NewsCasts");
            
        }

    }
}
