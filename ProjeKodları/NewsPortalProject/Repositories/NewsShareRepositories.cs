using NewsPortalProject.DataAccessLayer;
using NewsPortalProject.Interfaces;
using NewsPortalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NewsPortalProject.Models.Id;

namespace NewsPortalProject.Repositories
{
    public class NewsShareRepositories:IRepositories<NewsShare>
    {
        NewsContext db = new NewsContext();

        public bool Add(NewsShare entity)
        {
            User userId = db.User.FirstOrDefault(x => x.UserId == entity.UserId);
            NewsCast newsCastId = db.NewsCast.FirstOrDefault(x => x.NewsCastId == entity.NewsCastId);
            if (userId != null && newsCastId != null)
            {
                db.NewsShare.Add(entity);
                db.SaveChanges();
                return true;
            }
            else { return false; }

        }

        public bool Delete(int id)
        {
            bool exist = db.NewsShare.Any(x => x.NewsShareId == id);

            if (exist)
            {
                db.NewsShare.Find(id).IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }

        public NewsShare Get(int id)
        {
            NewsShare NewsShare = db.NewsShare.Find(id);
            return NewsShare;
        }
        public List<NewsShare> GetAll()
        {
            List<NewsShare> NewsShareList = db.NewsShare.Include(n=>n.NewsCast).Include(n=>n.User).Where(x => x.IsActive == true).ToList();
            return NewsShareList;
        }

        public bool Update(NewsShare entity)
        {

            var NewsShare = db.NewsShare.Find(entity.UserId);
            bool status = false;
            if (NewsShare != null)
            {
                NewsShare.UserId = (db.User.Any(x => x.UserId == entity.UserId)) ? entity.UserId : NewsShare.UserId;
                NewsShare.NewsCastId = (db.NewsCast.Any(x => x.NewsCastId == entity.NewsCastId)) ? entity.NewsCastId : NewsShare.NewsCastId;
                db.SaveChanges();
                status = true;
            }
            return status;
        }

        public IEnumerable<object> GetAll(int id)
        {
            var querry = from share in db.NewsShare
                         where share.IsActive == true
                         join cast in db.NewsCast on share.NewsCastId equals cast.NewsCastId
                         where cast.IsActive == true
                         join user in db.User on id equals user.UserId
                         where share.UserId == id
                         where user.IsActive == true
                         join senduser in db.User on share.SendUserId equals senduser.UserId
                         where senduser.IsActive == true
                         select new
                         {
                             Title = cast.Title,
                             UName = user.Name,
                             SName = senduser.Name,
                             SImage=senduser.ImageUrl,
                             Date = share.ShareDate,
                             NewsShareId = share.NewsShareId
                         };
            return querry.ToList();


        }
        public IEnumerable<object> GetAllNotifation(int id)
        {
            var query = (from share in db.NewsShare
                         where share.IsActive == true
                         where share.SendUserId == id
                         join user in db.User on share.SendUserId equals user.UserId
                         where user.IsActive == true && user.UserId == share.SendUserId
                         join cast in db.NewsCast on share.NewsCastId equals cast.NewsCastId
                         where cast.IsActive == true
                         join gelenid in db.User on share.UserId equals gelenid.UserId 
                         select new
                         {
                             Title = cast.Title,
                             SName = gelenid.Name,
                             SImage = gelenid.ImageUrl,
                             Date = share.ShareDate,
                             NewsCastId=cast.NewsCastId   
                         }).Take(3);

            return query.ToList();





        }

    }
}