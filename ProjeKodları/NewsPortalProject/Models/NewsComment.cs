using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Models
{
    public class NewsComment
    {
        [Key]
        public int NewsCommentId { get; set; }
        [Required(ErrorMessage = "Comment Field Cannot be Left Blank!")]
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int NewsCastId { get; set; }
        public NewsCast NewsCast { get; set; }

    }
}