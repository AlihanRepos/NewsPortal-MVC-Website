using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortalProject.Models
{
    public class NewsSubscriber
    {
        [Key]
        public int NewsSubscriberId { get; set; }
        public DateTime DateSubscriber { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int NewsSourceId { get; set; }
        public NewsSource NewsSource { get; set; }

    }
}