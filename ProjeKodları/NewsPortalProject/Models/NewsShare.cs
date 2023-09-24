using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Models
{
    public class NewsShare
    {

        [Key]
        public int NewsShareId { get; set; }

        public DateTime ShareDate { get; set; }=DateTime.Now;

        public bool IsActive { get; set; } = true;
        public int SendUserId { get; set; }
        [Required]
        public int UserId { get; set; }
      

        public User User { get; set; }
        [Required]

        public int NewsCastId { get; set; }
 

        public NewsCast NewsCast { get; set; }

    }
}