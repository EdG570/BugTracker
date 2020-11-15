using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Core.Models
{
    public class UserProject
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
