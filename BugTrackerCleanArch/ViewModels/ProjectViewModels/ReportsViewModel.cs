using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Application.ViewModels.ProjectViewModels
{
    public class ReportsViewModel
    {
        public int ProjectId { get; set; }
        public int TotalTicketPriorityHigh { get; set; }
        public int TotalTicketsPriorityMed { get; set; }
        public int TotalTicketsPriorityLow { get; set; }
        public int TotalTicketsPriorityUrgent { get; set; } 
        public int TotalTicketsStatusOpen { get; set; }
        public int TotalTicketsStatusInProgress { get; set; }
        public int TotalTicketsStatusClosed { get; set; }
        public int TotalTicketsTypeTask { get; set; }
        public int TotalTicketsTypeBug { get; set; }
        public int TotalTicketsTypeFeature { get; set; }    
    }
}
