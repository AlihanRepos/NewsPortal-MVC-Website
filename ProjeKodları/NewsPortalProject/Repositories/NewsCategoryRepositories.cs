using NewsPortalProject.DataAccessLayer;
using NewsPortalProject.Interfaces;
using NewsPortalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Repositories
{
    public class NewsCategoryRepositories:IRepositories<NewsCategory>
    {
        NewsContext db = new NewsContext();

        public bool Add(NewsCategory entity)
        {
            NewsCategory newsCategoryCategory = db.NewsCategory.FirstOrDefault(x => x.Category == entity.Category);

         
            if (newsCategoryCategory==null)
            {
                db.NewsCategory.Add(entity);
                db.SaveChanges();
                return true;
            }
            else { return false; }

        }

        public bool Delete(int id)
        {
            bool exist = db.NewsCategory.Any(x => x.NewsCategoryId == id);

            if (exist)
            {
                db.NewsCategory.Find(id).IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }

        public NewsCategory Get(int id)
        {
            NewsCategory NewsCategory = db.NewsCategory.Find(id);
            return NewsCategory;
        }

        public List<NewsCategory> GetAll()
        {
            List<NewsCategory> NewsCategoryList = db.NewsCategory.Where(x => x.IsActive == true).ToList();
            return NewsCategoryList;
        }

        public bool Update(NewsCategory entity)
        {

            var NewsCategory = db.NewsCategory.Find(entity.NewsCategoryId);
            bool status = false;
            if (NewsCategory != null)
            {
                NewsCategory.Category = String.IsNullOrWhiteSpace(entity.Category) ? NewsCategory.Category : entity.Category;
                NewsCategory.DateCategory = DateTime.Now;
                NewsCategory.IsActive = true;
                db.SaveChanges();
                status = true;
            }
            return status;
        }
    }
}