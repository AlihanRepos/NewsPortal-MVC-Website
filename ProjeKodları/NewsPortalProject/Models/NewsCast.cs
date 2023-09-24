using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Models
{
    public class NewsCast
    {
        [Key]
        public int NewsCastId { get; set; }
        [Required(ErrorMessage = "Title Field Cannot be Left Blank!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Text Field Cannot be Left Blank!")]
        public string Text { get; set; }
        public string MultimediaUrl { get; set; }
        public DateTime NewsDate { get; set; }= DateTime.Now;   
        public bool IsActive { get; set; } = true;
        [Required]
        public int NewsSourceId { get; set; }
        [Required]
        public int NewsCategoryId { get; set; }
        public NewsCategory NewsCategory { get; set; } = null;
        public NewsSource NewsSource { get; set; } = null;
        public List<NewsComment> NewsComment { get; set; } = null;
        public List<NewsFav> NewsFav { get; set; } = null;
        public List<NewsShare> NewsShare { get; set; } = null;
    }
}