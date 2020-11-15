using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsAcknowleged { get; set; } = false;

        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
