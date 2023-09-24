using NewsPortalProject.DataAccessLayer;
using NewsPortalProject.Interfaces;
using NewsPortalProject.Models;
using NewsPortalProject.Models.Id;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Repositories
{
    public class NewsSourceRepositories:IRepositories<NewsSource>
    {

        NewsContext db = new NewsContext();

        public bool Add(NewsSource entity)
        {
            NewsSource newsSourceCorptaionname = db.NewsSource.FirstOrDefault(x => x.CorpationName == entity.CorpationName);
            bool exist = (entity.CorpationPassword != null);
            if (newsSourceCorptaionname==null && exist) 
            {
                db.NewsSource.Add(entity);
                db.SaveChanges();
                return true;
            }
            else { return false; }

        }

        public bool Delete(int id)
        {
            bool exist = db.NewsSource.Any(x => x.NewsSourceId == id);

            if (exist)
            {
                db.NewsSource.Find(id).IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }

        public NewsSource Get(int id)
        {
            NewsSource NewsSource = db.NewsSource.Find(id);
            return NewsSource;
        }

        public List<NewsSource> GetAll()
        {
            List<NewsSource> NewsSourcesList = db.NewsSource.Where(x => x.IsActive == true).ToList();
            return NewsSourcesList;
        }

        public bool Update(NewsSource entity)
        {

            var NewsSource = db.NewsSource.Find(entity.NewsSourceId);
            bool status = false;
            if (NewsSource != null)
            {
                NewsSource.CorpationName = String.IsNullOrWhiteSpace(entity.CorpationName) ? NewsSource.CorpationName : entity.CorpationName;
                NewsSource.CorpationPassword = String.IsNullOrWhiteSpace(entity.CorpationPassword) ? NewsSource.CorpationPassword : entity.CorpationPassword;
                NewsSource.IsActive= true;
                db.SaveChanges();
                status = true;
            }
            return status;
        }

        public bool LoginControl(NewsSource entity)
        {

            NewsSource newsSource = db.NewsSource.FirstOrDefault(x => x.CorpationName == entity.CorpationName && x.CorpationPassword == entity.CorpationPassword && x.IsActive == entity.IsActive);

            if (newsSource != null)
            {
                Id.SourceId = newsSource.NewsSourceId;

                return true;
            }

            return false;
        }
        

    }
}