using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Models
{
    public class NewsFav
    {

        [Key]
        public int NewsFavId { get; set; }
        public DateTime Fav { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int NewsCastId { get; set; }
        public NewsCast NewsCast { get; set; }

    }
}