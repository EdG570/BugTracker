using BugTracker.Application.ViewModels.TicketViewModels;
using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Application.ViewModels.ProjectViewModels
{
    public class DetailViewModel
    {
        public Project Project { get; set; }
        public CreateViewModel TicketCreateVm { get; set; }
    }
}
