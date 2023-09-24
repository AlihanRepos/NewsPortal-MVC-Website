using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Models
{
    public class NewsSource
    {

        [Key]
        public int NewsSourceId { get; set; }
        [Required(ErrorMessage = "Corpation Name Field Cannot be Left Blank!")]

        public string CorpationName { get; set; }
        public string CorpationPassword { get; set; }
        public DateTime SourceDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public List<NewsSubscriber> NewsSubscribers { get; set; } = null;
        public List<NewsCast> NewsCast { get; set; } = null;
    }
}