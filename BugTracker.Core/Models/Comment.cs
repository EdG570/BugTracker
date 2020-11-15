using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Description { get; set; }
        
        [Required]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int CreatorId { get; set; }

        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
