using BugTracker.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Core.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Priority Priority { get; set; } = Priority.Medium;

        public Status Status { get; set; } = Status.Open;
        public TicketType Type { get; set; } = TicketType.Task;

        public int RequestorId { get; set; }
        public int AssignedToId { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
