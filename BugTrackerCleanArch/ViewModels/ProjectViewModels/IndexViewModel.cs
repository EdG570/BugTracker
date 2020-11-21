using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Application.ViewModels.ProjectViewModels
{
    public class IndexViewModel
    {
        public AppUser User { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string NewProjectName { get; set; }

        [Url]
        public string ProjectRepoUrl { get; set; }  

        [MaxLength(255)]
        public string NewProjectDescription { get; set; }

        [Url]
        public string NewProjectRepoUrl { get; set; }
        public string PartialImageName { get; set; }    
    }
}
