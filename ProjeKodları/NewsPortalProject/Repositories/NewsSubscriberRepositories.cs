using NewsPortalProject.DataAccessLayer;
using NewsPortalProject.Interfaces;
using NewsPortalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NewsPortalProject.Repositories
{
    public class NewsSubscriberRepositories:IRepositories<NewsSubscriber>
    {
        NewsContext db = new NewsContext();

        public bool Add(NewsSubscriber entity)
        {
            User userId = db.User.FirstOrDefault(x => x.UserId == entity.UserId);
            NewsSource newsSourceId = db.NewsSource.FirstOrDefault(x => x.NewsSourceId == entity.NewsSourceId);
            var status=db.NewsSubscriber.FirstOrDefault(x => x.UserId == entity.UserId && x.NewsSourceId == entity.NewsSourceId);
            if (userId!=null && newsSourceId!=null && status==null )
            {
                db.NewsSubscriber.Add(entity);
                db.SaveChanges();
                return true;
            }
            if (status != null)
            {
                status.IsActive = true;
                db.SaveChanges();
                return true;
            }
            return true;

        }

        public bool Delete(int id)
        {
            bool exist = db.NewsSubscriber.Any(x => x.NewsSubscriberId == id);

            if (exist)
            {
                db.NewsSubscriber.Find(id).IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }

        public NewsSubscriber Get(int id)
        {
            NewsSubscriber NewsSubscriber = db.NewsSubscriber.Find(id);
            return NewsSubscriber;
        }

        public List<NewsSubscriber> GetAll()
        {
            var NewsSubscribersList = db.NewsSubscriber.Include(n=>n.NewsSource).Include(n=>n.User).Where(x=>x.IsActive==true).ToList();
            ;
            return NewsSubscribersList;
        }

        public bool Update(NewsSubscriber entity)
        {

            var NewsSubscriber = db.NewsSubscriber.Find(entity.UserId);
            bool status = false;
            if (NewsSubscriber != null)
            {
                NewsSubscriber.UserId = (db.User.Any(x => x.UserId == entity.UserId)) ? entity.UserId : NewsSubscriber.UserId;
                NewsSubscriber.NewsSourceId = (db.NewsSource.Any(x => x.NewsSourceId == entity.NewsSourceId)) ? entity.NewsSourceId : NewsSubscriber.NewsSourceId;
                db.SaveChanges();
                status = true;
            }
            return status;
        }

      
        public IEnumerable<object> UserGetSourceList(int id)
        {
            var query = from cast in db.NewsCast
                        join cate in db.NewsCategory on cast.NewsCategoryId equals cate.NewsCategoryId
                        join source in db.NewsSource on cast.NewsSourceId equals source.NewsSourceId
                        where source.IsActive == true
                        join subs in db.NewsSubscriber on source.NewsSourceId equals subs.NewsSourceId
                        where subs.IsActive == true && subs.UserId == id
                        join user in db.User on subs.UserId equals user.UserId
                        select new
                        {
                            Title = cast.Title,
                            Text = cast.Text,
                            NewsCastId = cast.NewsCastId,
                            MultimediaUrl = cast.MultimediaUrl,
                            NewsCategoryId = cate.NewsCategoryId,
                            CorpationName = source.CorpationName,
                            NewsSourceId = source.NewsSourceId
                        };

            return query.ToList();


        }
        public bool UserDeleteSubs(NewsSubscriber entity)
        {
            var status=db.NewsSubscriber.FirstOrDefault(x => x.UserId == entity.UserId && x.NewsSourceId == entity.NewsSourceId);
            if (status!=null )
            {
                status.IsActive = false;
                db.SaveChanges();
                return true;
            }
            return false;
           
        }


    }
}