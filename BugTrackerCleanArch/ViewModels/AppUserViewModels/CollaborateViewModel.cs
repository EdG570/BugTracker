using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Application.ViewModels.AppUserViewModels
{
    public class CollaborateViewModel
    {
        public IEnumerable<AppUser> Collaborators { get; set; }
        public IEnumerable<SelectListItem> NonCollaborators { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public List<string> SelectedCollaborators { get; set; }
        public int ProjectId { get; set; }  
    }
}
