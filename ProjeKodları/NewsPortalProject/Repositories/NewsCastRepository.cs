using NewsPortalProject.DataAccessLayer;
using NewsPortalProject.Interfaces;
using NewsPortalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Ajax.Utilities;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tweetinvi;

namespace NewsPortalProject.Repositories
{
    public class NewsCastRepository:IRepositories<NewsCast>
    {
        NewsContext db = new NewsContext();

        public bool Add(NewsCast entity)
        {
            NewsCast newsCastTitle = db.NewsCast.FirstOrDefault(x => x.Title == entity.Title);
            NewsSource newsSourceId=db.NewsSource.FirstOrDefault(x=>x.NewsSourceId== entity.NewsSourceId);
            NewsCategory newsCategoryId=db.NewsCategory.FirstOrDefault(x=>x.NewsCategoryId== entity.NewsCategoryId);
            

            if (newsCastTitle==null && newsSourceId!=null && newsCategoryId!=null)
            {
                db.NewsCast.Add(entity);
                db.SaveChanges();
                return true;
            }
            else { return false; }

        }

        public bool Delete(int id)
        {
            bool exist = db.NewsCast.Any(x => x.NewsCastId == id);

            if (exist)
            {
                db.NewsCast.Find(id).IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }

        public NewsCast Get(int id)
        {
            NewsCast NewsCast = db.NewsCast.Find(id);
            return NewsCast;
        }

        public List<NewsCast> GetAll()
        {
            List<NewsCast> NewsCastList = db.NewsCast.Include(n=>n.NewsCategory).Include(n=>n.NewsSource).Include(n=>n.NewsComment).Where(x => x.IsActive == true).ToList();
            return NewsCastList;

        }

        public bool Update(NewsCast entity)
        {

            var NewsCast = db.NewsCast.Find(entity.NewsCastId);
            bool status = false;
            if (NewsCast != null)
            {
                NewsCast.Title = String.IsNullOrWhiteSpace(entity.Title) ? NewsCast.Title : entity.Title;
                NewsCast.Text = String.IsNullOrWhiteSpace(entity.Text) ? NewsCast.Text : entity.Text;
                NewsCast.MultimediaUrl = String.IsNullOrWhiteSpace(entity.MultimediaUrl) ? NewsCast.MultimediaUrl : entity.MultimediaUrl;
                NewsCast.NewsDate = DateTime.Now;
                NewsCast.IsActive = true;
                NewsCast.NewsSourceId = (db.NewsSource.Any(x => x.NewsSourceId == entity.NewsSourceId)) ? entity.NewsSourceId : NewsCast.NewsSourceId;
                NewsCast.NewsCategoryId = (db.NewsCategory.Any(x => x.NewsCategoryId == entity.NewsCategoryId)) ? entity.NewsCategoryId : NewsCast.NewsCategoryId;
                db.SaveChanges();
                status = true;
            }
            return status;
        }

        public IEnumerable<object> GetMainAllNews()
        {
             var query = from cast in db.NewsCast
                        join cate in db.NewsCategory on cast.NewsCategoryId equals cate.NewsCategoryId
                        join source in db.NewsSource on cast.NewsSourceId equals source.NewsSourceId
                        join comment in db.NewsComment on cast.NewsCastId equals comment.NewsCastId into commentJoin
                        from comment in commentJoin.DefaultIfEmpty()
                        join user in db.User on comment.UserId equals user.UserId into userJoin
                        from user in userJoin.DefaultIfEmpty()
                        group new { cast, comment, cate, source, user } by new { cast.Title, cast.Text } into groupedCast
                        select new
                        {
                            Title = groupedCast.Key.Title,
                            Text = groupedCast.Key.Text,
                            NewsCastId = groupedCast.FirstOrDefault().cast.NewsCastId,
                            MultimediaUrl = groupedCast.FirstOrDefault().cast.MultimediaUrl,
                            NewsCategoryId = groupedCast.FirstOrDefault().cate.NewsCategoryId,
                            CorpationName = groupedCast.FirstOrDefault().source.CorpationName,
                            NewsSourceId = groupedCast.FirstOrDefault().source.NewsSourceId,
                            Comments = groupedCast.Select(gc => new
                            {
                                Comment = gc.comment != null ? gc.comment.Comment : null,
                                UserName = gc.user != null ? gc.user.Name : null
                            }).ToList()
                        };
            return query.ToList();
        }
        public IEnumerable<object> GetMainNewsDetails(NewsCast entity)
        {
            var query = from cast in db.NewsCast
                        where cast.NewsCastId == entity.NewsCastId
                        join cate in db.NewsCategory on cast.NewsCategoryId equals cate.NewsCategoryId
                        join source in db.NewsSource on cast.NewsSourceId equals source.NewsSourceId
                        join comment in db.NewsComment on cast.NewsCastId equals comment.NewsCastId into commentJoin
                        from comment in commentJoin.DefaultIfEmpty()
                        join user in db.User on comment.UserId equals user.UserId into userJoin
                        from user in userJoin.DefaultIfEmpty()
                        group new { cast, comment, cate, source, user } by new { cast.Title, cast.Text } into groupedCast
                        select new
                        {
                            Title = groupedCast.Key.Title,
                            Text = groupedCast.Key.Text,
                            NewsCastId = groupedCast.FirstOrDefault().cast.NewsCastId,
                            MultimediaUrl = groupedCast.FirstOrDefault().cast.MultimediaUrl,
                            NewsCategoryId = groupedCast.FirstOrDefault().cate.NewsCategoryId,
                            CorpationName = groupedCast.FirstOrDefault().source.CorpationName,
                            NewsSourceId = groupedCast.FirstOrDefault().source.NewsSourceId,
                            Comments = groupedCast.Select(gc => new
                            {
                                Comment = gc.comment != null ? gc.comment.Comment : null,
                                UserName = gc.user != null ? gc.user.Name : null
                            }).ToList()
                        };
            return query.ToList();
        }
        public IEnumerable<object> GetMainAllCategoryNews(NewsCast entity)
        {
            var query = from cast in db.NewsCast
                        where cast.NewsCategoryId == entity.NewsCategoryId
                        join cate in db.NewsCategory on cast.NewsCategoryId equals cate.NewsCategoryId
                        join source in db.NewsSource on cast.NewsSourceId equals source.NewsSourceId
                        join comment in db.NewsComment on cast.NewsCastId equals comment.NewsCastId into commentJoin
                        from comment in commentJoin.DefaultIfEmpty()
                        join user in db.User on comment.UserId equals user.UserId into userJoin
                        from user in userJoin.DefaultIfEmpty()
                        group new { cast, comment, cate, source, user } by new { cast.Title, cast.Text } into groupedCast
                        select new
                        {
                            Title = groupedCast.Key.Title,
                            Text = groupedCast.Key.Text,
                            NewsCastId = groupedCast.FirstOrDefault().cast.NewsCastId,
                            MultimediaUrl = groupedCast.FirstOrDefault().cast.MultimediaUrl,
                            NewsCategoryId = groupedCast.FirstOrDefault().cate.NewsCategoryId,
                            CorpationName = groupedCast.FirstOrDefault().source.CorpationName,
                            NewsSourceId = groupedCast.FirstOrDefault().source.NewsSourceId,
                            Comments = groupedCast.Select(gc => new
                            {
                                Comment = gc.comment != null ? gc.comment.Comment : null,
                                UserName = gc.user != null ? gc.user.Name : null
                            }).ToList()
                        };
            return query.ToList();
        }

        public IEnumerable<object> GetMainOrderFavs()
        {
            var query = (from cast in db.NewsCast
                         join cate in db.NewsCategory on cast.NewsCategoryId equals cate.NewsCategoryId
                         join source in db.NewsSource on cast.NewsSourceId equals source.NewsSourceId
                         join comment in db.NewsComment on cast.NewsCastId equals comment.NewsCastId into commentJoin
                         from comment in commentJoin.DefaultIfEmpty()
                         join user in db.User on comment.UserId equals user.UserId into userJoin
                         from user in userJoin.DefaultIfEmpty()
                         join fav in db.NewsFav on cast.NewsCastId equals fav.NewsCastId into favJoin
                         from fav in favJoin.DefaultIfEmpty()
                         where fav.IsActive==true
                         group new { cast, comment, cate, source, user, fav } by new { cast.Title, cast.Text } into groupedCast
                         let favoriteCount = groupedCast.Select(gc => gc.fav != null ? gc.fav.NewsFavId : (int?)null).Distinct().Count(f => f != null)
                         orderby favoriteCount descending, groupedCast.Max(gc => gc.cast.NewsDate) descending
                         select new
                         {
                             Title = groupedCast.Key.Title,
                             Text = groupedCast.Key.Text,
                             NewsCastId = groupedCast.FirstOrDefault().cast.NewsCastId,
                             MultimediaUrl = groupedCast.FirstOrDefault().cast.MultimediaUrl,
                             NewsCategoryId = groupedCast.FirstOrDefault().cate.NewsCategoryId,
                             CorpationName = groupedCast.FirstOrDefault().source.CorpationName,
                             NewsSourceId = groupedCast.FirstOrDefault().source.NewsSourceId,
                             NewsDate= groupedCast.FirstOrDefault().cast.NewsDate,
                             FavoriteCount = favoriteCount,
                             Comments = groupedCast.Select(gc => new
                             {
                                 Comment = gc.comment != null ? gc.comment.Comment : null,
                                 UserName = gc.user != null ? gc.user.Name : null
                             }).ToList()
                         });

            return query.ToList();





        }


        public List<NewsCast> SearchNews(string search)
        {
            var results = db.NewsCast.Where(n => n.Title.Contains(search)).ToList();
            return results;
        }



        public async Task<bool> SendTweetsCast(NewsCast entity)
        {
            var userClient = new TwitterClient("WClag0duSP4BM5l4wK70juMTj", "3c2l5zosrjFL50j821zS4Psga82eQOuZ4c8JMBdOeDLao3X0aG", "1669695665469902850-ZJDfZgTafmxuLqHNOPdBXigNB2tVLk", "TEuCZpDaBVHaoJ7lHIethPcgVWFXfJ4fxVz2xS5jKYqyT");

            var user = await userClient.Users.GetAuthenticatedUserAsync();

            var tweetcontentid = db.NewsCast.Find(entity.NewsCastId);

            if (tweetcontentid != null)
            {
                var tweet = await userClient.Tweets.PublishTweetAsync(tweetcontentid.Title);

                return true;
            }
            return false;

        }

        public IEnumerable<object> SourceGetMainAllNews(int id)
        {
            var query = from cast in db.NewsCast
                        join cate in db.NewsCategory on cast.NewsCategoryId equals cate.NewsCategoryId
                        join source in db.NewsSource on cast.NewsSourceId equals source.NewsSourceId
                        where source.NewsSourceId == id
                        join comment in db.NewsComment on cast.NewsCastId equals comment.NewsCastId into commentJoin
                        from comment in commentJoin.DefaultIfEmpty()
                        join user in db.User on comment.UserId equals user.UserId into userJoin
                        from user in userJoin.DefaultIfEmpty()
                        group new { cast, comment, cate, source, user } by new { cast.Title, cast.Text } into groupedCast
                        select new
                        {
                            Title = groupedCast.Key.Title,
                            Text = groupedCast.Key.Text,
                            NewsCastId = groupedCast.FirstOrDefault().cast.NewsCastId,
                            MultimediaUrl = groupedCast.FirstOrDefault().cast.MultimediaUrl,
                            NewsCategoryId = groupedCast.FirstOrDefault().cate.NewsCategoryId,
                            CorpationName = groupedCast.FirstOrDefault().source.CorpationName,
                            NewsSourceId = groupedCast.FirstOrDefault().source.NewsSourceId,
                            Comments = groupedCast.Select(gc => new
                            {
                                Comment = gc.comment != null ? gc.comment.Comment : null,
                                UserName = gc.user != null ? gc.user.Name : null
                            }).ToList()
                        };
            return query.ToList();
        }



    }
}