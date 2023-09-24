using NewsPortalProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Models
{
    public class User
    {

        [Key] 
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name Field Cannot be Left Blank!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname Field Cannot be Left Blank!")]

        public string Surname { get; set; }
        [Required(ErrorMessage = "Age Field Cannot be Left Blank!")]

        public int Age { get; set; }
        [Required(ErrorMessage = "Age Field Cannot be Left Blank!")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password Field Cannot be Left Blank!")]
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime RegisterDate { get; set; }= DateTime.Now;
        [Required]
        public bool IsActive { get; set; } = true;
        public List<NewsComment> NewsComments { get; set; } = null;
        public List<NewsFav> NewsFav { get; set; }=null;
        
        public List<NewsShare> NewsShare { get; set; } = null;
        
        public List<NewsSubscriber> NewsSubscriber { get; set; } = null;

    }
}