using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Core.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Url]
        public string RepositoryUri { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int OwnerId { get; set; }

        public virtual ICollection<UserProject> UserProjects { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}
