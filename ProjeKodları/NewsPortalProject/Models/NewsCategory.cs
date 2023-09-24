using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Models
{
    public class NewsCategory
    {

        [Key]
        public int NewsCategoryId { get; set; }
        [Required(ErrorMessage = "Category Field Cannot be Left Blank!")]

        public string Category { get; set; }
        public DateTime DateCategory { get; set; }=DateTime.Now;
        public bool IsActive { get; set; } = true;
        public List<NewsCast> NewsCast { get; set; } = null;
    }
}