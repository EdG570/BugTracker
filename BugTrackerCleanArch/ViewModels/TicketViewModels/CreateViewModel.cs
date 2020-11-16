using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Application.ViewModels.TicketViewModels
{
    public class CreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public string Priority { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public IEnumerable<SelectListItem> PriorityOptions { get; set; }
        public IEnumerable<SelectListItem> StatusOptions { get; set; }
        public IEnumerable<SelectListItem> TypeOptions { get; set; }
        public IEnumerable<SelectListItem> ProjectUsers { get; set; }

        public string AssignedToId { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
