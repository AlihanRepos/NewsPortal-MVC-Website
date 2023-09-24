
using System.Web.Mvc;
using NewsPortalProject.Models;
using NewsPortalProject.Models.Id;
using NewsPortalProject.Repositories;

namespace NewsPortalProject.Controllers
{
    public class NewsSubscribersController : Controller
    {

        NewsSubscriberRepositories subscriberRepositories=new NewsSubscriberRepositories();
    


     
        public ActionResult UserAddSubs()
        {
           var subscriber= subscriberRepositories.UserGetSourceList(Id.UserId);
            return View(subscriber);

        }

        [HttpPost]
        public ActionResult UserAddSubs(NewsSubscriber newsSubscriber)
        {
            newsSubscriber.UserId = Id.UserId;
            bool status=subscriberRepositories.Add(newsSubscriber);
            if (status)
            {
                return RedirectToAction("UserAddSubs");
            }
            return RedirectToAction("UserAddSubs");

        }

        [HttpPost]
        public ActionResult UserDelSubs(NewsSubscriber newsSubscriber)
        {
            newsSubscriber.UserId = Id.UserId;
            bool status = subscriberRepositories.UserDeleteSubs(newsSubscriber);
            if (status)
            {
                return RedirectToAction("UserAddSubs");
            }
            return RedirectToAction("UserAddSubs");

        }
        
    }
}
