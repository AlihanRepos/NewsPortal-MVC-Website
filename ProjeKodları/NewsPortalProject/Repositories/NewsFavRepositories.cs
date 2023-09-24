using NewsPortalProject.DataAccessLayer;
using NewsPortalProject.Interfaces;
using NewsPortalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace NewsPortalProject.Repositories
{
    public class NewsFavRepositories : IRepositories<NewsFav>
    {
        NewsContext db = new NewsContext();
        
        public bool Add(NewsFav entity)
        {
            User userId = db.User.FirstOrDefault(x => x.UserId == entity.UserId);
            NewsCast newsCastId = db.NewsCast.FirstOrDefault(x => x.NewsCastId == entity.NewsCastId);
            if (userId != null && newsCastId != null )
            {
                db.NewsFav.Add(entity);
                db.SaveChanges();
                return true;
            }
          
            else { return false; }

        }

        public bool Delete(int id)
        {
            bool exist = db.NewsFav.Any(x => x.NewsFavId == id);

            if (exist)
            {
                db.NewsFav.Find(id).IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }

        public NewsFav Get(int id)
        {
            NewsFav NewsFav = db.NewsFav.Find(id);
            return NewsFav;
        }

        public List<NewsFav> GetAll()


        {
            var NewsFavList = db.NewsFav.Include(n => n.NewsCast).Include(n => n.User).Where(x => x.IsActive == true).ToList();


            return NewsFavList;
        }

        public bool Update(NewsFav entity)
        {

            var NewsFav = db.NewsFav.Find(entity.UserId);
            bool status = false;
            if (NewsFav != null)
            {
                NewsFav.UserId = (db.User.Any(x => x.UserId == entity.UserId)) ? entity.UserId : NewsFav.UserId;
                NewsFav.NewsCastId = (db.NewsCast.Any(x => x.NewsCastId == entity.NewsCastId)) ? entity.NewsCastId : NewsFav.NewsCastId;
                db.SaveChanges();
                status = true;
            }
            return status;
        }
        public IEnumerable<string> UserGet(int id)
        {
            var newsCasts = db.User
                                    .Where(u => u.UserId == id)
                                    .SelectMany(u => u.NewsFav)
                                    .Select(nf => nf.NewsCast.Title)
                                    .ToList();

            return newsCasts;
        }

        public IEnumerable<object> UserGetFav(int id)
        {
            var list = from u in db.User
                       where u.UserId == id
                       join f in db.NewsFav on u.UserId equals f.UserId
                       join c in db.NewsCast on f.NewsCastId equals c.NewsCastId
                       where f.IsActive == true
                       select new
                       {
                           Title = c.Title,
                           Text = c.Text,
                           NewsCastId = c.NewsCastId,
                           MultimediaUrl = c.MultimediaUrl,
                           NewsCategoryId = c.NewsCategoryId,
                       };

            return list;
        }

        public bool UserDelFav(int id, int newcastId)
        {
            NewsFav exist = db.NewsFav.FirstOrDefault(x => x.UserId == id && x.NewsCastId == newcastId);

            if (exist != null)
            {
                NewsFav newsFav = db.NewsFav.Find(exist.NewsFavId);
                newsFav.IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }
        public bool UserAdd(NewsFav entity)
        {
            User userId = db.User.FirstOrDefault(x => x.UserId == entity.UserId);
            NewsCast newsCastId = db.NewsCast.FirstOrDefault(x => x.NewsCastId == entity.NewsCastId);
            var status = db.NewsFav.FirstOrDefault(x => x.UserId == entity.UserId && x.NewsCastId == entity.NewsCastId);

            if (userId != null && newsCastId != null && status == null)
            {
                db.NewsFav.Add(entity);
                db.SaveChanges();
                return true;
            }
            if (userId != null && newsCastId != null && status != null)
            {
                var check = db.NewsFav.Find(status.NewsFavId);
                check.IsActive = true;
                db.SaveChanges();
                return true;

            }
            else { return false; }


        }
    }
}


