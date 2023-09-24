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

    public class NewsCommentRepositories : IRepositories<NewsComment>
    {
        NewsContext db = new NewsContext();

        public bool Add(NewsComment entity)
        {
            
            NewsCast newsCastId=db.NewsCast.FirstOrDefault(x=>x.NewsCastId==entity.NewsCastId);
            User userId = db.User.FirstOrDefault(x => x.UserId == entity.UserId);

            bool exist = entity.Comment != null;
            if (newsCastId!=null&&userId!=null&&exist)
            {
                db.NewsComment.Add(entity);
                db.SaveChanges();
                return true;
            }
            else { return false; }

        }

        public bool Delete(int id)
        {
            bool exist = db.NewsComment.Any(x => x.NewsCommentId == id);

            if (exist)
            {
                db.NewsComment.Find(id).IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }

        public NewsComment Get(int id)
        {
            NewsComment NewsComment = db.NewsComment.Find(id);
            return NewsComment;
        }

        public List<NewsComment> GetAll()
        {
            List<NewsComment> NewsCommentList = db.NewsComment.Include(n => n.NewsCast).Include(n => n.User).Where(x => x.IsActive == true).ToList();
            return NewsCommentList;
        }

        public bool Update(NewsComment entity)
        {

            var NewsComment = db.NewsComment.Find(entity.UserId);
            bool status = false;
            if (NewsComment != null)
            {
                NewsComment.Comment = String.IsNullOrWhiteSpace(entity.Comment) ? NewsComment.Comment : entity.Comment;
                NewsComment.UserId = (db.User.Any(x => x.UserId == entity.UserId)) ? entity.UserId : NewsComment.UserId;
                NewsComment.NewsCastId = (db.NewsCast.Any(x => x.NewsCastId == entity.NewsCastId)) ? entity.NewsCastId : NewsComment.NewsCastId;
                db.SaveChanges();
                status = true;
            }
            return status;
        }
    }
}