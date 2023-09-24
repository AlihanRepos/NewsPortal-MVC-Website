using NewsPortalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewsPortalProject.DataAccessLayer
{
    public class NewsContext:DbContext
    {
        public NewsContext() : base("dbConnection") { }
        public DbSet<User> User { get; set; }
        public DbSet<NewsCast> NewsCast { get; set; }
        public DbSet<NewsCategory> NewsCategory { get; set; }
        public DbSet<NewsComment> NewsComment { get; set; }
        public DbSet<NewsFav> NewsFav { get; set; }
        public DbSet<NewsShare> NewsShare { get; set; }
        public DbSet<NewsSource> NewsSource { get; set; }
        public DbSet<NewsSubscriber> NewsSubscriber { get; set; }
    }
}