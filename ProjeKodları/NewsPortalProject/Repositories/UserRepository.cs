
using NewsPortalProject.DataAccessLayer;
using NewsPortalProject.Interfaces;
using NewsPortalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace NewsPortalProject.Repositories
{
    public class UserRepository : IRepositories<User>
    {
        NewsContext db = new NewsContext();

        public bool Add(User entity)
        {
            User user= db.User.FirstOrDefault(x => x.Email == entity.Email);

            bool exist =(entity.Password!=null && entity.Name!=null&& entity.Surname!=null);
            if (user==null && exist)
            {
                db.User.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
              return false;
            }

        }

        public bool Delete(int id)
        {
            bool exist=db.User.Any(x => x.UserId==id);

            if (exist)
            {
                db.User.Find(id).IsActive = false;
                db.SaveChanges();
                return true;

            }
            else { return false; }
        }

        public User Get(int id)

        {
            User user = db.User.Find(id);
            return  user;
        }

        public List<User> GetAll()
        {
            List<User> UserList =db.User.Where(x=>x.IsActive==true).ToList();
            return UserList;
        }

        public bool Update(User entity)
        {

            var user = db.User.Find(entity.UserId);
            bool status = false;
            if (user != null)
            {
                user.Name = String.IsNullOrWhiteSpace(entity.Name) ? user.Name : entity.Name;
                user.Surname = String.IsNullOrWhiteSpace(entity.Surname) ? user.Surname : entity.Surname;
                user.Age = entity.Age==0 ? user.Age : entity.Age;
                user.Email = String.IsNullOrWhiteSpace(entity.Email) ? user.Email : entity.Email;
                user.Password = String.IsNullOrWhiteSpace(entity.Password) ? user.Password : entity.Password;
                user.ImageUrl = String.IsNullOrWhiteSpace(entity.ImageUrl) ? user.ImageUrl : entity.ImageUrl;
                user.RegisterDate = DateTime.Now;
                user.IsActive= true;
                db.SaveChanges();
                status = true;
            }
            return status;
        }
        public bool LoginControl(User entity)
        {

            User user = db.User.FirstOrDefault(x => x.Email == entity.Email && x.Password==entity.Password && x.IsActive==entity.IsActive);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        public int GetUserId(User entity)
        {
            User user = db.User.FirstOrDefault(x => x.Email == entity.Email);
            return user.UserId;
            
        }

        public User GetUserDetail(int id)
        {
            User user = db.User.FirstOrDefault(x=>x.UserId == id);
            return user;

        }

        public User GetShareEmailControl(string Email)
        {
          User user=db.User.FirstOrDefault(x=> x.Email == Email);
            return user;
        }
    }
}